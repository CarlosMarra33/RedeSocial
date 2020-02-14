using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetodeBloco.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe o seu nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o seu email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe uma senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Confirma Senha")]
        [Compare("Senha", ErrorMessage ="As senhas não são iguais")]
        [Required(ErrorMessage = "Confirme sua senha")]
        [DataType(DataType.Password)]
        public string ConfirmaSenha { get; set; }

        [Required(ErrorMessage = "Informe a sua Data de nascimento")]
        [Display(Name = "Data de Nascimanto")]
        [DataType(DataType.DateTime)]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "Informe o seu gênero")]
        [Display(Name = "Gênero")]
        public char Genero { get; set; }

    }
}