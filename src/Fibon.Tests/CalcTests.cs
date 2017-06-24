using System;
using Fibon.Service;
using Xunit;

namespace Fibon.Tests
{
    public class CalcTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(3, 2)]
        [InlineData(4, 3)]
        [InlineData(5, 5)]
        public void DoWork(int number, int result)
        {
            var calc = new SlowOne();
            var actual = calc.DoYourJob(number);
            Assert.Equal(result, actual);
        }
    }
}
