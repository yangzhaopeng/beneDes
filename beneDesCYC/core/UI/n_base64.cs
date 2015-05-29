using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// n_base64 的摘要说明
/// 
/// //		背景知识
//		
//		MIME是一种Internet协议，全称为“Multipurpose Internet Mail Extensions” ，
//		中文名称为“多用途互联网邮件扩展”。其实，它的应用并不局限于收发Internet
//		邮件——它已经成为Internet上传输多媒体信息的基本协议之一。本文仅关心MIME
//		的编码算法。
//		
//		MIME编码的原理就是把 8 bit 的内容转换成 7 bit 的形式以能正确传输，在接收方
//		收到之后，再将其还原成 8 bit 的内容。对邮件进行编码最初的原因是因为 Internet
//		上的很多网关不能正确传输8 bit 内码的字符，比如汉字等。MIME编码共有Base64、
//		Quoted-printable、7bit、8bit和Binary等几种。
//		
//		Base64算法将输入的字符串或一段数据编码成只含有
//		{''A''-''Z'', ''a''-''z'', ''0''-''9'', ''+'', ''/''}这64个字符的串，
//		''=''用于填充。其编码的方法是，将输入数据流每次取6 bit，用此6 bit的值(0-63)
//		作为索引去查表，输出相应字符。这样，每3个字节将编码为4个字符(3×8 → 4×6)；
//		不满4个字符的以''=''填充。
//
/// </summary>
public class n_base64
{
    //public n_base64()
    //{
		String[] EnCodeMap; //Base64编码矩阵
        int[] DeCodeMap; //Base64解码矩阵
        //Base64字符表
        const String CharMap = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
     
        public n_base64()
        {
            int len,index;

            EnCodeMap=new string[64];
            DeCodeMap=new int[138];

            len = CharMap.Length;
            for(int i=0;i<138;i++)
                DeCodeMap[i]=-1;

            for(index=0;index<len;index++)
            {
                EnCodeMap[index]=CharMap.Substring(index,1);
                DeCodeMap[(int)(CharMap[index])]=index;
            }            

        }   

        //public void print()
        //{
        //    int i;
        //    for(i=0;i<64;i++)
        //        System.Console.WriteLine(EnCodeMap[i]);
        //    for(i=0;i<138;i++)
        //        if(DeCodeMap[i]!=-1)
        //        {                       
        //            System.Console.WriteLine(Convert.ToString(i)+":"+Convert.ToString(DeCodeMap[i]));
        //        }
        //}

        //将一个正整数转化为二进制串
        public String of_binary(int dec)
        {            
            int rem;//余数
            String strBinary="";

            if(dec<0) return null;

            if(dec==0) return "0";

            while(dec!=0)
            {
                rem=dec%2;
                strBinary=Convert.ToString(rem)+strBinary;
                dec=dec/2;
            }
            return strBinary;
        }

        //将一个二进制串转换成相应的十进制整数,参数不合法返回-1
        public int of_decimal(String bin)
        {
            int len,index,dec;

            dec=0;
            len=bin.Length;

            if(bin==null || len==0)
                return -1;

            for(index=0;index<len;index++)
            {
                if(bin[index]!='0' && bin[index]!='1')
                    return -1;

                dec = dec + Convert.ToInt32(Math.Pow(2,len-1-index) * (bin[index]-'0'));
            }
            return dec;
        }

        //把两个整数进行与操作
        public int of_and(int first, int second)
        {           
            int pos;
            int result;
            string sf,ss,sr="";
            int lenf,lens;

            sf=of_binary(first);
            ss=of_binary(second);
            lenf=sf.Length;
            lens=ss.Length;

            //若长度不相等则进行补0操作
            if(lenf<lens)
                for(int i=1;i<=lens-lenf;i++)
                    sf="0"+sf;
            else if(lenf>lens)
                for(int i=1;i<=lenf-lens;i++)
                    ss="0"+ss;

            for(pos=0;pos<sf.Length;pos++)
                if(sf[pos]=='1'&&ss[pos]=='1')
                    sr=sr+"1";
                else
                    sr=sr+"0";
            
            result=of_decimal(sr);
            return result;
        }

        public String of_encode(String src)
        {
            String result=""; //返回编码的字符串
            int index,by3;
            int first,second,third;

            //需要编码的字符串为空返回空字符串            
            if(src.Length==0)
                return "";

            //循环编码
            by3 = (src.Length / 3) * 3;
            index = 0;
            
            while(index < by3)
            {
                first=(int)(src[index]);
                second=(int)(src[index+1]);
                third=(int)(src[index+2]);

                result = result + EnCodeMap[ of_and( first/4,63) ];//first的高6位
                result = result + EnCodeMap[ of_and( first*16,48) + of_and(second/16,15) ];//first的低2位和second的高4位
                result = result + EnCodeMap[ of_and( second*4,60) + of_and(third/64,3) ];//second的低4位和third的高2位
                result = result + EnCodeMap[ of_and(third,63) ];//third的低6位

                index+=3;
            }
            
            //检查是否有遗漏的字符，因为上面的循环是按3的倍数进行的，所以可能有1到2个字符遗漏
            //同时将不足3的倍数的部分用'='补齐
            if(by3 < src.Length)
            {
                first = (int)(src[index]);
                result = result + EnCodeMap[ of_and( first/4,63) ];//first的高6位
                if(src.Length % 3 == 2)
                {
                    second=(int)(src[index+1]);
                    result = result + EnCodeMap[ of_and( first*16,48) + of_and(second/16,15) ];//first的低2位和second的高4位
                    result = result + EnCodeMap[ of_and( second*4,60) ];//second的低4位右补0
                }
                else
                {
                    result = result + EnCodeMap[ of_and( first*16,48) ];//first的低2位右补0
                    result = result + "=";
                }
                result = result + "=";
            }

            return result;
        }

        public String of_decode(String des)
        {
            String result=""; //返回字符串
            int reallen;//真实长度
            int index,by4;//循环指针
            int first,second,third,fourth;

            //需要解码的字符串为空返回空字符串
            if(des.Length==0)
                return "";


            //求需要解码的字符串的真实长度，忽略最后的填充字符'='
            reallen = des.Length;
            while(des[reallen-1] == '=')
                reallen = reallen -1;

            //循环解码
            
          
            by4 = reallen / 4 * 4;
            index=0;
            while(index < by4)
            {
                first = DeCodeMap[(int)(des[index])];
                second = DeCodeMap[(int)(des[index+1])];
                third = DeCodeMap[(int)(des[index+2])];
                fourth = DeCodeMap[(int)(des[index+3])];

                result = result + Convert.ToString((char)(of_and(first*4,255) + of_and(second/16,3)));
                result = result + Convert.ToString((char)(of_and(second*16,255) + of_and(third/4,15)));
                result = result + Convert.ToString((char)(of_and(third*64,255) + of_and(fourth,63)));

                index = index + 4;
            }
            //检查是否有遗漏的字符没有处理，因为上面是按4的倍数循环的，所以可能有2到3个字符被遗漏未处理
              
            if(index<reallen)
            {
                first = DeCodeMap[(int)(des[index])];
                second = DeCodeMap[(int)(des[index + 1])];
                result = result + Convert.ToString((char)(of_and(first*4,255) + of_and(second/16,3)));
                if(reallen%4 == 3)
                {
                    third = DeCodeMap[(int)(des[index+2])];
                    result = result + Convert.ToString((char)(of_and(second*16,255) + of_and(third/4,15)));
                }

            }

            //返回解码的结果
            return result;
        }
    //}
}
