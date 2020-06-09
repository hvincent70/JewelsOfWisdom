using OpenQA.Selenium;
using System;

namespace JewelsOfWisdom
{
    /// <summary>
    /// This class is written as a singleton for no other reason 
    /// than to practice working with singletons
    /// </summary>

    public class MathTrainerPage
    {
        /// <summary>
        /// Holder allows for extreme laziness.  Like, mind-blowingly lazy laziness
        /// </summary>
        private static class MathTrainerPageHolder {
            internal static readonly MathTrainerPage instance = new MathTrainerPage();

            static MathTrainerPageHolder() { }
        }

        private MathTrainerPage() {
            Console.WriteLine("MathTrainerPage constructed");
        }

        public static MathTrainerPage Instance { get { return MathTrainerPageHolder.instance; } }
        public static By Start { get; } = By.CssSelector("#start");
        public static By OperandA { get; } = By.CssSelector("#a");
        public static By OperandB { get; } = By.CssSelector("#b");
        public static By Operator { get; } = By.CssSelector("#operator");
        /// <summary>
        /// This constant left nonstatic so that a singleton instance can grab it.
        /// </summary>
        public By Input { get; } = By.CssSelector("#input");
    }
}