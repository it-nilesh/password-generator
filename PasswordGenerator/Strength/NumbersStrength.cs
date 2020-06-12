using System.Text.RegularExpressions;

namespace PasswordGenerator.Strength
{
    public class NumbersStrength : BaseStrength
    {
        private readonly BaseStrength _baseStrength;
        public NumbersStrength(BaseStrength baseStrength)
        {
            _baseStrength = baseStrength;
        }

        public override string GetStength(string password, int count)
        {
            if (ContainsNumbers(password))
            {
                count += 10;
            }

            return _baseStrength?.GetStength(password, count) ?? GetPasswordStrength(password, count);
        }

        private bool ContainsNumbers(string password)
        {
            Regex pattern = new Regex(@"[\d]");
            return pattern.IsMatch(password);
        }
    }
}
