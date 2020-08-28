using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        public byte[] _passwordHash;
        public byte[] _passwordSalt;

        public void CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            {
                _passwordSalt = hmac.Key;
                _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public byte[] CreatePasswordHash(string password, byte[] salt)
        {
            byte[] hash;
            using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
            {
                hash =  hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            return hash;
        }


        public byte[] GetPassworHash()
        {
            return _passwordHash;
        }

        public byte[] GetSalt()
        {
            return _passwordSalt;
        }
    }
}
