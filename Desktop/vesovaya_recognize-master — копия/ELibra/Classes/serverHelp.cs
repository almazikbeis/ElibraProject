using ELibra.DBModel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;

namespace ELibra.Classes
{
    public class serverHelp
    {
        //private static DataBaseContext context = new DataBaseContext();
        //private static string[] URL = (from i in context.Settings
        //                             where i.id == 1
        //                             select i.HostDB).FirstOrDefault().Split('|');



        public static string getToken(string login, string pass, string fLogin = "")
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string[] URL = (from i in context.Settings
                              where i.id == 1
                              select i.HostDB).FirstOrDefault().Split('|');

                string data = "username=" + login + "&password=" + pass;
                string uri;
                if (string.IsNullOrEmpty(fLogin))
                    uri = URL[0] + "/api/login?" + data;
                else
                    uri = fLogin + "/api/login?" + data;
                HttpWebResponse res = getHttpResp(uri);
                return res.Headers["Authorization"];
            }
            
        }

        public static List<string> getLicense(string token)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string[] URL = (from i in context.Settings
                                where i.id == 1
                                select i.HostDB).FirstOrDefault().Split('|');
                string url = URL[0] + "/api/license";
                string res = "[" + getHttp(url, token) + "]";

                #region Парсинг json документа
                JArray a = JArray.Parse(res);
                List<string> arr = new List<string>();
                foreach (JObject o in a.Children<JObject>())
                {
                    foreach (JProperty p in o.Properties())
                    {
                        string value = (string)p.Value;
                        arr.Add(value);
                    }
                }
                #endregion

                return arr;

            }
        }

        public static void addWeighing(string json, string token)
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                string[] URL = (from i in context.Settings
                                where i.id == 1
                                select i.HostDB).FirstOrDefault().Split('|');
                string url;
                if (URL.Length > 1)
                    url = URL[1] + "/api/add";
                else
                    url = URL[0] + "/api/add";

                postHttp(url, json, token);
            }
        }

        public static HttpStatusCode postHttp(string url, string data = "", string token = "")
        {
            try
            {
                #region Создание и настройка requst'а
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.Method = "POST";
                if (token != "")
                    req.Headers.Add("Authorization", token);
                req.ContentType = "application/json;charset=UTF-8";
                #endregion

                #region Запись данных в запрос
                using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }
                #endregion

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                return resp.StatusCode;
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;
                return resp.StatusCode;
            }
        }

        public static HttpWebResponse getHttpResp(string url, string token = "")
        {
            #region Создание и настройка request'а
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            if (token != "")
                req.Headers.Add("Authorization", token);
            #endregion

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            return resp;
        }

        public static string getHttp(string url, string token = "")
        {
            #region Создание и настройка request'а
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            if (token != "")
                req.Headers.Add("Authorization", token);
            #endregion

            #region Считывание response
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string sLine;
            #endregion

            sLine = sr.ReadLine();
            resp.Close();
            sr.Close();
            return sLine;
        }
    }
}
