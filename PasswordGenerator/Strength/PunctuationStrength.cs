using System.Text.RegularExpressions;

namespace PasswordGenerator.Strength
{
    public class PunctuationStrength : BaseStrength
    {
        private readonly BaseStrength _baseStrength;
        public PunctuationStrength(BaseStrength baseStrength)
        {
            _baseStrength = baseStrength;
        }

        public override string GetStength(string password, int count)
        {
            if (ContainsPunctuationStrength(password))
            {
                count += 31;
            }

            return _baseStrength?.GetStength(password, count) ?? GetPasswordStrength(password, count);
        }

        private bool ContainsPunctuationStrength(string password)
        {
            Regex pattern = new Regex(@"[\W|_]");
            return pattern.IsMatch(password);
        }
    }
}
