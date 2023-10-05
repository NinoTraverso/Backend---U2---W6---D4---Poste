using Poste.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Poste.Controllers
{
    public class LoginController : Controller
    {

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // /////////////////////////////////////// LOG IN PER ADMIN /////////////////////////////////////////////////
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Username, Password")] Users u)
        {
            string conn = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(conn);

            try
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("select * from Users where Username=@Username and Password=@Password and Role='Admin'", sqlConnection);
                sqlCommand.Parameters.AddWithValue("Username", u.Username);
                sqlCommand.Parameters.AddWithValue("Password", u.Password);

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    Session["Username"] = u.Username;
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("RegisterPackage", "Spedizioni");
                }
                else
                {
                    ViewBag.Error = "You are not an administrator";
                    return View();
                }
            }
            catch (Exception ex)
            {
                
            }
            finally { sqlConnection.Close(); }

            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}