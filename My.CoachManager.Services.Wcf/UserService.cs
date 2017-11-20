﻿using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.Application.Services.Users;
using My.CoachManager.CrossCutting.Unity;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Player Service.
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserById(int id)
        {
            return UnityFactory.Resolve<IUserAppService>().GetById(id);
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserByLoginAndPassword(string login, string password)
        {
            return UnityFactory.Resolve<IUserAppService>().GetByLoginAndPassword(login, password);
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public UserDto GetUserByLogin(string login)
        {
            return UnityFactory.Resolve<IUserAppService>().GetByLogin(login);
        }

    }
}