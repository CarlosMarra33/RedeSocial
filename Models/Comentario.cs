using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Models
{
    public class Comentario
    {
        public int CometarioId { get; set; }

        [Display(Name = "Comentario")]
        public string Texto { get; set; }

        public string Nome { get; set; }

        public int PostId { get; set; }
    }
}