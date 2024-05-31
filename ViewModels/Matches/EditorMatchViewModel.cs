// Essa classe é a junção de dois view models diferentes:
// Um para "insert" e outro para "put". Então podemos
// nomea-lo como "editor" pois serve para editar um registro.

using MarcaTento.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarcaTento.ViewModels.Matches
{
    public class EditorMatchViewModel
    {
        /* Note que não é necessário passar o Id aqui,
         * porque ele é gerado automaticamente pelo 
         * banco. Isso foi definido no MatchMap.cs.
         * Também não é necessário passar o slug, porque
         * definimos ele utilizando uma Regex no arquivo
         * MatchController, na hora de fazer o POST.
         */
        public string NameTeamOne { get; set; }
        public string NameTeamTwo { get; set; }
        public string ImageTeamOne { get; set; }
        public string ImageTeamTwo { get; set; }
        public int ScoreTotal { get; set; }
        public int ScoreTeamOne { get; set; }
        public int ScoreTeamTwo { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
