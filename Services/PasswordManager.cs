using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Security;

namespace GenaroSilvestre.Services
{
    public class PasswordManager
    {

        public static class SaltGenerator
        {
            private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
            private const int SALT_SIZE = 24;

            static SaltGenerator()
            {
                m_cryptoServiceProvider = new RNGCryptoServiceProvider();
            }

            public static string GetSaltString()
            {
                // Lets create a byte array to store the salt bytes
                byte[] saltBytes = new byte[SALT_SIZE];

                // lets generate the salt in the byte array
                m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

                // Let us get some string representation for this salt
                string saltString = System.Text.Encoding.UTF8.GetString(saltBytes);

                // Now we have our salt string ready lets return it to the caller
                return saltString;
            }
        }

        public static string GetPasswordHashAndSalt(string message)
        {
            // Let us use SHA256 algorithm to 
            // generate the hash from this salted password
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            // return the hash string to the caller
            return System.Text.Encoding.UTF8.GetString(resultBytes);
        }


        public static string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = SaltGenerator.GetSaltString();

            string finalString = plainTextPassword + salt;

            return GetPasswordHashAndSalt(finalString);
        }

        public bool IsPasswordMatch(string password, string salt, string hash)
        {
            string finalString = password + salt;
            return hash == GetPasswordHashAndSalt(finalString);
        }
    }
}
