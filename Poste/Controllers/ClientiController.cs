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
    public class ClientiController : Controller
    {
        public ActionResult Index()
        {
            List<Clienti> clientiList = new List<Clienti>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "select * from Clienti";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Clienti cliente = new Clienti
                            {
                                IdCliente = (int)reader["IdCliente"],
                                Nome = reader["Nome"].ToString(),
                                CodiceTasse = reader["CodiceTasse"].ToString(),
                                Indirizzo = reader["Indirizzo"].ToString()
                            };

                            clientiList.Add(cliente);
                        }
                    }
                }
            }

            return View(clientiList);
        }

        [HttpGet]
        public ActionResult AddClienti()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddClienti(Clienti newCliente)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionstring);

            if (ModelState.IsValid)
            {
                Clienti.ListClienti.Add(newCliente);

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;

                    cmd.CommandText = "insert into Clienti values (@Nome, @CodiceTasse, @Indirizzo)";

                    cmd.Parameters.AddWithValue("Nome", newCliente.Nome);
                    cmd.Parameters.AddWithValue("CodiceTasse", newCliente.CodiceTasse);
                    cmd.Parameters.AddWithValue("Indirizzo", newCliente.Indirizzo);
      

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


                return RedirectToAction("Index", "Clienti");


            }
            else
            {
                return View();
            }

        }
    }
}