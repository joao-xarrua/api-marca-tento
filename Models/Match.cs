using System.ComponentModel.DataAnnotations.Schema;

namespace MarcaTento.Models
{
    public class Match
    {
        public int Id { get; set; }
        public string NameTeamOne { get; set; }
        public string NameTeamTwo { get; set;}
        public string Slug { get; set; }
        public string ImageTeamOne { get; set; }
        public string ImageTeamTwo { get; set; }
        public int ScoreTotal { get; set; }
        public int ScoreTeamOne { get; set; }
        public int ScoreTeamTwo { get; set; }
        public string MatchDate { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
