using System.Text.RegularExpressions;

namespace PasswordGenerator.Strength
{
    public class UpperCaseCharsStrength : BaseStrength
    {
        private readonly BaseStrength _baseStrength;
        public UpperCaseCharsStrength(BaseStrength baseStrength)
        {
            _baseStrength = baseStrength;
        }

        public override string GetStength(string password, int count)
        {
            if (ContainsUpperCaseChars(password))
            {
                count += 26;
            }

            return _baseStrength?.GetStength(password, count) ?? GetPasswordStrength(password, count);
        }

        private bool ContainsUpperCaseChars(string password)
        {
            Regex pattern = new Regex("[A-Z]");
            return pattern.IsMatch(password);
        }
    }
}
