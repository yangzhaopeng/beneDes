using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace beneDesCYC.model.month
{
    public class djfy_model
    {
        //使用以下代码进行费用查询   费用类型可通过for循环  获取。
        //从而，只要修改前台js中要展示的列字段，并将列字段作为参数传到后台，就可以实现加载多列了。@YZP     目的：省略fee_cross这个中间表
        //            select rownum,A.* from
        //(select ny,Dp.Dep_Name,substr(dj_id,instr(dj_id,'$')+1,length(dj_id)-instr(dj_id,'$')) JH
        //,sum(case when fee_code='cjsjf' then fee else 0 end) as CJSJF        --测井测试费
        //,sum(case when fee_code='csxzylwf' then fee else 0 end) as CSXZYLWF         --措施性作业劳务费
        //,sum(case when fee_code='whxzylwf' then fee else 0 end) as ZJCLF         --维护性作业劳务费
        //,sum(case when fee_code='ysf' then fee else 0 end) as YSF         --运输费
        //,sum(case when fee_code='zjzh' then fee else 0 end) as ZJZH           --折旧折耗
        //from djfy left join department  Dp on djfy.dep_id=Dp.dep_id 
        //where ny=201412 and Dp.dep_type='ZYQ' and cyc_id='qhtrq'  
        //group by ny,Dp.Dep_Name,dj_id)  A order by rownum

    }
}
