using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//using System.Data.SqlClient;
using System.Configuration;
using System.Data.OracleClient;

namespace beneDesYGS.core
{
    /// <summary>
    /// 连接SqlServer处理类

    /// </summary>
    public class SqlHelper
    {

        #region 方法

        #region GetConn()
        /// <summary>
        /// 获得数据连接串

        /// </summary>
        /// <returns></returns>
        public OracleConnection GetConn()
        {
            //string Strconn = ConfigurationSettings.AppSettings["conn"].ToString();
            //OracleConnection conn = new OracleConnection(Strconn);
            //OracleConnection conn = new OracleConnection(@"Data Source=.\SQLExpress;AttachDbFilename=E:\ly\extjs\beneDesYGS\dataBase\MsgMass_Data.MDF;Integrated Security=True;User Instance=True;");
            string StrConn = "";
            StrConn = ConfigurationManager.ConnectionStrings["ygsConnectionString"].ToString();//得到WebConfig数据库连接字符串
            OracleConnection Conn = new OracleConnection(StrConn);
            return Conn;
      
        }

        #endregion

        #region ExcuteSql()
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public int ExcuteSql(string strSql)
        {
            int count = 0;
            OracleConnection conn = GetConn();
            try
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(strSql, conn);
                count = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }
        #endregion

        #region GetDataSet()
        /// <summary>
        /// 获得数据集

        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, string strTableName)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            conn.Open();
            OracleCommand cmd = new OracleCommand();

