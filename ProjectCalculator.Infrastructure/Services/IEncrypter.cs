using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectCalculator.Infrastructure.Services
{
    public interface IEncrypter
    {
        void CreatePasswordHash(string password);
        byte[] CreatePasswordHash(string password, byte[] salt);
        public byte[] GetPassworHash();
        public byte[] GetSalt();
    }
}
