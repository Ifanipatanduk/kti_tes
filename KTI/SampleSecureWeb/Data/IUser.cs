using System;
using SampleSecureWeb.Models;

namespace SampleSecureWeb.Data;

public interface IUser
{
    User Registration(User user);
    User Login(User user);
    Models.User? GetUser(string username);  
    void UpdateUser(Models.User user);      
}
