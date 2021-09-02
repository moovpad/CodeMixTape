using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
	public class FetchData
	{
        protected System.Data.SqlClient.SqlConnection sqlConn;
        protected System.Data.SqlClient.SqlCommand sqlComm;
        protected System.Data.SqlClient.SqlDataReader rdr;
        public string conString;
        public string cmdText;

        public string[] dataresult;
        public string[] dataresults;

        public string workstation;
        public string srv;
        public string db;
        public string table;
        public string cols;
        public string vals;
        public int n;
        public int a;
        public int b;
        public object[] obj;
        public int TaskID;
        public string conditioncol;
        public string conditionval;
        public string[] resA;
        public string[] resB;

        //
        //NOTE: This class is built based on the assumptions:
        //  1. That the necessary parameters (cols, table, conditionalcol, conditionalval, n, a, and b) are set by the calling object;
        //  2. That windows authentication is used;
        //  3. That the object is used on a local machine or network.
        //

        //This is the main function of the class, and calls subordinate functions below as needed (GetRow, GetMultiRow, GetCol, GetUnknownRows).
        public string[] GExecute()
        {
            cmdText = @"SELECT " + cols + " FROM[" + table + "] WHERE " + conditioncol + "='" + conditionval + "'";
            conString = @"data source=" + srv + "; initial catalog =" + db + "; integrated security = SSPI; persist security info=True;workstation id=" + workstation + ";packet size=4096";
            switch (TaskID)
            {

                case 1:
                    dataresult = GetRow();
                    return dataresult;
                case 2:
                    dataresult = GetMultiRow();
                    return dataresult;
                case 3:
                    dataresult = GetCol();
                    return dataresult;
                case 4:
                    dataresult = GetUnknownRows();
                    return dataresult;
            }

            return null;
        }

        public string[] GetRow()
        {
            var obj = new string[2];
            obj[0] = cmdText;
            obj[1] = conString;
            Init(obj);
            sqlComm.Connection = sqlConn;
            sqlComm.Connection.Open();
            rdr = sqlComm.ExecuteReader(); //this replaces "me" from VB
            var i = 0;
            dataresult = new string[n];
            while (rdr.Read())
            {
                for (i = 0; i < n; i++) //need to verify that the ++i is correct, might be i++, not too sure here
                {
                    dataresult[i] = rdr.GetString(i);
                }
            }

            sqlComm.Connection.Close();
            return dataresult;
        }

        public string[] GetMultiRow()
        {
            var res = new string[a - 1];
            var obj = new string[2];
            obj[0] = cmdText;
            obj[1] = conString;
            Init(obj);
            sqlComm.Connection = sqlConn;
            sqlComm.Connection.Open();
            rdr = sqlComm.ExecuteReader();
            var i = 0;
            var j = 0;
            while (rdr.Read())
            {

                for (i = 0; i < b; i++)
                {
                    res[i + j] = rdr.GetString(i);
                    dataresult = res;

                }
                j += b;
            }
            sqlComm.Connection.Close();
            return dataresult;
        }

        public string[] GetCol()
        {
            var i = 0;
            var j = 0;
            var res = new string[n - 1];
            var obj = new string[2];
            obj[0] = cmdText;
            obj[1] = conString;
            Init(obj);
            sqlComm.Connection = sqlConn;
            sqlComm.Connection.Open();
            rdr = sqlComm.ExecuteReader();
            while (rdr.Read())
            {
                res[i + j] = rdr.GetString(i);
                dataresult = res;
                j += 1;
            }
            sqlComm.Connection.Close();
            return dataresult;
        }

        public string[] GetUnknownRows()
        {
            var res = new string[n - 1];
            var obj = new string[2];
            obj[0] = cmdText;
            obj[1] = conString;
            Init(obj);
            sqlComm.Connection = sqlConn;
            sqlComm.Connection.Open();
            rdr = sqlComm.ExecuteReader();
            var i = 0;
            var z = 0;
            while (rdr.Read())
            {
                for (i = 0; i < n; ++i)
                {
                    dataresult[i] = rdr.GetString(i);
                    if (Convert.ToBoolean(resA = null))
                    {
                        resA = new string[n - 1];
                        for (i = 0; i < n; ++i)
                        {
                            resA[i + z] = dataresult[i];
                            z += n;
                        }
                    }
                    else
                    {
                        resB = new string[n + z - 1];
                        var x = resA.Length;
                        var y = 0;
                        for (y = 0; y < x;)
                        {
                            resB[y] = resA[y];
                        }
                        for (i = 0; i < n; ++i)
                        {
                            resB[i + z] = dataresult[i];
                        }
                        resA = resB;
                        z += n;

                    }
                }
            }
            sqlComm.Connection.Close();
            return resA;
        }
    }
}
