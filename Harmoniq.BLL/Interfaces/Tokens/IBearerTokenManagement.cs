using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Harmoniq.BLL.DTOs;

namespace Harmoniq.BLL.Interfaces.Tokens
{
    public interface IBearerTokenManagement
    {
        Task<string> GenerateTokenAsync(UserRegisterDto userRegisterDto);
    }
}