using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Models
{
    public class Curtida
    {
        public int CurtidaId { get; set; }
        public int UsuarioId { get; set; }
        public int PostId { get; set; }
    }
}