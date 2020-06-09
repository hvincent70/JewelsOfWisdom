using System;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace JewelsOfWisdom
{
    static class CramPage
    {
        public const string CardShifterNeutral = "top: 0px;";
        public const string CardFallerNeutral = "top: -420px;";
        public const string MustacheNuetral = "transform: rotate(0deg);";

        /// <summary>
        /// static By object
        /// </summary>
        public static By CardShifter { get; } = By.CssSelector("#cardShifter");
        public static By CardFaller { get; } = By.CssSelector("#cardFaller");
        public static By PlayGame { get; } = By.CssSelector("#menuButtonStartGame");
        public static By HeadIdle { get; } = By.CssSelector(".headFrame.idle");
        public static By AllJewels { get; } = By.CssSelector("[id^=col1card]");
        public static By Mustache { get; } = By.CssSelector("#headMustache");
        public static By JewelImage { get; } = By.CssSelector(".jewelImage");

        /// <summary>
        /// Dynamic methods
        /// </summary>
        /// <param name="num">Number that appears in desired ID of WebElement</param>
        /// <returns>By object designated by number</returns>
        public static By JewelWithIndex(int num) => By.CssSelector($"#col1card{num}");
        public static By AnswerWithIndex(int num) => By.CssSelector($"#col2card{num}");
    }
}