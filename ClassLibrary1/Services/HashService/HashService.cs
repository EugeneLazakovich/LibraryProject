using Lesson1_BL.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using System;

namespace Lesson1_BL.Services.HashService
{
    public class HashService : IHashService
    {
        private readonly HashOptions _hashOptions;
        public HashService(IOptions<HashOptions> options)
        {
            _hashOptions = options.Value;
        }

        public string HashString(string source)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: source,
                salt: Convert.FromBase64String(_hashOptions.Salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32));
        }
    }
}
