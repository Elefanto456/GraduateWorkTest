using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraduateWorkTest
{
    public class WatsonTest
    {
        private static string url = "";
        private static RestClient client = new RestClient(url);
        private static RestRequest request = new RestRequest(Method.POST);
    }
}
