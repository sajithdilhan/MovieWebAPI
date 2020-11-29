using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWebAPI
{
    public interface IUserService
    {
        string Login(string userName, string password);
    }
}
