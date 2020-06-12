using PasswordGenerator.Strength;

namespace PasswordGenerator
{
    internal sealed class FindPasswordStrength
    {
        private readonly BaseStrength _baseStrength;
        internal FindPasswordStrength()
        {
            _baseStrength = new NumbersStrength(new LowerCaseCharsStrength(new UpperCaseCharsStrength(new PunctuationStrength(null))));
        }

        public string GetPasswordStrength(string password)
        {
            return _baseStrength.GetStength(password, 0);
        }
    }
}
