using PasswordGenerator.Validate;
using System;
using System.Text;

namespace PasswordGenerator
{
    public sealed class PasswordPolicy : IPasswordPolicy
    {
        public Policy Policy { get; }

        private readonly PasswordPolicySetting _settings;
        private readonly RandomPasswordSecureVersion _randomSecure = new RandomPasswordSecureVersion();

        public PasswordPolicy(Policy policy) :
            this(null, policy)
        { }

        public PasswordPolicy() :
           this(null, Policy.Default)
        { }

        public PasswordPolicy(Action<PasswordCharacter> policyCharsOptions, Policy policy)
        {
            ValidatePolicy.ThrowIfPolicyIsInValid(policy);
            _settings = new PasswordPolicySetting(policy, policyCharsOptions);
            Policy = policy;
        }

        public RandomSecurePassword Generate()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            var lengthOfPassword = _randomSecure.Next(Policy.MinimumPasswordLength, Policy.MaximumPasswordLength);
            passwordBuilder.Append(GetRandomString(_settings.AllLowerCaseChars, Policy.MinimumLowerCaseChars));
            passwordBuilder.Append(GetRandomString(_settings.AllUpperCaseChars, Policy.MinimumUpperCaseChars));
            passwordBuilder.Append(GetRandomString(_settings.AllNumericChars, Policy.MinimumNumericChars));
            passwordBuilder.Append(GetRandomString(_settings.AllSpecialChars, Policy.MinimumSpecialChars));
            passwordBuilder.Append(GetRandomString(_settings.AllAvailableChars, lengthOfPassword - passwordBuilder.Length));
            string password = GetMaxUniqueChars(passwordBuilder.ToString());
            return new RandomSecurePassword(password.ShuffleText());
        }

        private string GetMaxUniqueChars(string password)
        {
            if (Policy.MaximumUniqueChars > 0)
            {
                var uniqueChars = password.RemoveDuplicateChars();
                var charsLength = password.Length - uniqueChars.Length;

                if (uniqueChars.Length <= Policy.MaximumUniqueChars && charsLength > 0)
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
            var passwordValidate = new PasswordValidate(Policy);
            return passwordValidate.IsValid(password);
        }
    }
}
