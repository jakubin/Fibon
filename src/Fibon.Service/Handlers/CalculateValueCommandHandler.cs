using System.Threading.Tasks;
using Fibon.Messages.Commands;
using Fibon.Messages.Events;
using RawRabbit;

namespace Fibon.Service.Handlers
{
    public class CalculateValueCommandHandler : ICommandHandler<CalculateValueCommand>
    {
        private ICalculator _calculator;
        private IBusClient _busClient;

        public CalculateValueCommandHandler(ICalculator calculator, IBusClient busClient)
        {
            _calculator = calculator;
            _busClient = busClient;
        }

        public async Task HandleAsync(CalculateValueCommand command)
        {
            var result = _calculator.DoYourJob(command.Number);
            await _busClient.PublishAsync(new ValueCalculatedEvent(command.Number, result));
        }
    }
}