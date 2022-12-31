using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace QMS_BenhVien
{
    public class APIClient
    {
        static object key = new object();
        private static volatile APIClient _Instance;
        public static APIClient Instance
        {
            get
            {
                if (_Instance == null)
                    lock (key)
                        _Instance = new APIClient();
                return _Instance;
            }
        }

        private APIClient() { }

        public HttpClient InitAPI(string apiAddress)
        {
            HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://www.api.benhvienranghammat.vn:6633/api/");
            client.BaseAddress = new Uri("http://"+apiAddress+"/api/"); 
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
