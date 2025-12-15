using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;

namespace Application.Utils
{
    public class UtHash
    {
        public static string Hash(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Favor de proporcionar una contraseña");
            }
            string passHash = Argon2.Hash(password);
            return passHash;
        }

        public static bool verify(string password, string hashStoredPassword) {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Favor de proporcionar la contraseña");
            }
            bool status = Argon2.Verify(hashStoredPassword, password);
            
            return status;
        }
    }
}
