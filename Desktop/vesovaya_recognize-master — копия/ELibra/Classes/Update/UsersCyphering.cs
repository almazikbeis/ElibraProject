using ELibra.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ELibra.Classes.Update
{
    class UsersCyphering
    {
        public static void Cipher()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                var editTable = from i in context.Users
                                select i;
                foreach (var item in editTable)
                {
                    if (item.password.Length != 32)
                    {
                        item.password = Hash(item.password);
                    }
                }
                context.SaveChanges();
            }
        }

        public static string Hash(string password)
        {
            byte[] hash = Encoding.ASCII.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;        
        }

    }
}
