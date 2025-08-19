using Domain.DTO.User;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Service
{
    public interface IUserService
    {
        Task<bool> Login(UserLoginDTO u);
        Task<bool> Register (UserRegisterDTO u);


    }
}
