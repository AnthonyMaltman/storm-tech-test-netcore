using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Todo.Repositories
{

    public interface IUserStore
    {
        Task<IdentityUser> GetUserAsync(string userId);
    }
    public class UserStore : IUserStore
    {
        private readonly IUserStore<IdentityUser> _userStore;

        public UserStore(IUserStore<IdentityUser> userStore)
        {
            _userStore = userStore;
        }

        public async Task<IdentityUser> GetUserAsync(string userId)
        {
            return await _userStore.FindByIdAsync(userId, CancellationToken.None);
        }
    }
}
