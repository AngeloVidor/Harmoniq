using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harmoniq.BLL.Interfaces.UserContext
{
    public interface IUserContextService
    {
        int GetUserIdFromContext();

        Task<int?> GetContentConsumerIdByUserIdAsync(int userId);
        Task<int> GetContentCreatorIdByUserIdAsync(int userId);
    }
}