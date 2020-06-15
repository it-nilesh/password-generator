namespace PasswordGenerator
{
    public sealed class PasswordCharacter
    {
        private string _exclusiveLowerCaseChars;
        private string _exclusiveUpperCaseChars;
        private string _inclusiveSpecialChars;
        private string _exclusiveNumericChars;

        internal PasswordCharacter() { }

        public PasswordCharacter ExcludeNumericChars(string chars)
        {
            _exclusiveNumericChars = chars;
            return this;
        }

        public PasswordCharacter ExcludeLowerChars(string chars)
        {
            _exclusiveLowerCaseChars = chars;
            return this;
        }

        public PasswordCharacter ExcludeUpperChars(string chars)
        {
            _exclusiveUpperCaseChars = chars;
            return this;
        }

        public PasswordCharacter IncludeSpecialChars(string chars)
        {
            _inclusiveSpecialChars = chars;
            return this;
        }

        internal void SetNewPasswordChars(PasswordPolicySetting policySetting)
        {
            if (_exclusiveLowerCaseChars?.Length > 0)
            {
                policySetting.AllLowerCaseChars = policySetting.AllLowerCaseChars.ToRemoveChars(_exclusiveLowerCaseChars);
            }

            if (_exclusiveUpperCaseChars?.Length > 0)
            {
                policySetting.AllUpperCaseChars = policySetting.AllUpperCaseChars.ToRemoveChars(_exclusiveUpperCaseChars);
            }

            if (_inclusiveSpecialChars?.Length > 0)
            {
                policySetting.AllSpecialChars = _inclusiveSpecialChars;
            }

            if (_exclusiveNumericChars?.Length > 0)
            {
                policySetting.AllNumericChars = policySetting.AllNumericChars.ToRemoveChars(_exclusiveNumericChars);
            }
        }
    }
}
