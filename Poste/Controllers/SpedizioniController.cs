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
    public class SpedizioniController : Controller
    {
        public ActionResult Index()
        {

            List<Spedizioni> spedizioniList = new List<Spedizioni>(); //lista vuota di tipo spedizione

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Spedizioni";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Spedizioni spedizione = new Spedizioni 
                            {
                                IdSpedizione = (int)reader["IdSpedizione"],
                                DataSpedizione = reader["DataSpedizione"].ToString(),
                                Peso = (int)reader["Peso"],
                                CittaDestinazione = reader["CittaDestinazione"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString(),
                                Destinatario = reader["Destinatario"].ToString(),
                                ClienteID = (int)reader["ClienteID"],
                                ConsegnaPrevista = reader["ConsegnaPrevista"].ToString()
                            };

                            spedizioniList.Add(spedizione);
                        }
                    }
                }
            }

            return View(spedizioniList);
        }

        [HttpGet]
        public ActionResult RegisterPackage()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RegisterPackage(Spedizioni newSpedizione)
        {

            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Spedizioni.ListaSpedizioni.Add(newSpedizione);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Spedizioni values (@DataSpedizione, @Peso, @CittaDestinazione, @Indirizzo, @Destinatario, @ClienteID, @ConsegnaPrevista)";

                    cmd.Parameters.AddWithValue("DataSpedizione", newSpedizione.DataSpedizione);
                    cmd.Parameters.AddWithValue("Peso", newSpedizione.Peso);
                    cmd.Parameters.AddWithValue("CittaDestinazione", newSpedizione.CittaDestinazione);
                    cmd.Parameters.AddWithValue("Indirizzo", newSpedizione.Indirizzo);
                    cmd.Parameters.AddWithValue("Destinatario", newSpedizione.Destinatario);
                    cmd.Parameters.AddWithValue("ClienteID", newSpedizione.ClienteID);
                    cmd.Parameters.AddWithValue("ConsegnaPrevista", newSpedizione.ConsegnaPrevista);

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


                return RedirectToAction("Index", "Spedizioni");


            }
            else
            {
                return View();
            }

        }
    }
}