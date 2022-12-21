using BeajLearner.Domain.Interfaces.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Infrastructure.Persistance.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private string SecretKey { get; set; }

        public EncryptionService(IConfiguration configuration, IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
            SecretKey = configuration["EncryptionKey"];

        }
        public EncryptionService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string valueToEncypt)
        {
            var protector = _dataProtectionProvider.CreateProtector(SecretKey);
            return protector.Protect(valueToEncypt);
        }

        public string Decrypt(string valueToDecrypt)
        {
            var protector = _dataProtectionProvider.CreateProtector(SecretKey);
            return protector.Unprotect(valueToDecrypt);
        }
    }
}
