using System;
using System.Threading;
using OpenQA.Selenium;

namespace JewelsOfWisdom
{
    public class MathTrainerLogic : PageActions
    {
        const string URL = "https://www.mathtrainer.org/";
        const int TIME_TO_WAIT_BEFORE_RUN = 8000;
        const int TIME_TO_WAIT_BETWEEN_PROBLEMS = 250;

        internal void Run()
        {
            GoToURL(URL);
            while (1 == 1)
            {
                Console.WriteLine("Starting new run");
                Thread.Sleep(TIME_TO_WAIT_BEFORE_RUN);
                ResilientClick(MathTrainerPage.Start);
                Thread.Sleep(TIME_TO_WAIT_BETWEEN_PROBLEMS);
                while (QuickGrab(MathTrainerPage.OperandA) != null)
                {
                    var problem = new Problem(
                        RetryElementUntilPresent(MathTrainerPage.OperandA).Text,
                        RetryElementUntilPresent(MathTrainerPage.OperandB).Text,
                        RetryElementUntilPresent(MathTrainerPage.Operator).Text);

                    ///This instance calls the MathTrainerPage constructor, giving 
                    ///console output.  Because MathTrainerPage is a singleton, 
                    ///console output occures once and once only.
                  
                    var instance = MathTrainerPage.Instance;
                    SendKeys(problem.Answer, instance.Input);
                }
            }
        }


    }
}