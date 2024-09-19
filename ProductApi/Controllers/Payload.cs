using Microsoft.AspNetCore.Mvc;

namespace ProductApi.Controllers
{
    public class Payload : Controller
    {
        public int quantity { set; get; }
    }
}
