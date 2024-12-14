using Microsoft.AspNetCore.Identity;
using static UserService.Data.UserDbContext;

namespace UserService.Model;
    public class UserModel : IdentityUser
    {
      public int Id {  get; set; }
     public RoleEnum Role { get; set; }    
    }

