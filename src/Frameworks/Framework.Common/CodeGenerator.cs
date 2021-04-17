using System;
using System.Linq;

namespace Framework.Common
{
    public static class CodeGenerator
    {
        private static Random random = new Random();
        public static string CreateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var code= new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return code;
        }
    }
}