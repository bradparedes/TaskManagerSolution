﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;

namespace TaskManager.Infrastructure.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
