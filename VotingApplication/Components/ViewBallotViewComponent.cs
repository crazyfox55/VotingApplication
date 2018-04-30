using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class ViewBallotViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(FilteredBallotViewModel.BallotViewModel model)
        {
            return View(model);
        }
    }
}
