using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class CandidateSelectViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FilteredUsersViewModel.UserViewModel model)
        {
            return View(model);
        }
    }
}
