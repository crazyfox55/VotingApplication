using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel(ApplicationUser user)
        {
            Username = user.UserName;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed ? "Yes" : "No";
            // this view model should show useful things to a manager
            //user.PhoneNumber;
            //user.PhoneNumberConfirmed;
            //user.TwoFactorEnabled;
        }

        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Email Confirmed")]
        public string EmailConfirmed { get; set; }
    }
}
