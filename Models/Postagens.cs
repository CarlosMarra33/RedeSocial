using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Models
{
    public class Postagens
    {
        [Key]
        public int PostId { get; set; }

        public string Conteudo { get; set; }

        public DateTime Data { get; set; }

        public int UsuarioId { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Imagem")]
        public string Midia { get; set; }

        [Display(Name = "Curtidas")]
        public int NumCurtidas { get; set; }

    }
}