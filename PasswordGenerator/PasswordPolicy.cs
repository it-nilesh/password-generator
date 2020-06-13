using PasswordGenerator.Validate;
using System;
using System.Text;

namespace PasswordGenerator
{
    public sealed class PasswordPolicy : IPasswordPolicy
    {
        public int MinimumPasswordLength { get; }
        public int MaximumPasswordLength { get; }
        public int MinimumLowerCaseChars { get; }
        public int MinimumUpperCaseChars { get; }
        public int MinimumNumericChars { get; }
        public int MinimumSpecialChars { get; }
        public int MaximumUniqueChars { get; }

        private readonly PasswordPolicySetting _settings;
        private readonly RandomPasswordSecureVersion _randomSecure = new RandomPasswordSecureVersion();

        public PasswordPolicy(int minimumPasswordLength = 4, int maximumPasswordLength = 6,
                              int minimumLowerCaseChars = 1, int minimumUpperCaseChars = 1,
                              int minimumNumericChars = 1, int minimumSpecialChars = 1,
                              int maximumUniqueChars = 0) :
            this(null, minimumPasswordLength, maximumPasswordLength, minimumLowerCaseChars, minimumUpperCaseChars,
                        minimumNumericChars, minimumSpecialChars, maximumUniqueChars)
        { }

        public PasswordPolicy(Action<PasswordPolicyCharacter> policyCharsOptions,
                              int minimumPasswordLength = 4, int maximumPasswordLength = 6,
                              int minimumLowerCaseChars = 1, int minimumUpperCaseChars = 1,
                              int minimumNumericChars = 1, int minimumSpecialChars = 1,
                              int maximumUniqueChars = 0)

        {
            MinimumPasswordLength = minimumPasswordLength;
            MaximumPasswordLength = maximumPasswordLength;
            MinimumLowerCaseChars = minimumLowerCaseChars;
            MinimumUpperCaseChars = minimumUpperCaseChars;
            MinimumNumericChars = minimumNumericChars;
            MinimumSpecialChars = minimumSpecialChars;
            MaximumUniqueChars = maximumUniqueChars;

            var minimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;

            Policy.ThrowIfPolicyIsInValid(minimumPasswordLength, maximumPasswordLength,
                                          minimumLowerCaseChars, minimumUpperCaseChars, minimumNumericChars,
                                          minimumSpecialChars, minimumNumberOfChars);

            var passwordConfig = new PasswordPolicySetting();
            passwordConfig.CharsConf(policyCharsOptions);
            _settings = new PasswordPolicySetting(minimumNumberOfChars, this, passwordConfig);
        }

        public RandomSecurePassword Generate()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            var lengthOfPassword = _randomSecure.Next(MinimumPasswordLength, MaximumPasswordLength);
            passwordBuilder.Append(GetRandomString(_settings.AllLowerCaseChars, MinimumLowerCaseChars));
            passwordBuilder.Append(GetRandomString(_settings.AllUpperCaseChars, MinimumUpperCaseChars));
            passwordBuilder.Append(GetRandomString(_settings.AllNumericChars, MinimumNumericChars));
            passwordBuilder.Append(GetRandomString(_settings.AllSpecialChars, MinimumSpecialChars));
            passwordBuilder.Append(GetRandomString(_settings.AllAvailableChars, lengthOfPassword - passwordBuilder.Length));

            string password = GetMaxUniqueChars(passwordBuilder.ToString());
            return new RandomSecurePassword(password.ShuffleText());
        }

        private string GetMaxUniqueChars(string password)
        {
            if (MaximumUniqueChars > 0)
            {
                var uniqueChars = password.RemoveDuplicateChars();
                var charsLength = password.Length - uniqueChars.Length;
                if (uniqueChars.Length <= MaximumUniqueChars && charsLength > 0)
                {
                    var availableChars = _settings.AllAvailableChars.ToRemoveChars(uniqueChars);

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(uniqueChars);
                    stringBuilder.Append(GetRandomString(availableChars, charsLength));
                    return GetMaxUniqueChars(stringBuilder.ToString());
                }
            }

            return password;
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
            return this.Generate().SecurePassword;
        }

        public void Dispose()
        {
            _randomSecure.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool IsValid(string password)
        {
            var passwordValidate = new PasswordValidate(MinimumPasswordLength, MaximumPasswordLength, MinimumLowerCaseChars, MinimumUpperCaseChars,
                                                        MinimumNumericChars, MinimumSpecialChars, MaximumUniqueChars);
            return passwordValidate.IsValid(password);
        }
    }
}
