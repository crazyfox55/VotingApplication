﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotingApplication.ViewModels;

namespace VotingApplication.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        public ApplicationDbContext _Context;

        public PaginationViewComponent(ApplicationDbContext context)
        {
            _Context = context;
        }

        public IViewComponentResult Invoke()
        {
            var zips = _Context.Zip.Count();

            var pageNav = new ViewPageNavigationViewModel()
            {
                Count = zips
            };

            return View(pageNav);
        }
    }
}
