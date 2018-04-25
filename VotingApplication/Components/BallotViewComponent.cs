using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class BallotViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _Context;

        public BallotViewComponent(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IViewComponentResult Invoke(BasicBallotSearchViewModel model)
        {
            FilteredBallotViewModel result = null;
            result = new FilteredBallotViewModel()
            {
                FilteredBallots = _Context.Ballot
                .Where(b => string.IsNullOrWhiteSpace(model.BallotId) || b.BallotName == model.BallotId)
                .Where(b => model.ElectionDay == null || b.ElectionDay.Date == model.ElectionDay.Value.Date)
                .Where(b => model.BallotName == null || b.BallotName == model.BallotName)
                .Select(b => new FilteredBallotViewModel.BallotViewModel()
                {
                    BallotName = b.BallotName,
                    ElectionDay = b.ElectionDay,
                    OfficeName = b.OfficeName,
                    // use region, if null use district, if null use zipcode; one of them should be not null
                    Zone = b.RegionName ?? b.DistrictName ?? b.ZipCode.ToString()
                }),
                ActionViewComponent = model.ActionViewComponent
            };

            return View(result);
        }
    }
}
