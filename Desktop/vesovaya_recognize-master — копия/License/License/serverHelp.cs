using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace License
{
    public static class serverHelp
    {
        public static string getToken(string login, string pass)
        {
            string data = "username=" + login + "&password=" + pass;
            string url = Properties.Settings.Default.Host + "/api/login?" + data;
            HttpWebResponse res = getHttpResp(url);
            return res.Headers["Authorization"];
        }

        public static string getCompanies(string login, string pass)
        {

            string token = getToken(login, pass);
            string url = Properties.Settings.Default.Host + "/api/companies";
            string res = getHttp(url, token);
            return res;
        }

        public static void addCompanie(string login, string pass, string companyName, string licenseCode, string expirationDate, string token="")
        {
            string url = Properties.Settings.Default.Host + "/api/create";
            string data = "username=" + login + "&password=" + pass + "&companyName=" + companyName + "&licenseCode=" + licenseCode + "&expirationDate=" + expirationDate;
            postHttp(url, data, token);
        }

        public static void postHttp(string url, string data="", string token="")
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url + "?" + data);
            req.Method = "POST";
            if (token != "")
                req.Headers.Add("Authorization", token);
            string result;
            using (var responseStream = req.GetResponse().GetResponseStream())
            using (var sr = new StreamReader(responseStream))
            {
                result = sr.ReadToEnd();
            }
        }

        public static void changeLicense(string name, string licenseCode, string date)
        {
            string data = "licenseCode=" + licenseCode + "&companyName=" + name + "&expirationDate=" + date;
            string url = Properties.Settings.Default.Host + "/api/license";

            postHttp(url, data, getToken("manager", "4urL2Keo"));
        }

        public static HttpWebResponse getHttpResp(string url, string token="")
        {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                if (token != "")
                    req.Headers.Add("Authorization", token);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                resp.Close();
                return resp;        
        }

        public static string getHttp(string url, string token = "")
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            if (token != "")
                req.Headers.Add("Authorization", token);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string sLine;
            sLine = sr.ReadLine();
            resp.Close();
            sr.Close();
            return sLine;
        }
    }
}
