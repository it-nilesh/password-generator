using System;

namespace PasswordGenerator.Strength
{
    public abstract class BaseStrength
    {
        protected BaseStrength()
        {
        }

        public abstract string GetStength(string password, int count);

        protected virtual string GetPasswordStrength(string password, int charSet)
        {
            string passStrength = string.Empty;

            double result = Math.Log(Math.Pow(charSet, password.Length)) / Math.Log(2);

            if (result <= 32)
            {
                passStrength = "Weak";
            }
            else if (result <= 64)
            {
                passStrength = "Good";
            }
            else if (result <= 128)
            {
                passStrength = "Strong";
            }
            else if (result > 128)
            {
                passStrength = "Very Strong";
            }

            return passStrength;
        }
    }
}
