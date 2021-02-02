using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ComisionesSaludOcupacional.Models.ClasesUtilidad
{
    /* Clase CryptoEngine 
     * Utilizada para la encriptación de contraseñas que van hacia la base de datos,
     * con fines de seguridad.*/
    public class CryptoEngine
    {
        /* Función que se encarga de encriptar una contraseña, mediante una key definida.
         * Parámetros: contraseña a encriptar, y key de encriptación.*/
        public static string Encrypt(string input)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("sxlw-3jn8-sqoy12");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /* Función que se encarga de desencriptar una contraseña, mediante una key definida.
         * Parámetros: contraseña a desencriptar, y key de encriptación.*/
        public static string Decrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes("sxlw-3jn8-sqoy12");
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}