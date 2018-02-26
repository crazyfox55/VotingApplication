using Microsoft.Extensions.DependencyInjection;
using System;

namespace VotingApplication
{
    public static class IoC
    {
        public static ApplicationDbContext ApplicationDbContext => IoCContainer.Provider.GetService<ApplicationDbContext>();
    }

    public static class IoCContainer
    {
        public static IServiceProvider Provider { get; set; }
    }
}