            OracleTransaction concreteDbTrans = conn.BeginTransaction();

            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            OracleDataAdapter sda = new OracleDataAdapter();
            try
            {
                cmd.CommandText = sql;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).MissingSchemaAction = MissingSchemaAction.AddWithKey;
                ((OracleDataAdapter)sda).Fill(ds, strTableName);
                concreteDbTrans.Commit();
            }
            catch
            {
                concreteDbTrans.Rollback();
                ds.Clear();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetDataSet(string CommandText)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            conn.Open();
            OracleCommand cmd = new OracleCommand();

            OracleTransaction concreteDbTrans = conn.BeginTransaction();

            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            OracleDataAdapter sda = new OracleDataAdapter();
            try
            {
                cmd.CommandText = CommandText;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).Fill(ds);
            }
            catch
            {
                ds.Clear();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetDataSet(string CommandText, int start, int MaxRecordCoumt)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            conn.Open();
            OracleCommand cmd = new OracleCommand();

            OracleTransaction concreteDbTrans = conn.BeginTransaction();

            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            OracleDataAdapter sda = new OracleDataAdapter();
            try
            {
                cmd.CommandText = CommandText;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).Fill(ds, start, MaxRecordCoumt, "tab");
            }
            catch
            {
                ds.Clear();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return ds;
        }
        public DataSet GetDataSet(string cmdText, CommandType cmdType, params OracleParameter[] paras)
        {
            DataSet ds = new DataSet();
            OracleConnection conn = GetConn();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Parameters.AddRange(paras);
            cmd.CommandType = cmdType;
            OracleDataAdapter sda = new OracleDataAdapter(cmd);
            try
            {
                conn.Open();
                OracleTransaction concreteDbTrans = conn.BeginTransaction();
                cmd.Transaction = concreteDbTrans;
                cmd.CommandText = cmdText;
                sda.Fill(ds);
            }
            catch
            {
                ds.Clear();
                //throw;
            }
            finally
            {
                cmd.Parameters.Clear();
                conn.Close();
            }
            return ds;
        }
        #endregion

        #region UpdateDataSet
        /// <summary>
        /// 更新DataSet
        /// </summary>
        /// <param name="objSet"></param>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public bool UpdateDataSet(DataSet objSet, string SQL, string strTableName)
        {
            bool bolSuccess = true;
            DataSet dsTemp = new DataSet();
            OracleConnection conn = GetConn();
            try
            {
                conn.Open();

                OracleTransaction concreteDbTrans = conn.BeginTransaction();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.Transaction = concreteDbTrans;
                OracleDataAdapter sda = new OracleDataAdapter();
                cmd.CommandText = SQL;
                sda.SelectCommand = cmd;
                ((OracleDataAdapter)sda).MissingSchemaAction = MissingSchemaAction.AddWithKey;
                OracleCommandBuilder sqlBilder = new OracleCommandBuilder(sda);
                sda.DeleteCommand = sqlBilder.GetDeleteCommand();
                sda.InsertCommand = sqlBilder.GetInsertCommand();
                sda.UpdateCommand = sqlBilder.GetUpdateCommand();
                ((OracleDataAdapter)sda).Fill(dsTemp, strTableName);
                dsTemp = objSet;
                ((OracleDataAdapter)sda).Update(dsTemp, strTableName);
                concreteDbTrans.Commit();
            }
            catch
            {
                bolSuccess = false;
            }
            finally
            {
                conn.Close();
            }
            return bolSuccess;
        }
        #endregion

        #region GetExecuteScalar()
        /// <summary>
        /// 获取某一数值

        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object GetExecuteScalar(string sql)
        {
            OracleConnection conn = GetConn();
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            object scalar=null;
            OracleTransaction concreteDbTrans = conn.BeginTransaction();

            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            try
            {
                cmd.CommandText = sql;
                scalar = cmd.ExecuteScalar();
            }
            catch
            {
                concreteDbTrans.Rollback();
                //throw;
            }
            finally
            {
                conn.Close();
            }
            return scalar;

        }
        #endregion

        #region File2Blob()
        /// <summary>
        /// 
        /// </summary>
        /// <param name="remotingService"></param>
        /// <param name="commandText"></param>
        /// <param name="parameterName"></param>
        /// <param name="SourceFilePath"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool File2Blob(string commandText, string parameterName, string SourceFilePath, out string resultMsg)
        {
            try
            {
                System.IO.FileStream fs = new System.IO.FileStream(SourceFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                Byte[] bytBLOBData = new Byte[fs.Length];
                fs.Read(bytBLOBData, 0, bytBLOBData.Length);
                fs.Close();
                return Byte2Blob(commandText, parameterName, bytBLOBData, out resultMsg);
            }
            catch (Exception ex)
            {
                resultMsg = ex.Message;
                return false;
            }
        }
        #endregion

        #region Byte2Blob()
        public bool Byte2Blob(string CommandText, string parameterName, Byte[] blobData, out string errorMsg)
        {
            OracleConnection conn = GetConn();
            conn.Open();
            OracleTransaction concreteDbTrans = conn.BeginTransaction();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.Transaction = concreteDbTrans;
            try
            {
                cmd.CommandText = CommandText;
                cmd.Parameters.Add(CreateDataParameter(parameterName, blobData));
                cmd.ExecuteNonQuery();
                concreteDbTrans.Commit();
            }
            catch (Exception e)
            {
                concreteDbTrans.Rollback();
                errorMsg = e.Message;
                return false;
            }
            finally
            {
                conn.Close();
            }
            errorMsg = "";
            return true;
        }
        #endregion

        #region CreateDataParameter()
        public System.Data.Common.DbParameter CreateDataParameter(string parameterName, Byte[] blobData)
        {
            OracleParameter P = new OracleParameter(parameterName, OracleType.Blob, blobData.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, blobData);
            return P;
        }
        #endregion
        #region CreateOracleCommand>>OracleCommand,根据CommandText、OracleParameter[]、CommandType来创建SlqCommand对象，数据库连接默认
        /// <summary>
        /// 创建SlqCommand对象
        /// </summary>
        /// <param name="commandText">string命令文本</param>
        /// <param name="para">OracleParameter[]参数</param>
        /// <param name="commandType">CommandType命令类型</param>
        /// <returns></returns>
        public OracleCommand CreateOracleCommand(string commandText, OracleParameter[] para, CommandType commandType)
        {
            OracleCommand cmd = new OracleCommand(); //创建Command对象
            try
            {
                OracleConnection conn = GetConn();
                cmd.CommandType = commandType; //设置CommandType
                cmd.CommandText = commandText; //设置CommandText
                cmd.Connection = conn; //得到数据库连接

                if (para != null)
                {
                    foreach (OracleParameter sp in para)//添加存储过程参数
                    {
                        cmd.Parameters.Add(sp);
                    }
                }
            }
            catch (OracleException ex)
            {
                //throw ex;
            }
            return cmd;
        }
        #endregion 根据CommandText、OracleParameter[]、CommandType来创建SlqCommand对象，数据库连接默认
        #region FillDataSetByProc>>DataSet，根据CommandText、OracleParameter[]查询DataSet,CommandType默认为CommandType.StoredProcedure
        /// <summary>
        /// 查询DataSet
        /// </summary>
        /// <param name="commandText">命令文本</param>
        /// <param name="para">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>DataSet</returns>
        public DataSet FillDataSetByProc(string commandText, OracleParameter[] para)
        {
            return FillDataSet(commandText, para, CommandType.StoredProcedure);
        }
        #endregion
        #region FillDataSet>>DataSet，根据CommandText、OracleParameter[]、CommandType查询DataSet
        /// <summary>
        /// 查询DataSet
        /// </summary>
        /// <param name="commandText">命令文本</param>
        /// <param name="para">参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>DataSet</returns>
        public DataSet FillDataSet(string commandText, OracleParameter[] para, CommandType commandType)
        {
            OracleCommand cmd = null;
            try
            {
                //得到OracleCommand对象
                cmd = CreateOracleCommand(commandText, para, commandType);

                //create the DataAdapter & DataSet
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();

                //fill the DataSet using default values for DataTable names, etc.
                da.Fill(ds);

                // detach the OracleParameters from the command object, so they can be used again.			
                cmd.Parameters.Clear();

                //return the dataset
                return ds;
            }
            catch (Exception e)
            {
                //throw e;
                return null;
            }
            finally
            {
                try
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch
                {
                }
            }
        }
        #endregion

        #region Blob2File()
        /// <summary>
        /// 将数据库表中的二进制数据另存为文件

        /// </summary>
        /// <param name="commandText">SQL语句</param>
        /// <param name="pictureCol"></param>
        /// <param name="destFilePath"></param>
        /// <param name="resultMsg"></param>
        /// <returns></returns>
        public bool Blob2File(DataTable dataTable, string field, string destFilePath, out string resultMsg)
        {
            if (dataTable == null)
            {
                resultMsg = "DataTable is null";
                return false;
            }
            try
            {
                int rowCount = dataTable.Rows.Count;

                if (rowCount > 0)
                {
                    //BLOB is read into Byte array, then used to construct MemoryStream
                    Byte[] byteBLOBData = new Byte[0];
                    byteBLOBData = (Byte[])(dataTable.Rows[rowCount - 1][field]);
                    System.IO.FileStream fs = new System.IO.FileStream(destFilePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    fs.Write(byteBLOBData, 0, byteBLOBData.Length);
                    fs.Close();
                    resultMsg = "Successed";
                    return true;
                }
                else
                {
                    resultMsg = "No Data";
                    return false;
                }
            }
            catch (Exception ex)
            {
                resultMsg = ex.Message;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。BY ZJ 2011-12
        /// </summary>
        /// <param name="SQLStringList"></param>
        /// <returns>返回出错的位置，成功则返回-1</returns>
        public int ExecuteTranErrorCount(List<String> SQLStringList)
        {
            OracleConnection conn = GetConn();
            using (OracleConnection connection = conn)
            {
                connection.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = connection;
                OracleTransaction tx = connection.BeginTransaction();
                cmd.Transaction = tx;
                int count = 1;
                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count = n + 1;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return -1;
                }
                catch
                {
                    tx.Rollback();
                    return count;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
