using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VotingApplication
{
    public class SecurityQuestions
    {
        //[Key]
        //public string Id { get; set; }

        //FixMe TODO
        [Required]
        [ForeignKey(nameof(SettingsDataModel)+"RefId")]
        public SettingsDataModel Account { get; set; }

        [Required]
        [MaxLength(256)]
        public string QuestionOne { get; set; }

        [Required]
        [MaxLength(256)]
        public string QuestionTwo { get; set; }

        [Required]
        [MaxLength(256)]
        public string AnswerOne { get; set; }

        [Required]
        [MaxLength(256)]
        public string AnswerTwo { get; set; }

    }
}
