using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenPop.Pop3;
using System.Net.Mail;
using System.Web.Security;
using System.Net;
using OpenPop.Mime.Header;
using System.Collections;
using OpenPop.Mime;
using System.Data;
using System.Data.Entity;
using AsystentTrenera2.Models;

namespace AsystentTrenera2.Controllers
{
    public class WiadomosciController : Controller
    {
        //
        // GET: /Wiadomosci/
        Pop3Client client;
        AsystentTrenera2Context context = new AsystentTrenera2Context();
        string has="";

        public ActionResult Index()
        {
            if (has != "")
            {
                try
                {

                    var lista = OdbierzWiadomosci(has);
                    lista.Reverse();
                    return View(lista);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(string haslo)
        {
            has = haslo;
            try
            {
                
                var lista = OdbierzWiadomosci(haslo);
                lista.Reverse();
                return View(lista);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        public ActionResult NowaWiadomosc()
        {
            var listaUzytkownikow = Membership.GetAllUsers();
            SelectList lista = new SelectList(listaUzytkownikow, "Email", "UserName");
            var user = Membership.GetUser(User.Identity.Name);
            ViewBag.lista = lista;
            return View();
        }
        [HttpPost]
        public ActionResult Wyslij(string email,string tresc,string temat,string haslo)
        {
            WyslijWiadomosc(email, temat, tresc, haslo);
            return RedirectToAction("Index", new {haslo=haslo});
        }
        public void WyslijWiadomosc(string adres, string temat, string tresc, string haslo)
        {
            var user = Membership.GetUser(User.Identity.Name);
            string email = user.Email;


            var loginInfo = new NetworkCredential(email, haslo);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(adres));
            msg.Subject = temat;
            msg.Body = tresc;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }
        public List<OpenPop.Mime.Message> OdbierzWiadomosci(string haslo)
        {
           
            List<Message> listaWiadomosci = new List<Message>();
            try
            {
                var user = Membership.GetUser(User.Identity.Name);
                client = new Pop3Client();
                //dane serwera gmail
                client.Connect("pop.gmail.com", 995, true);
                //autoryzacja nazwa maila i haslo
                client.Authenticate(user.Email,haslo);
                for (int i = 1; i <= client.GetMessageCount(); i++)
                {
                    var message=client.GetMessage(i);
                    if (message.Headers.From.Address != user.Email)
                    {
                        listaWiadomosci.Add(message);
                    }
                }
                return listaWiadomosci;
            }
            catch
            {
                return null;
            }
        }

    }
}
