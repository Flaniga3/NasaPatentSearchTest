using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleProject.Data.Daos;
using ExampleProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExampleProject.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NasaPatentsController : Controller
    {
        // NASA's API currently only serves up 154 unique patents at a time. 
        // I wouldn't normally grab all the data I could, but this is a pretty
        // safe sized set. Seeing as I didn't design their APIs myself, this is
        // better than trying to implement server-side paging wrapping one of
        // the few NASA APIs that doesn't offer the feature itself.
        private int resultSetSizeLimit = 155;

        [HttpGet]
        public IEnumerable<NasaPatentModel> Get([FromQuery] string query)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "limit", resultSetSizeLimit.ToString() }
            };

            // Ultimately this project is about searching and filtering a set
            // of data from an API. For this purpose, the NASA API I chose is
            // lacking several important features: it doesn't not support 
            // multi-word queries, it has a fixed limit on the size of the
            // result set, and it doesn't offer any sort of paging or sorting.
            // I've chosen to simply search, sort, and filter on several key
            // fields here in the "wrapper" API.
            IEnumerable<NasaPatentModel> patents = new NasaPatentDao().Get(parameters);

            // filter and sort the top 155 results:
            // Keep it simple and just filter by category, abstract, and title
            IEnumerable<NasaPatentModel> filteredSet = null;
            if (patents != null && !String.IsNullOrEmpty(query))
            {
                filteredSet = patents
                    .Where(x => (!String.IsNullOrEmpty(x.Abstract) && x.Abstract.Contains(query))
                                || (!String.IsNullOrEmpty(x.Title) && x.Title.Contains(query))
                                || (!String.IsNullOrEmpty(x.Category) && x.Category.Contains(query)))
                    .OrderBy(x => x.Title);
            }

            return filteredSet;
        }
    }
}
