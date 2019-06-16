using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using calculatorstatemachine;

namespace calculatorstatemachine.Tests
{
    public class FunctionTest
    {
        public FunctionTest()
        {
        }

        [Fact]
        public void TestGreeting()
        {
            TestLambdaContext context = new TestLambdaContext();

            StepFunctionTasks functions = new StepFunctionTasks();

            var state = new State
            {
                Input1 = "4",
                Input2 = "3"
            };

            state = functions.Calculation(state, context);

            Assert.Equal(3, state.WaitInSeconds);
            Assert.Equal("7", state.Result);
        }
    }
}
