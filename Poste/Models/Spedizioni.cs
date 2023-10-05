using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Spedizioni
    {

        public int IdSpedizione { get; set; }

        [Required(ErrorMessage = "Date must be in the form: YYYY-MM-DD")]
        public string DataSpedizione { get; set; }

        [Required(ErrorMessage = "Enter weight 0 and up")]
        public int Peso { get; set; }

        [Required(ErrorMessage = "Enter city")]
        public string CittaDestinazione { get; set; }

        [Required(ErrorMessage = "Insert an address")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Insert the person receiving the package")]
        public string Destinatario { get; set; }

        [Required(ErrorMessage = "Not a valid client ID")]
        public int ClienteID { get; set; }

        [Required(ErrorMessage = "Date must be in the form: YYYY-MM-DD")]
        public string ConsegnaPrevista { get; set; }

        public static List<Spedizioni> ListaSpedizioni { get; set; } = new List<Spedizioni>();
    }
}