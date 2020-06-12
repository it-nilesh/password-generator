namespace PasswordGenerator
{
    public class PasswordPolicyCharacter
    {
        private char[] _exclusiveLowerCaseChars;
        private char[] _exclusiveUpperCaseChars;
        private char[] _inclusiveSpecialChars;
        private char[] _exclusiveNumericChars;

        public PasswordPolicyCharacter ExcludeNumericChars(string chars)
        {
            _exclusiveNumericChars = chars.ToCharArray();
            return this;
        }

        public PasswordPolicyCharacter ExcludeLowerChars(string chars)
        {
            _exclusiveLowerCaseChars = chars.ToCharArray();
            return this;
        }

        public PasswordPolicyCharacter ExcludeUpperChars(string chars)
        {
            _exclusiveUpperCaseChars = chars.ToCharArray();
            return this;
        }

        public PasswordPolicyCharacter IncludeSpecialChars(string chars)
        {
            _inclusiveSpecialChars = chars.ToCharArray();
            return this;
        }

        internal void SetNewPasswordChars(PasswordPolicySetting policySetting)
        {
            if (_exclusiveLowerCaseChars?.Length > 0)
            {
                policySetting.AllLowerCaseChars = policySetting.AllLowerCaseChars.Trim(_exclusiveLowerCaseChars);
            }

            if (_exclusiveUpperCaseChars?.Length > 0)
            {
                policySetting.AllUpperCaseChars = policySetting.AllUpperCaseChars.Trim(_exclusiveUpperCaseChars);
            }

            if (_inclusiveSpecialChars?.Length > 0)
            {
                policySetting.AllSpecialChars = new string(_inclusiveSpecialChars);
            }

            if (_exclusiveNumericChars?.Length > 0)
            {
                policySetting.AllNumericChars = policySetting.AllNumericChars.Trim(_exclusiveNumericChars);
            }
        }
    }
}
