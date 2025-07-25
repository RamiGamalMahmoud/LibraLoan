﻿namespace LibraLoan.Core.Abstraction
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string password, string hash);
    }
}
