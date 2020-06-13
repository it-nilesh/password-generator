using System;

namespace PasswordGenerator
{
    public sealed class RandomSecurePassword
    {
        public string SecurePassword { get; }

        public RandomSecurePassword(string randomSecurePassword)
        {
            SecurePassword = randomSecurePassword;
        }

        public string GetPassword()
        {
            return SecurePassword;
        }

        public string ShuffleText()
        {
            return SecurePassword.ShuffleText();
        }

        public string GetPasswordStrength()
        {
            var findPasswordStrength = new FindPasswordStrength();
            return findPasswordStrength.GetPasswordStrength(SecurePassword);
        }

        public override string ToString()
        {
            return SecurePassword;
        }

        public void SavePassword(Action<string> securePassword)
        {
            securePassword?.Invoke(SecurePassword);
        }
    }
}
