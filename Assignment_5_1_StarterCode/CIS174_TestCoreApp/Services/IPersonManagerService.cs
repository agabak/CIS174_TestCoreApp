﻿using CIS174_TestCoreApp.Models;
using System.Threading.Tasks;

namespace CIS174_TestCoreApp.Services
{
    public interface IPersonManagerService
    {
        Task<UserManagerUpdateCommandModel> FindUserByName(string username);
        Task<bool> UpdateUser(UserManagerUpdateCommandModel model);
        Task<bool> Register(RegisterCommandModel model);
        Task<bool> Login(LoginViewModel model);
        void Logout();
    }
}
