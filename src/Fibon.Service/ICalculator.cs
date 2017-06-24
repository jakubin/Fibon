namespace Fibon.Service
{
    public interface ICalculator
    {
        int DoYourJob(int number);
    }

    public class SlowOne : ICalculator
    {
        public int DoYourJob(int number)
        {
            if (number == 0) return 0;
            if (number == 1) return 1;
            return DoYourJob(number - 2) + DoYourJob(number - 1);
        }
    }
}