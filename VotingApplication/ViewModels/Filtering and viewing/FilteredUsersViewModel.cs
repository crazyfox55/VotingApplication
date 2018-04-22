using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.ViewModels
{
    public class FilteredUsersViewModel
    {
        public string ActionViewComponent;

        public IEnumerable<UserViewModel> FilteredUsers { get; set; }
        public struct UserViewModel
        {
            public string UserId { get; set; }

            [Display(Name = "Username")]
            public string UserName { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Display(Name = "Party")]
            public string Party { get; set; }

            [Display(Name = "Ballot Name")]
            public string BallotName { get; set; }
        }
    }
}
