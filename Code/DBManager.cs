using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Student
{
    public class DBManager
    {
        private static SqlConnection _connection = null;

        public SqlConnection GetSQLConnection()
        {
            if (_connection == null)
            {
                //string configvalue1 = @"Data Source=.\sqlexpress;Initial Catalog=School;Integrated security = true;";

                string configvalue1 = @"Data Source=.;Initial Catalog=School;Integrated security = true";
                
                // ConfigurationSettings.AppSettings["SQLDBConnection"].ToString();
                _connection = new SqlConnection(configvalue1);
            }

            return _connection;

        }
    }
}