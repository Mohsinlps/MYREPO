using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeajLearner.Domain.Interfaces.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string valueToEncypt);
        string Decrypt(string valueToDecrypt);
    }
}
