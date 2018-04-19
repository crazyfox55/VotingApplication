using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using VotingApplication.Controllers;

namespace VotingApplication.ViewModels
{
    public class VoterDashboardViewModel
    {
        public VoterDashboardViewModel()
        {

        }

        public VoterDashboardViewModel(bool registrationDone, bool addressDone, bool demographicsDone)
        {
            RegistrationDone = registrationDone;
            AddressDone = addressDone;
            DemographicsDone = demographicsDone;
        }

        public bool RegistrationDone { get; set; }
        public bool AddressDone { get; set; }
        public bool DemographicsDone { get; set; }

    }
}
