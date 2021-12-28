

using System.Security.Cryptography;
using System.Text;

namespace transx.Utilities{
    public static class PasswordHash{
        public static byte[] GetHash(string s){
            using (HashAlgorithm algorithm= SHA256.Create()){
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(s));
            }
        }
        public static string StringHash(string s){
            StringBuilder stringBuilder = new StringBuilder();
            foreach(byte b in GetHash(s)){
                stringBuilder.Append(b.ToString("X2"));
            }
            return stringBuilder.ToString();
        }
    }
}