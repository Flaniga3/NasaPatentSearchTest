using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExampleProject.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExampleProject.Data.Daos
{
    public class NasaPatentDao : NasaBaseDao<NasaPatentModel>
    {
        // This is up here for discoverability, even though it's only used by
        // the constructor for the base class to generate the URL.
        private static readonly string PartialPath = "patents/content";

        // Calls the base constructor with the partial path to generate the URL
        public NasaPatentDao() : base(PartialPath)
        {
            
        }
    }
}
