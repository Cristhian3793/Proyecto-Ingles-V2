using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PagosVarios.LoginDb
{
    public static class Funciones
    {
        public static string ConectarMail()
        {
            string cadena = "tx0MFWCG55OvJYcY6H/Bw1N7/Jlcsk1O";
            string cadena1 = Desencriptar(cadena);
            return cadena1;
        }
        private static string Encriptar(string Input)
        {

            byte[] IV = ASCIIEncoding.ASCII.GetBytes("qualityi"); //La clave debe ser de 8 caracteres
            byte[] EncryptionKey = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5"); //No se puede alterar la cantidad de caracteres pero si la clave
            byte[] buffer = Encoding.UTF8.GetBytes(Input);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = IV;

            return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));

        }
        private static string Desencriptar(string Input)
        {

            byte[] IV = ASCIIEncoding.ASCII.GetBytes("qualityi"); //La clave debe ser de 8 caracteres
            byte[] EncryptionKey = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5"); //No se puede alterar la cantidad de caracteres pero si la clave
            byte[] buffer = Convert.FromBase64String(Input);
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = EncryptionKey;
            des.IV = IV;
            return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));

        }
        public static string EncriptarURL(string Input)
        {
            string query = Encriptar(Input);
            query = query.Replace("+", "-").Replace("/", "_").Replace("=", "~");


            return HttpUtility.UrlEncode(query);
        }

        public static string DesencriptarURL(string Input)
        {
            string query = HttpUtility.HtmlDecode(Input);
            query = query.Replace("-", "+").Replace("_", "/").Replace("~", "=");

            return Desencriptar(query);



        }
        public static string Encripta(string sPasswd)
        {
            string fc_b1 = null;
            string fc_b2 = null;
            string fc_b3 = null;
            string fc_b4 = null;
            string fc_b5 = null;
            string fc_password = null;
            byte[] fn_b1 = null;
            byte[] fn_b2 = null;
            byte[] fn_b3 = null;
            byte[] fn_b4 = null;
            byte[] fn_b5 = null;
            int fn_c1 = 0;
            int fn_c2 = 0;
            int fn_c3 = 0;
            int fn_c4 = 0;
            int fn_c5 = 0;

            if (sPasswd.Length < 5)
            {
                sPasswd = new string(' ', 5 - sPasswd.Length) + sPasswd;
            }
            fc_b1 = sPasswd.Substring(0, 1);
            fc_b2 = sPasswd.Substring(1, 1);
            fc_b3 = sPasswd.Substring(2, 1);
            fc_b4 = sPasswd.Substring(3, 1);
            fc_b5 = sPasswd.Substring(4, 1);
            fn_b1 = Encoding.ASCII.GetBytes(fc_b1);
            fn_b2 = Encoding.ASCII.GetBytes(fc_b2);
            fn_b3 = Encoding.ASCII.GetBytes(fc_b3);
            fn_b4 = Encoding.ASCII.GetBytes(fc_b4);
            fn_b5 = Encoding.ASCII.GetBytes(fc_b5);
            fn_c1 = fn_b1[0] + 3;
            fn_c2 = fn_b2[0] + 5;
            fn_c3 = fn_b3[0] + 2;
            fn_c4 = fn_b4[0] + 4;
            fn_c5 = fn_b5[0] + 1;
            fc_password = Char.ConvertFromUtf32(fn_c1) + (Char)fn_c4 + (Char)fn_c5 + (Char)fn_c3 + (Char)fn_c2;
            return fc_password;
        }
        /// <summary>
         /// Encripta una cadena de carecteres.
         /// </summary>
         /// <param name="Input"></param>
         /// <returns></returns>
        public static string Ec_sender(string Input)
        {
            return Encriptar(Input);
        }
    } 
    
    public class Producto
    {
        public string codConcepto { get; set; }
        public string concepto { get; set; }
        public decimal importe { get; set; }
        public int cantidad { get; set; }
        public decimal iva { get; set; }
    }
}