﻿using Microsoft.AspNetCore.Identity;

namespace UserService.Model;
    public class UserModel
{    
    public int Id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
}

