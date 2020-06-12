using PasswordGenerator;
using System;
using System.Collections.Generic;

namespace PasswordPolicyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IPasswordPolicy passwordGenerator = new PasswordPolicy(3, 3, 0, 1, 0, 1);

            var userNameClass = new UserNameClass();

            userNameClass = passwordGenerator.AutoSetPassword<UserNameClass>(userNameClass, x => x.Password);

            List<UserNameClass> userNames = new List<UserNameClass>();
            userNames.Add(new UserNameClass
            {
                UserName = "UserName"
            });

            userNames.Add(new UserNameClass
            {
                UserName = "UserName1"
            });

            userNames = passwordGenerator.AutoSetPassword<UserNameClass>(userNames, x => x.Password);

            var ss = passwordGenerator.RenderPassword<UserNameClass>(userNames, (u, s) =>
             {
                 u.Password = s.GetPasswordStrength();
             });

            string uniq = passwordGenerator.RenderUniquePassword(x =>
             {
                 return true;
             }, 2);

            for (int i = 0; i < 2; i++)
             {
               var pssd = passwordGenerator.Generate();
               Console.WriteLine(passwordGenerator.ToString());
               Console.WriteLine(pssd.GetPassword());
               Console.WriteLine(pssd.ShuffleText());
               Console.WriteLine(pssd.GetPasswordStrength());
               
              pssd.SavePassword(x =>
              {
                  Console.WriteLine(x);
              });

            }

            Console.WriteLine(userNameClass.Password);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

    public class UserNameClass
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
