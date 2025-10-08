using Blazored.LocalStorage;
using ClientApp.Model;
using Microsoft.AspNetCore.Components;

namespace ClientApp.Service;

public class UserRepo
{
    private List<User> mUsers;
    
    private static User? loggedInUser = null;
    
    public UserRepo()
    {
        mUsers = [new User { Name = "rip", Password = "1234", Role = "admin" },
                  new User { Name = "rap", Password = "2345", Role="Normal"},
                  new User { Name="rup", Password = "3456", Role="admin"}];
    }

    public async Task<User?> ValidLogin(string name, string password)
    {
        foreach (User u in mUsers)
            if (u.Name == name && u.Password == password)
            {
                loggedInUser = u;
                return u;
            }

        return null;
    }

    public User? UserLoggedIn()
    {
        return loggedInUser;
    }



}