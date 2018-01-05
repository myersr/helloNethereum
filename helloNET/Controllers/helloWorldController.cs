using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class helloWorldController : Controller
    {
        //GET: /Helloworld/

        public string Index()
        {
            return "This is my default action...";
        }

        //GET: /helloworld/welcome/
        public string Welcome()
        {
            return "This is the welcome action method";
        }
    }
}