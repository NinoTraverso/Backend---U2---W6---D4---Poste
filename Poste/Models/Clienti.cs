using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Clienti
    {

        public int IdCliente { get; set; }

        [Required(ErrorMessage = "Insert client name")]
        public string Nome {  get; set; }

        [Required(ErrorMessage = "Insert CF or P.Iva")]
        public string CodiceTasse { get; set; }

        [Required(ErrorMessage = "Insert client's address")]
        public string Indirizzo { get; set; }

        public static List<Clienti> ListClienti { get; set; } = new List<Clienti>();
    }
}