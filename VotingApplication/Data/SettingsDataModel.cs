using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VotingApplication
{
    public class SettingsDataModel
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(512)]
        public string Value { get; set; }

    }
}
