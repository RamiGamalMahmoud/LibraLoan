﻿using LibraLoan.Core.DTOs;
using LibraLoan.Core.Models;

namespace LibraLoan.Core.Abstraction.Repositories
{
    public interface IUsersRepository : IRepository<User, UserDto>
    {
    }
}
