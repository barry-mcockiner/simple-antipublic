using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using AtleX.HaveIBeenPwned;
using HaveIBeenPwned;
namespace anti_public
{
    class Program
    {
        static void Main(string[] args)
        {
            int pub = 0;
            int nonp = 0;
            int error = 0;
            Console.Title = $"Antipublic - by barry-mcockiner | public: {pub} | private: {nonp}";
            Console.WriteLine("enter path to combo");
            string path = Console.ReadLine();
            Console.WriteLine("checking starts.");
            foreach(string line in File.ReadAllLines(path))
            {
                string[] split_data = line.Split(':');
                var pwned = new HaveIBeenPwned.Password.HaveIBeenPwned();
                // Call IsPasswordPwned with a plain text string password to know if the password was leaked at least once
                bool isPasswordPwned = pwned.IsPasswordPwned(split_data[1]);
                try
                {
                    if (!isPasswordPwned)
                    {
                        nonp++;
                        File.AppendAllText("NonPublic.txt", line + "\n");
                    }
                    if (isPasswordPwned)
                    {
                        pub++;
                    }
                }
                catch(Exception ex)
                {
                    error++;
                    Console.WriteLine(ex.Message);
                }

                Console.Title = $"Antipublic - by notaps#7777 | public: {pub} | private: {nonp} | retries: {error} ";
            }
        }
    }
}
