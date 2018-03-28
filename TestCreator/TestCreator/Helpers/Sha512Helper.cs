using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TestCreator.Helpers
{
    public class Sha512Helper
    {
        public Sha512Helper()
        {

        }

        public string GetHashString(string input)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                SHA512Managed hashstring = new SHA512Managed();
                byte[] hash = hashstring.ComputeHash(bytes);
                string hashString = string.Empty;
                foreach (byte x in hash)
                {
                    hashString += x.ToString("x2");
                }

                return hashString;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}