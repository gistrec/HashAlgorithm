using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography; // MD5.Create(), SHA256Managed()
using System.IO; // File.OpenRead()

namespace Cryptography
{

    class Program
    {
        /**
         * Получаем SHA256 и MD5 хэш файла
         * @param filename - название файла
         */
        static void getFileHash(string filename)
        {
            var md5 = MD5.Create();
            var sha256 = new SHA256Managed();

            var stream = File.OpenRead(filename);

            var sha256_hash = sha256.ComputeHash(stream);
            stream.Seek(0, SeekOrigin.Begin); // Перемещаем указатель на начало файла
            var md5_hash = md5.ComputeHash(stream);

            Console.WriteLine(BitConverter.ToString(sha256_hash).Replace("-", "").ToLowerInvariant());
            Console.WriteLine(BitConverter.ToString(md5_hash).Replace("-", "").ToLowerInvariant());

            Environment.Exit(0);
        }

        static void getStringHash(string message, string key)
        {
            HMACSHA256 alice_hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            byte[] hash = alice_hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            Console.WriteLine(BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant());

            Environment.Exit(0);
        }


        static void Main(string[] args)
        {
            if (args[0] == "file_hash") getFileHash(args[1]);
            if (args[0] == "string_hash") getStringHash(args[1], args[2]);

            Environment.Exit(1);
        }
    }
}
