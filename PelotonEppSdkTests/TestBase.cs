using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PelotonEppSdkTests
{
    public class TestBase
    {
        protected TestBase()
        {
            baseUri = new Uri("https://testapi.peloton-technologies.com");
            //baseUri = new Uri("http://localhost:2590/");
        }
        protected static Uri baseUri;
    }
}
