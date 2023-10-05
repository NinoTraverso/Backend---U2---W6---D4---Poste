using Poste.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poste.Controllers
{
    public class TrackingController : Controller
    {
        
        public ActionResult Index()
        {
            List<Tracking> trackingList = new List<Tracking>(); 

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "ho provato a fare una query per fare l'inner join delle mie due tabelle ma ho avuto poco tempo rimasto e non ci sono riuscito";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tracking spedizione = new Tracking
                            {
                                IdSpedizione = (int)reader["IdSpedizione"],
                                CodiceTasse = reader["CodiceTasse"].ToString(),
                            };

                            trackingList.Add(spedizione);
                        }
                    }
                }
            }

            return View(trackingList);
        }
    }
}