namespace PasswordGenerator
{
    public class Policy
    {
        public virtual int MinimumPasswordLength { get; } = 4;
        public virtual int MaximumPasswordLength { get; } = 6;
        public virtual int MinimumLowerCaseChars { get; } = 1;
        public virtual int MinimumUpperCaseChars { get; } = 1;
        public virtual int MinimumNumericChars { get; } = 1;
        public virtual int MinimumSpecialChars { get; } = 1;
        public virtual int MaximumUniqueChars { get; } = 0;

        internal int MinimumNumberOfChars { get; } = 0;

        public static Policy Default { get { return new Policy(); } }

        protected Policy() { }

        private Policy(int minimumPasswordLength, int maximumPasswordLength,
                       int minimumLowerCaseChars, int minimumUpperCaseChars,
                       int minimumNumericChars, int minimumSpecialChars,
                       int maximumUniqueChars)
        {
            MinimumPasswordLength = minimumPasswordLength;
            MaximumPasswordLength = maximumPasswordLength;
            MinimumLowerCaseChars = minimumLowerCaseChars;
            MinimumUpperCaseChars = minimumUpperCaseChars;
            MinimumNumericChars = minimumNumericChars;
            MinimumSpecialChars = minimumSpecialChars;
            MaximumUniqueChars = maximumUniqueChars;
            MinimumNumberOfChars = minimumLowerCaseChars + minimumUpperCaseChars + minimumNumericChars + minimumSpecialChars;
        }

        public static Policy OfOptNumber(int passwordLength = 4, int uniqueNumber = 2)
        {
            return new Policy(passwordLength, passwordLength, 0, 0, 1, 0, uniqueNumber);
        }
    }
}
