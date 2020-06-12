using System;

namespace PasswordGenerator
{
    public interface IPasswordPolicy : IDisposable
    {
        RandomSecurePassword Generate();
    }
}
