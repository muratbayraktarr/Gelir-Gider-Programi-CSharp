using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gelirgider
{
    public class Sql
    {
        public static SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=gelirgider;Integrated Security=True");

        public static void CheckConnection(SqlConnection temp)
        {
            if (temp.State == ConnectionState.Closed)
            {
                temp.Open();
            }


        }
    }
}
