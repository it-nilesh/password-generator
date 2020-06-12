using System;

namespace PasswordGenerator
{
    public sealed class RandomSecurePassword
    {
        private readonly string _randomSecurePassword;
        public RandomSecurePassword(string randomSecurePassword)
        {
            _randomSecurePassword = randomSecurePassword;
        }

        public string GetPassword()
        {
            return _randomSecurePassword;
        }

        public string ShuffleText()
        {
            return _randomSecurePassword.ShuffleTextSecure();
        }

        public string GetPasswordStrength()
        {
            var findPasswordStrength = new FindPasswordStrength();
            return findPasswordStrength.GetPasswordStrength(_randomSecurePassword);
        }

        public override string ToString()
        {
            return _randomSecurePassword;
        }

        public void SavePassword(Action<string> passwords)
        {
            passwords?.Invoke(_randomSecurePassword);
        }
    }
}
