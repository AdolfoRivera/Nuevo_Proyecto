using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//-----------------CONEXION-------
using System.Data.SqlClient;
using SoftOne.Properties;
using System.Configuration;
namespace SoftOne
{
    class Conexion
    {
        public static string ObtenerString()
        {
            return Settings.Default.BD_SofOneConnectionString;
        }

        public static SqlConnection ObtenerConexion()
        {
            SqlConnection con = new SqlConnection(ObtenerString());
            //con.Open();
            return con;
        }
    }
}
