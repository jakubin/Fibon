namespace Fibon.Messages.Commands
{
    public class CalculateValueCommand : ICommand
    {
        public int Number { get; }

        protected CalculateValueCommand()
        {
        }

        public CalculateValueCommand(int number)
        {
            Number = number;
        }
    }
}