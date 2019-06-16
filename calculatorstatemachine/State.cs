using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorstatemachine
{
    /// <summary>
    /// The state passed between the step function executions.
    /// </summary>
    public class State
    {

        public string Input1 { get; set; }
        public string Input2 { get; set; }
        public string Result { get; set; }

        public string Message { get; set; }

        public int WaitInSeconds { get; set; }

        public bool IsValid { get; set; }
    }
}
