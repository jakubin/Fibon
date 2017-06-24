using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Fibon.Api.Controllers
{
    [Route("[controller]")]
    public class FibonacciController: Controller
    {
        private IBusClient _busClient;

        public FibonacciController(IBusClient busClient)
        {
            _busClient = busClient;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            return Content(number.ToString());
        }

        [HttpPost("{number}")]
        public async Task<IActionResult> Post(int number)
        {
            return Accepted($"fibonacci/{number}");
        }
    }
}