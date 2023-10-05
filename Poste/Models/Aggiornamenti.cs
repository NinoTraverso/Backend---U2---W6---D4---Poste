using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Poste.Models
{
    public class Aggiornamenti
    {

        [Required(ErrorMessage = "Not a valid package ID")]
        public int SpedizioneID { get; set; }

        [Required(ErrorMessage = "Insert the order status")]
        public string Satus { get; set; }

        [Required(ErrorMessage = "Insert the current package location")]
        public string Luogo { get; set; }

        [Required(ErrorMessage = "Insert description")]
        public string Descrizione { get; set; }

        [Required(ErrorMessage = "Insert date & time: YYYY-MM-DD HH-MM-SS")]
        public string DataEOra { get; set; }

        [Required(ErrorMessage = "Insert client's CF or P.Iva")]
        public string CodiceTasseCliente { get; set; }
        public static List<Aggiornamenti> ListaAggiornamenti { get; set; } = new List<Aggiornamenti>();
    }
}