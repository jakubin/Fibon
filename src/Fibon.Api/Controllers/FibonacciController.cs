using System.Threading.Tasks;
using Fibon.Api.Repository;
using Fibon.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Fibon.Api.Controllers
{
    [Route("[controller]")]
    public class FibonacciController: Controller
    {
        private readonly IBusClient _busClient;
        private readonly IRepository _repository;

        public FibonacciController(IBusClient busClient, IRepository repository)
        {
            _busClient = busClient;
            _repository = repository;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            int? inCache = _repository.Get(number);

            return inCache != null
                ? Content(inCache.ToString())
                : (IActionResult)NotFound("Dupa");
        }

        [HttpPost("{number}")]
        public async Task<IActionResult> Post(int number)
        {
            int? inCache = _repository.Get(number);
            if (inCache == null)
            {
                await _busClient.PublishAsync(new CalculateValueCommand(number));
            }

            return Accepted($"fibonacci/{number}");
        }
    }
}