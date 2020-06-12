using System;

namespace PasswordGenerator
{
    public class PasswordPolicy : IPasswordPolicy
    {
        public int MinimumLengthPassword { get; }
        public int MaximumLengthPassword { get; }
        public int MinimumLowerCaseChars { get; }
        public int MinimumUpperCaseChars { get; }
        public int MinimumNumericChars { get; }
        public int MinimumSpecialChars { get; }


        private readonly PasswordPolicySetting _settings;
        private readonly RandomPasswordSecureVersion _randomSecure = new RandomPasswordSecureVersion();

        public PasswordPolicy(int minimumLengthPassword = 4, int maximumLengthPassword = 6,
                               int minimumLowerCaseChars = 1, int minimumUpperCaseChars = 1,
                                 int minimumNumericChars = 1, int minimumSpecialChars = 1) :
            this(null, minimumLengthPassword, maximumLengthPassword, minimumLowerCaseChars, minimumUpperCaseChars, minimumNumericChars, minimumSpecialChars)
        {
        }

        public PasswordPolicy(Action<PasswordPolicyCharacter> policyCharsOptions,
                              int minimumLengthPassword = 4, int maximumLengthPassword = 6,
                              int minimumLowerCaseChars = 1, int minimumUpperCaseChars = 1,
                              int minimumNumericChars = 1, int minimumSpecialChars = 1)

        {
            MinimumLengthPassword = minimumLengthPassword;
            MaximumLengthPassword = maximumLengthPassword;
            MinimumLowerCaseChars = minimumLowerCaseChars;
            MinimumUpperCaseChars = minimumUpperCaseChars;
            MinimumNumericChars = minimumNumericChars;
            MinimumSpecialChars = minimumSpecialChars;

            var _minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;

            Policy.ThrowIfPolicyIsInValid(minimumLengthPassword, maximumLengthPassword,
                                          minimumLowerCaseChars, minimumUpperCaseChars, minimumNumericChars,
                                          minimumSpecialChars, _minimumNumberOfChars);

            var passwordConfig = new PasswordPolicySetting();
            passwordConfig.CharsConf(policyCharsOptions);
            _settings = new PasswordPolicySetting(_minimumNumberOfChars, this, passwordConfig);
        }

        public RandomSecurePassword Generate()
        {
            var lengthOfPassword = _randomSecure.Next(MinimumLengthPassword, MaximumLengthPassword);

            var minimumChars =
                        GetRandomString(_settings.AllLowerCaseChars, MinimumLowerCaseChars) +
                        GetRandomString(_settings.AllUpperCaseChars, MinimumUpperCaseChars) +
                        GetRandomString(_settings.AllNumericChars, MinimumNumericChars) +
                        GetRandomString(_settings.AllSpecialChars, MinimumSpecialChars);

            var restChars = GetRandomString(_settings.AllAvailableChars, lengthOfPassword - minimumChars.Length);
            var unshuffeledResult = minimumChars + restChars;

            return new RandomSecurePassword(unshuffeledResult.ShuffleTextSecure());
        }

        private string GetRandomString(string possibleChars, int lenght)
        {
            var result = string.Empty;
            for (var position = 0; position < lenght; position++)
            {
                var index = _randomSecure.Next(possibleChars.Length);
                result += possibleChars[index];
            }
            return result;
        }

        public override string ToString()
        {
            return this.Generate()
                       .GetPassword();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _randomSecure.Dispose();
            }

            _disposed = true;
        }
    }
}
