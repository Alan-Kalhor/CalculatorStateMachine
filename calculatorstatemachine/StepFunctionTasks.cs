using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace calculatorstatemachine
{
    public class StepFunctionTasks
    {
        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public StepFunctionTasks()
        {
        }

        public State Initial(State state, ILambdaContext context)
        {
            LogMessage(context, state.ToString());


            //state.IsMale = state.Name.StartsWith("Mr") ? 1 : 0;


            // Tell Step Function to wait 5 seconds before calling 
            //state.WaitInSeconds = 5;

            return state;
        }

        public State Validation(State state, ILambdaContext context)
        {
            state.IsValid = true;
            if (string.IsNullOrEmpty(state.Input1) || !int.TryParse(state.Input1, out var input1Val) || string.IsNullOrEmpty(state.Input2) || !int.TryParse(state.Input2, out var input2Val))
            {
                state.IsValid = false;
                LogMessage(context, "Invalid input!");
            }

            state.WaitInSeconds = 3;

            return state;
        }

        public State Pass(State state, ILambdaContext context)
        {
            return state;
        }

        public State Calculation(State state, ILambdaContext context)
        {

            var input1Val = 0;
            var input2Val = 0;

            int.TryParse(state.Input1, out input1Val);
            int.TryParse(state.Input2, out input2Val);

            //state.Message = $"Calculatig adding {input1Val} and {input2Val}";
            LogMessage(context, "Calculatig the result...");

            state.Result = (input1Val + input2Val).ToString();

            // Tell Step Function to wait 3 seconds before calling 
            state.WaitInSeconds = 3;

            return state;
        }

        public State PrintResult(State state, ILambdaContext context)
        {
            state.Message = "The result of calculation is " + state.Result;

            LogMessage(context, "Printing the result..." + state.Result);

            return state;
        }

        public State PrintError(State state, ILambdaContext context)
        {
            state.Message = "Validation error!";

            LogMessage(context, "Printing the error...");

            return state;
        }

        void LogMessage(ILambdaContext ctx, string msg)
        {
            ctx.Logger.LogLine(
                string.Format("{0}:{1} - {2}",
                    ctx.AwsRequestId,
                    ctx.FunctionName,
                    msg));
        }
    }
}
