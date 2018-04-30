using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VotingApplication.Services;

namespace VotingApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // adds cookie base authentication
            // adds scoped classes for things like UserManager, SignInManager, PassworkHashers
            // automatically adds the validated user from a cookie to the HttpContext.User
            services.AddIdentity<ApplicationUser, IdentityRole>()
                
                // adds the user store and the role store from this context
                // that are consumed by the user manager and role manager
                .AddEntityFrameworkStores<ApplicationDbContext>()

                // adds a provider that generates unique keys and hashes for things like
                // forgot password links, phone number verification codes etc...
                .AddDefaultTokenProviders();
            
            // change password policy
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings
                // we will not be using lockout.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                //options.Lockout.MaxFailedAccessAttempts = 10;
                //options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            // alter application cookie info
            services.ConfigureApplicationCookie(options =>
            {
                // redirect to login page
                options.LoginPath = "/Authentication/Login";
                options.LogoutPath = "/Authentication/Logout";
                options.AccessDeniedPath = "/Authentication/AccessDenied";

                // cookie expires in 5 minutes
                options.ExpireTimeSpan = TimeSpan.FromMinutes(100);

            });

            // Adds an email and sms service to the services container. This allows for these services to be injected into controllers.
            // See the following link for more details https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection 
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ISmsService, SmsService>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            IoCContainer.Provider = serviceProvider;

            using (var context = serviceProvider.GetService<ApplicationDbContext>())
            {
                //This only runs when the server starts
                context.Database.EnsureCreated();

                /**** MOVE ADD ROLES TO SQL FILE? ****/
                #region Add Roles
                if (context.Roles.Any() == false)
                {
                    RoleManager<IdentityRole> roleManager =
                        serviceProvider.GetService<RoleManager<IdentityRole>>();

                    // admin
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "Administrator"
                    }).Wait();

                    // manager
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "Manager"
                    }).Wait();

                    // user who has created an account
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "GenericUser"
                    }).Wait();

                    // voter who has completed registration
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "RegisteredVoter"
                    }).Wait();

                    // voter who has been varified by an admin or manager
                    roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "VerifiedVoter"
                    }).Wait();
                }
                #endregion

                #region Add Users
                if (context.Users.Any() == false)
                {
                    Random random = new Random();
                    const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    char[] userName, email;
                    ApplicationUser user;
                    UserManager<ApplicationUser> userManager =
                        serviceProvider.GetService<UserManager<ApplicationUser>>();

                    user = new ApplicationUser
                    {
                        UserName = "blah",
                        Email = "complete@random",
                        EmailConfirmed = true
                    };

                    //create an easy to login user
                    userManager.CreateAsync(user, "hello").Wait();

                    userManager.AddToRoleAsync(user, "Administrator").Wait();

                    //create 100 users for presenting
                    for (int i = 0; i < 100; i++)
                    {
                        userName = Enumerable.Repeat(chars, 10)
                            .Select(s => s[random.Next(s.Length)]).ToArray();
                        email = Enumerable.Repeat(chars, 20)
                            .Select(s => s[random.Next(s.Length)]).ToArray();
                        email[10] = '@';

                        user = new ApplicationUser
                        {
                            UserName = new string(userName),
                            Email = new string(email),
                            EmailConfirmed = true
                        };

                        userManager.CreateAsync(user, "hello").Wait();
                        userManager.AddToRoleAsync(user, "GenericUser").Wait();
                    }
                }
                #endregion

                /**** MOVE ADD OFFICES TO SQL FILE? ****/
                #region Add Offices
                if (context.Office.Any() == false)
                {
                    // from: http://www.politicalcampaigningtips.com/the-big-list-of-local-elected-offices-for-political-candidates/
                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "State Senator",
                        OfficeDescription = "The State Senate consists of representatives who are elected in districts that usually span several cities and counties.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "State Representative",
                        OfficeDescription = "The State House of Representatives, or State Assembly as it is called in some states, generally consists of members who are elected in from districts for terms of two years.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Board of County Commissioners",
                        OfficeDescription = "The position of County Commissioner is usually a full-time position with a term of four years.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Executive",
                        OfficeDescription = "Some counties have an elected County Executive in addition to, or instead of, County Commissioners.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Auditor",
                        OfficeDescription = "This position is appointed in some counties, but is a elected office in most with a four-year term.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Engineer",
                        OfficeDescription = "Many states only allow certified engineers to run for this position, which handles building, construction and road projects in the county.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Treasurer",
                        OfficeDescription = "The County Treasurer usually caries an elected, four-year term, but isn’t as much of a high-profile county race as Commissioner or Prosecutor.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Prosecuting Attorney",
                        OfficeDescription = "The County Prosecutor is among the most powerful and influential elected positions you can run for on the county level, but not everyone can qualify for the seat: you need to be an attorney to run. It is usually a full-time, four-year term.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Coroner",
                        OfficeDescription = "Surprisingly, this is actually an elected position in many counties, and to run for it you usually need to have your medical license or degree.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "County Recorder",
                        OfficeDescription = "This is another four-year elected county office which, like County Treasurer, is a bit more low-profile. In some counties, this position is appointed, not elected.",
                        OfficeLevel = "Low-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Common Pleas Court Judge",
                        OfficeDescription = "This is another elected office that has requirements to run: in order to be a candidate, you need to have your law degree or license, and most candidates are practicing attorneys.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Clerk of Court",
                        OfficeDescription = "You do not generally have to have a law degree to run for Clerk of Courts, but most candidates who run a political campaign for the office are attorneys. Clerk of Courts usually carries a four-year, full-time term.",
                        OfficeLevel = "Low-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Mayor",
                        OfficeDescription = "This is usually a full-time, four-year elected position, although the Mayor can also be part-time in smaller cities, villages, towns and townships. Mayor is generally considered the most powerful local elected position in a city.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "City Manager",
                        OfficeDescription = "In some cities, a City Manager is elected–or appointed by City Council–instead of a Mayor. Generally, City Managers have experience in urban planning and related fields. If the position is appointed, then City Council usually launches a recruitment campaign and interviews candidates from around the state or country for the job.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "City Treasurer",
                        OfficeDescription = "The City Treasurer keeps track of municipal bank accounts, income, taxes and other money matters. It is usually a four-year term, but is not considered full-time.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "City Auditor",
                        OfficeDescription = "The Auditor for a given city is also usually a four-year, part-time elected position. In most cases, successful City Auditor candidates also have similar careers and educational backgrounds.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "City Law Director",
                        OfficeDescription = "This is another local elected office that usually carries the requirement of having a law degree or license. The four-year, part-time position is in many cases held by a practicing attorney.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "President of City Council",
                        OfficeDescription = "Council President is usually in charge of setting agendas, committee assignments and chairing city council meetings. Many City Council Presidents hold office for two-year, part-time terms and are elected by the entire city.",
                        OfficeLevel = "High-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "City Ward Councilman",
                        OfficeDescription = "City Council is in many cases made up of councilpersons who are elected in individual city wards, as well as at-large council members who are elected by the entire city.",
                        OfficeLevel = "Low-Profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "At-Large Councilman",
                        OfficeDescription = "A Councilman/Councilwoman At-Large has the same duties as a ward councilperson, but they are elected by voters across the entire city instead of voters only in a specific ward.",
                        OfficeLevel = "Low-Profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Town Council",
                        OfficeDescription = "The legislative body of smaller villages, towns and townships are usually made up of trustees, which perform duties similar to that of city councilpersons and hold two-year, part-time terms.",
                        OfficeLevel = "Low-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "School Board Member",
                        OfficeDescription = "Candidates for School Board run for elected office in the school districts where they reside, and are in charge of voting on school issues. It is usually a two-year, part-time, paid position.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.Office.Add(new OfficeDataModel
                    {
                        OfficeName = "Precinct Committeeman",
                        OfficeDescription = "Each political party in a given county–Democrats and Republicans–is usually made up of elected precinct committee members, and they vote on county party issues like leadership and appointments to vacant offices. Precinct committee members are usually elected during presidential primary elections, and you can only run for precinct committeeman or committeewoman in your own precinct and for the party in which you are registered.",
                        OfficeLevel = "Medium-profile"
                    });

                    context.SaveChanges();
                }
                #endregion

                #region Add District for each state
                string StateAbbreviation(string abbreviation) {
                    switch (abbreviation.ToUpper())
                    {
                        case "AL": return "Alabama";
                        case "AK": return "Alaska";
                        case "AZ": return "Arizona";
                        case "AR": return "Arkansas";
                        case "CA": return "California";
                        case "CO": return "Colorado";
                        case "CT": return "Connecticut";
                        case "DE": return "Delaware";
                        case "FL": return "Florida";
                        case "GA": return "Georgia";
                        case "HI": return "Hawaii";
                        case "ID": return "Idaho";
                        case "IL": return "Illinois";
                        case "IN": return "Indiana";
                        case "IA": return "Iowa";
                        case "KS": return "Kansas";
                        case "KY": return "Kentucky";
                        case "LA": return "Louisiana";
                        case "ME": return "Maine";
                        case "MD": return "Maryland";
                        case "MA": return "Massachusetts";
                        case "MI": return "Michigan";
                        case "MN": return "Minnesota";
                        case "MS": return "Mississippi";
                        case "MO": return "Missouri";
                        case "MT": return "Montana";
                        case "NE": return "Nebraska";
                        case "NV": return "Nevada";
                        case "NH": return "New Hampshire";
                        case "NJ": return "New Jersey";
                        case "NM": return "New Mexico";
                        case "NY": return "New York";
                        case "NC": return "North Carolina";
                        case "ND": return "North Dakota";
                        case "OH": return "Ohio";
                        case "OK": return "Oklahoma";
                        case "OR": return "Oregon";
                        case "PA": return "Pennsylvania";
                        case "RI": return "Rhode Island";
                        case "SC": return "South Carolina";
                        case "SD": return "South Dakota";
                        case "TN": return "Tennessee";
                        case "TX": return "Texas";
                        case "UT": return "Utah";
                        case "VT": return "Vermont";
                        case "VA": return "Virginia";
                        case "WA": return "Washington";
                        case "WV": return "West Virginia";
                        case "WI": return "Wisconsin";
                        case "WY": return "Wyoming";
                        case "GU": return "Guam";
                        case "PR": return "Puerto Rico";
                        case "VI": return "Virgin Islands";
                        default: return abbreviation;
                    }
                }
                if (context.Zip.Any() == true)
                {
                    if(context.District.Any() == false)
                    {
                        IEnumerable<string> states = context.Zip.GroupBy(z => z.State).Select(grp => grp.FirstOrDefault().State);

                        foreach (string state in states)
                        {
                            if (StateAbbreviation(state) == "Empty")
                                continue;

                            var district = new DistrictDataModel()
                            {
                                DistrictName = "State of " + StateAbbreviation(state)
                            };
                            district.Zip = context.Zip.Where(z => z.State == state).Select(z => new ZipFillsDistrict()
                            {
                                DistrictName = district.DistrictName,
                                District = district,
                                ZipCode = z.ZipCode,
                                Zip = z
                            }).ToList();
                            context.District.Add(district);
                        }

                        context.SaveChanges();
                    }
                }
                #endregion
            }
            
            // setup identity
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
