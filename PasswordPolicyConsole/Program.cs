using PasswordGenerator;
using System;
using System.Collections.Generic;

namespace PasswordPolicyConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IPasswordPolicy password = new PasswordPolicy(new NewPolicy());

            for (int i = 0; i < 10; i++)
            {
                var pswd = password.Generate();
                Console.WriteLine(password.ToString());
                Console.WriteLine(pswd.GetPassword());
                Console.WriteLine(pswd.SecurePassword);

                pswd.SavePassword(x =>
                {
                    Console.WriteLine("Password save method :: " + x);
                });

                Console.WriteLine("Password strength :: " + pswd.GetPasswordStrength());
                Console.WriteLine("Password shuffle text :: " + pswd.ShuffleText());
                Console.WriteLine("Password Validate as per policy :: " + password.IsValid(pswd.SecurePassword));
            }

            var userNameClass = new UserNameClass();
            userNameClass = password.AutoSetPassword<UserNameClass>(userNameClass, x => x.Password);

            List<UserNameClass> userNames = new List<UserNameClass>();
            userNames.Add(new UserNameClass
            {
                UserName = "UserName"
            });

            userNames.Add(new UserNameClass
            {
                UserName = "UserName1"
            });

            var userPassword = password.AutoSetPassword<UserNameClass>(userNames, x => x.Password);

            var userNameClasss = password.RenderPassword<UserNameClass>(userNames, (u, s) =>
             {
                 u.Password = s.SecurePassword;
             });

            string uniq = password.RenderUniquePassword(x =>
             {
                 Console.WriteLine(x);
                 return true;
             }, 2);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }

    public class UserNameClass
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class NewPolicy : Policy
    {
        public override int MaximumPasswordLength => 10;
        public override int MinimumPasswordLength => 10;
    }
}
