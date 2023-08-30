﻿using filemanager_blazor.Models;

namespace filemanager_blazor.Utilities
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
    }

    public class UserService : IUserService
    {
        private IHttpService _httpService;

        public UserService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _httpService.Get<IEnumerable<User>>("/users");
        }
    }
}