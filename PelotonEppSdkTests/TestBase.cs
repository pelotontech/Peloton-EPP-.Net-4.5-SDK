using System;

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
