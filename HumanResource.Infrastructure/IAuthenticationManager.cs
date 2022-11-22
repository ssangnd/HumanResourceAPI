using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Infrastructure
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationFto userForAuth);
        Task<string> Createtoken();
    }
}
