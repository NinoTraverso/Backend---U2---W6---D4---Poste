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
    public class AggiornamentiController : Controller
    {
        public ActionResult Index()
        {
            List<Aggiornamenti> aggiornamentiList = new List<Aggiornamenti>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Aggiornamenti";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Aggiornamenti aggiornamenti = new Aggiornamenti
                            {
                                SpedizioneID = (int)reader["SpedizioneID"],
                                Satus = reader["Satus"].ToString(),
                                Luogo = reader["Luogo"].ToString(),
                                Descrizione = reader["Descrizione"].ToString(),
                                DataEOra = reader["DataEOra"].ToString(),
                                CodiceTasseCliente = reader["CodiceTasseCliente"].ToString(),
                            };

                            aggiornamentiList.Add(aggiornamenti);
                        }
                    }
                }
            }

            return View(aggiornamentiList);
        }

        [HttpGet]
        public ActionResult Aggiorna()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Aggiorna(Aggiornamenti newAggiornamento)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Aggiornamenti.ListaAggiornamenti.Add(newAggiornamento);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Aggiornamenti values (@SpedizioneID, @Satus, @Luogo, @Descrizione, @DataEOra, @CodiceTasseCliente)";


                    cmd.Parameters.AddWithValue("SpedizioneID", newAggiornamento.SpedizioneID);
                    cmd.Parameters.AddWithValue("Satus", newAggiornamento.Satus);
                    cmd.Parameters.AddWithValue("Luogo", newAggiornamento.Luogo);
                    cmd.Parameters.AddWithValue("Descrizione", newAggiornamento.Descrizione);
                    cmd.Parameters.AddWithValue("DataEOra", newAggiornamento.DataEOra);
                    cmd.Parameters.AddWithValue("CodiceTasseCliente", newAggiornamento.CodiceTasseCliente);


                    int insertedSuccessfully = cmd.ExecuteNonQuery();

                    if (insertedSuccessfully > 0)
                    {
                        Response.Write("Inserted into database!");

                    }

                }
                catch (Exception ex)
                {
                    Response.Write("Error" + ex.Message);
                }
                finally
                { conn.Close(); }


                return RedirectToAction("Index", "Aggiornamenti");


            }
            else
            {
                return View();
            }
        }
    }
}