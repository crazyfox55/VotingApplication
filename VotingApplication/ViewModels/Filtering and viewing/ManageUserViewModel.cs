using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class ManageUserViewModel
    {
        public ManageUserViewModel()
        {
                
        }
        public ManageUserViewModel(ApplicationUser user)
        {
            Username = user.UserName;
            PrevUsername = user.UserName;
            
            EmailConfirmed = user.EmailConfirmed ? "Yes" : "No";
            // this view model should show useful things to a manager
            //user.PhoneNumber;
            //user.PhoneNumberConfirmed;
            //user.TwoFactorEnabled;
        }
        [HiddenInput]
        public string PrevUsername { get; set; }

        [Required]
        [Remote(action: nameof(VerifyController.VerifyUniqueUserAsync), controller: "Verify")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [RegularExpression("Yes|No")]
        [Display(Name = "Email Confirmed")]
        public string EmailConfirmed { get; set; }


    }
}