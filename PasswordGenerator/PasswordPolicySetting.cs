using System;
using System.Linq;
using System.Text;

namespace PasswordGenerator
{
    internal sealed class PasswordPolicySetting : IDisposable
    {
        public string AllAvailableChars { get; private set; }
        public string AllLowerCaseChars { get; set; }
        public string AllUpperCaseChars { get; set; }
        public string AllNumericChars { get; set; }
        public string AllSpecialChars { get; set; }

        public PasswordPolicySetting(Policy policy, Action<PasswordCharacter> characterOptions = null)
        {
            AllLowerCaseChars = GetCharRange('a', 'z');
            AllUpperCaseChars = GetCharRange('A', 'Z');
            AllNumericChars = GetCharRange('0', '9');
            AllSpecialChars = @"~!@#$%^&*_-+=`|\(){}[]:;<>,.?/";

            if (characterOptions.IsNotNull())
            {
                var passwordPolicyCharacters = new PasswordCharacter();
                characterOptions(passwordPolicyCharacters);
                passwordPolicyCharacters.SetNewPasswordChars(this);
            }

            SetPasswordOneChar(policy);
        }

        private void SetPasswordOneChar(Policy policy)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(policy.MinimumNumberOfChars, policy.MinimumLowerCaseChars, AllLowerCaseChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(policy.MinimumNumberOfChars, policy.MinimumUpperCaseChars, AllUpperCaseChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(policy.MinimumNumberOfChars, policy.MinimumNumericChars, AllNumericChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(policy.MinimumNumberOfChars, policy.MinimumSpecialChars, AllSpecialChars));
            AllAvailableChars = stringBuilder.ToString();
        }

        private string GetOnlyIfOneCharIsRequired(int minimumNumberOfChars, int minimum, string allChars)
        {
            return minimum > 0 || minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        internal string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }

            if (exclusiveChars.IsNotNullOrEmpty())
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }

            return result;
        }

        public void Dispose()
        {
            AllAvailableChars = null;
            AllLowerCaseChars = null;
            AllNumericChars = null;
            AllSpecialChars = null;
            AllUpperCaseChars = null;

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
