using Blazored.LocalStorage;
using ClientApp.Model;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Service;

public class UserRepo
{
    private List<User> mUsers;
    
    public UserRepo()
    {
        mUsers = [new User { Name = "rip", Password = "1234", Role = "admin" },
                  new User { Name = "rap", Password = "2345", Role="Normal"},
                  new User { Name="rup", Password = "3456", Role="admin"}];
    }

    public User? ValidLogin(string name, string password)
    {
        foreach (User u in mUsers)
            if (u.Name == name && u.Password == password)
            {
                return u;
            }

        return null;
    }
    



}