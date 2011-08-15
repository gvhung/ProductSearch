﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using ProductSearch.Model;
using ProductSearch.Utility;

/*
 * Parts obtained from Remix.NET
 * http://code.google.com/p/remixdotnet/
 */

namespace ProductSearch.DataAccess.Repository
{
    public class BestBuyProductRepository : IProductSearchRepository
    {
        private static readonly Logger Log = new Logger(typeof(BestBuyProductRepository));
        private const string SearchUrl = "http://api.remix.bestbuy.com/v1/products(name='{0}*')?apiKey=x4p9sbyznrgjnxwdadqz25qe";

        public ProductSearchResult Search(string criteria)
        {
            var results = GetProduct(criteria);

            return new ProductSearchResult(false, false, results.FirstOrDefault());
        }

        private static string GetOutputFromUrl(string url)
        {
            try
            {
                // Create the web request   
                var request = WebRequest.Create(url) as HttpWebRequest;

                // HACK: Fixes "417 Expectation Failed".
                ServicePointManager.Expect100Continue = false;

                // Get response   
                var response = request.GetResponse() as HttpWebResponse;

                // check return codes
                //200 OK: everything went awesome.
                if (response.StatusCode == HttpStatusCode.OK)
                    return new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8).ReadToEnd();
                else
                {
                    Log.Warn(string.Format("Get - Bad HttpStatusCode: {0}", response.StatusCode));
                    return null;
                }
            }
            catch (WebException we)
            {
                Log.Error("Get - Error.", we);
                return null;
            }
            catch (Exception e)
            {
                Log.Error("Get - Error.", e);
                return null;
            }
        }
        
        private static IEnumerable<Product> GetProduct(string productName)
        {
            var searchUrl = string.Format(SearchUrl, productName);

            var output = GetOutputFromUrl(searchUrl);

            var p = Utf8XmlSerializer.Deserialize<Products>(output);

            return p;
        }

    }
}
