using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Models
{
    public class Amigo
    {
        public int OrigemId { get; set; }
        public int DestinoId { get; set; }
        public bool Aceito { get; set; }
    }
}