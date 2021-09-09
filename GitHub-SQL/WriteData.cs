using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project
{
	public class WriteData
	{
		public string[] dbs;
		public string[] multitables;
		public string[] multidbs;
		public string[] multicols;
		public string[] multivals;
		public string[] multiconds;
		public int TaskID;

		public string workstation;
		public string srv;
		public string db;
		public string table;
		public string cols;
		public string vals;
		public int n;
		public int a;
		public int b;

        protected System.Data.SqlClient.SqlConnection sqlConn;
        protected System.Data.SqlClient.SqlCommand sqlComm;
        protected System.Data.SqlClient.SqlDataReader rdr;
        public string conString;
        public string cmdText;

        public void GExecute()
		{
            cmdText = "INSERT into " + table;
            conString = @"data source=" + srv + "; initial catalog =" + db + "; integrated security = SSPI; persist security info=True;workstation id=" + workstation + ";packet size=4096";
            switch (TaskID)
			{
				case '1':
					AddNewData();
					break;
				case '2':
					ModifyData();
					break;
			}
		}

        public void AddNewData()
        {
            if (Convert.ToBoolean(cols = null))
            {
                cmdText += " VALUES (" + vals + ")";
            }
            else
            {
                cmdText += " (" + cols + ") VALUES (" + vals + ")";
            }
            SendData();
        }

        public void ModifyData()
        {
            var i = 0;
            var j = 0;
            for (i = 0; i < n; ++i)
            {
                for (j = 0; j < multicols.Length; ++j)
                {
                    var obj1 = multicols[i];
                    var obj2 = multivals[i];
                    if (Convert.ToBoolean(j = multicols[i].Length - 1))
                    {
                        cmdText += obj1[j] + "='" + obj2[j] + "'"; //since for the last j index, no comma is needed
                    }
                    else
                    {
                        cmdText += obj1[j] + "='" + obj2[j] + "', ";

                    }
                    cmdText += " WHERE (" + multiconds[i] + ")";
                    SendData();
                }
            }
        }

        public void Init()
        {
            sqlConn.ConnectionString = conString;
            sqlComm.CommandText = cmdText;
        }

        public void SendData()
        {
            Init();
            sqlComm.Connection = sqlConn;
            sqlComm.Connection.Open();
            sqlComm.ExecuteNonQuery();
            sqlComm.Connection.Close();
        }
    }
}


