using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

namespace COMPLETE_FLAT_UI
{
    internal class funcionesUsuario
    {
        public static string ruta = "Server=CARAPRCTIFSD170\\SQLEXPRESS;Database=BD_TiendaDonPedro;trusted_connection=true;";
        public static string message = "";
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
        public static DataTable PA_Usuario_Seleccionar_Uno (string alias)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("PA_Usuario_Seleccionar_Uno",ruta);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@alias", alias);
                adapter.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                message = e.Message;
                return dt;
            }
        }
        public static bool PA_Usuario_Insertar(string alias, string nombre, string apellido, string pass, string rol)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("PA_Usuario_Insertar", ruta);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@alias", alias);
                adapter.SelectCommand.Parameters.AddWithValue("@nombre", nombre);
                adapter.SelectCommand.Parameters.AddWithValue("@apellido", apellido);
                adapter.SelectCommand.Parameters.AddWithValue("@pass", pass);
                adapter.SelectCommand.Parameters.AddWithValue("@rol", rol);
                adapter.Fill(dt);
                return true;
            }
            catch(Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        public static bool PA_Usuario_Editar(long id,string alias, string nombre, string apellido, string pass, string rol)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("PA_Usuario_Editar", ruta);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@alias", alias);
                adapter.SelectCommand.Parameters.AddWithValue("@nombre", nombre);
                adapter.SelectCommand.Parameters.AddWithValue("@apellido", apellido);
                adapter.SelectCommand.Parameters.AddWithValue("@pass", pass);
                adapter.SelectCommand.Parameters.AddWithValue("@rol", rol);
                adapter.SelectCommand.Parameters.AddWithValue("@id", id);
                adapter.Fill(dt);
                return true;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }
        public static DataTable PA_Usuario_Cargar_Todo()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("PA_Usuario_Seleccionar_Todo", ruta);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dt);
                return dt;
            }
            catch(Exception e)
            {
                message = e.Message;
                return dt;
            }
        }
        public static bool PA_Usuario_Eliminar(string usuario)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("PA_Usuario_Eliminar", ruta);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.SelectCommand.Parameters.AddWithValue("@usuario", usuario);
                adapter.Fill(dt);
                return true;
            }
            catch(Exception e)
            {
                message = e.Message;
                return false;
            }
        }
    }
}
