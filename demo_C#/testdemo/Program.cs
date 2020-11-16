using System;
using System.Text;
using Mono.Security.Cryptography;

namespace testdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string txt = "39";
            DateTime d1; //保存开始时间
            DateTime d2; //保存程序当前运行时间
            d1 = DateTime.UtcNow; 
            //循环次数根据题目时间情况修改
            for (int i = 0; i < 200000000; i++)
            {
                txt = Md2Crypto(txt);
                if (i % 1000000 == 1)
                {
                    Console.WriteLine($"{i:d9}:{txt}");
                    d2 = DateTime.UtcNow;
                    Console.WriteLine($"{d2-d1:c}");
                }
                
            }
            Console.WriteLine($" {txt}");
            d2 = DateTime.UtcNow;
            Console.WriteLine($"{d2-d1:c}");

            

        }

        public static string Md2Crypto(string source)
        {
            using (MD2 myMD2 = MD2.Create())
            {
                try
                {
                    byte[] input = Encoding.UTF8.GetBytes(source);
                    byte[] output=  myMD2.ComputeHash(input);

                    string hashstr = GetHexStrByteArray(output);
                   
                    return hashstr;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;        
                }
            }
        }
        /// <summary>
        /// 官方文档介绍为了hash后是需要核实后位十六进制字节数组，为了方便查看结果，格式化成2位十六禁止位的字符串
        /// https://docs.microsoft.com/zh-cn/dotnet/api/system.security.cryptography.md5?view=netframework-4.8
        /// ComputeHash类的方法将 MD5 哈希值作为16字节的数组返回。 请注意，某些 MD5 实现产生了32字符的十六进制格式的哈希。
        /// 若要与此类实现进行互操作，请将方法的返回值格式化 ComputeHash 为十六进制值。
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string GetHexStrByteArray(byte[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                //输出2位的十六进制
                result+=($"{array[i]:x2}");
                 
            }

            return result;
        }
    }
}