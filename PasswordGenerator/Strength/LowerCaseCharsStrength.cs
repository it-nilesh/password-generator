using System.Text.RegularExpressions;

namespace PasswordGenerator.Strength
{
    public class LowerCaseCharsStrength : BaseStrength
    {
        private readonly BaseStrength _baseStrength;
        public LowerCaseCharsStrength(BaseStrength baseStrength)
        {
            _baseStrength = baseStrength;
        }

        public override string GetStength(string password, int count)
        {
            if (ContainsLowerCaseChars(password))
            {
                count += 26;
            }

            return _baseStrength?.GetStength(password, count) ?? GetPasswordStrength(password, count);
        }

        private bool ContainsLowerCaseChars(string password)
        {
            Regex pattern = new Regex("[a-z]");
            return pattern.IsMatch(password);
        }
    }
}
