using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApplication.Controllers
{
    public static class StringExtensions
    {
        public static string RemoveController(this string name)
        {
            return name.Remove(name.Length - 10);
        }
    }
}
