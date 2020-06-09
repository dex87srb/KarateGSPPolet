using HtmlAgilityPack;
using Karate_GSP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;


namespace Karate_Klub_GSP_Polet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GetWeatherInfo();
            return View();
        }
        protected void GetWeatherInfo()
        {
            string appId = "424108e37267fe2158f0a38346ce24b4";
            string city = "Belgrade";
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&APPID={1}", city, appId);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                WeatherInfo weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(json);

                ViewBag.Drzava = weatherInfo.Sys.Country;
                ViewBag.Grad = "Beograd";
                ViewBag.Temperatura = string.Format("{0} °C", Math.Truncate(weatherInfo.Main.Temp));
                ViewBag.TemperaturaMin = string.Format("{0} °C", weatherInfo.Main.Temp_min);
                ViewBag.TemperaturaMax = string.Format("{0} °C", weatherInfo.Main.Temp_max);
                ViewBag.Pritisak = string.Format("{0} hPa", weatherInfo.Main.Pressure);
                ViewBag.Vlaznost = string.Format("{0} %", weatherInfo.Main.Humidity);

                List weatherI = JsonConvert.DeserializeObject<List>(json);
                if (weatherI.Weather[0].Main.ToString() == "Clear")
                {
                    ViewBag.Stanje = "Vedro";

                }
                else if (weatherI.Weather[0].Main.ToString() == "Clouds")
                {
                    ViewBag.Stanje = "Oblačno";
                }
                else if (weatherI.Weather[0].Main.ToString() == "Rain")
                {
                    ViewBag.Stanje = "Kiša";
                }
                else if (weatherI.Weather[0].Main.ToString() == "Extreme")
                {
                    ViewBag.Stanje = "Oluja";
                }
                else if (weatherI.Weather[0].Main.ToString() == "Mist")
                {
                    ViewBag.Stanje = "Magla";
                }
                else ViewBag.Stanje = weatherI.Weather[0].Main.ToString();

                //  ViewBag.ImageUrl = string.Format("http://openweathermap.org/img/w/{0}.png", weatherI.Weather[0].Icon);

            }

        }

        [HttpGet]
        public IActionResult Video()
        {
            GetMetaTagValue();
            return View();

        }

        protected void GetMetaTagValue()
        {
            string url = "https://www.youtube.com/watch?v=CdcfpywZSfQ&t=1s";
            string url1 = "https://www.youtube.com/watch?v=beneA7a_VyA";
            string url2 = "https://www.youtube.com/watch?v=OfSd-_zdOUw";
            string url3 = "https://www.youtube.com/watch?v=h7yVHa48bVE";
            string url4 = "https://www.youtube.com/watch?v=1pqgHCnyQYk";

            ArrayList UrlList = new ArrayList
            {
                url,
                url1,
                url2,
                url3,
                url4
            };

            foreach (string item in UrlList)
            {
                WebClient x = new WebClient();
                string sourcedata = x.DownloadString(item);

                if (item == url)
                {
                    ViewBag.Text = Regex.Match(sourcedata, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                }
                else if (item == url1)
                {
                    ViewBag.Text1 = Regex.Match(sourcedata, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                }
                else if (item == url2)
                {
                    ViewBag.Text2 = Regex.Match(sourcedata, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                }
                else if (item == url3)
                {
                    ViewBag.Text3 = Regex.Match(sourcedata, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                }
                else 
                {
                    ViewBag.Text4 = Regex.Match(sourcedata, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
                }
            }
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Contact(ContactFormModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage
                    {
                        From = new MailAddress(vm.Email)//Email which you are getting 
                    };
                    //from contact us page 
                    msz.To.Add("dex87srb@gmail.com");//Where mail will be sent 
                    msz.Subject = vm.Subject;
                    msz.Body = vm.Message;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",

                        Port = 587,

                        Credentials = new NetworkCredential
                    ("loodness87@gmail.com", "danijeltomic1987"),

                        EnableSsl = true
                    };

                    smtp.Send(msz);

                    ModelState.Clear();
                    ViewBag.Message = "Hvala što ste nas kontaktirali.  ";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Greška!! {ex.Message}";
                }
            }

            return View();
        }

        public IActionResult PictureGallery()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

    }
}