using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace JewelsOfWisdom
{
    public class JewelsOfWisdomLogic: PageActions
    {
        const string URL = "https://www.cram.com/flashcards/games/jewel/istqb-study-6496542";

        public void TestRun()
        {
            BeginGame();
            WaitForPageReady();

            //Method organizes jewels from top left to bottom right; maximizes speed            
            GetCardOrder().ForEach(i => AnswerQuestionNoWait(i));

            //Method for selecting highest value jewel
            //var jewels = RetryElementsUntilPresent(CramPage.AllJewels);
            //OptimizeRun(jewels);

            //Method simply goes by jewel id
            //EasyRun();
        }

        private void AnswerQuestion(int num)
        {
            ResilientClick(CramPage.JewelWithIndex(num));
            ResilientClick(CramPage.AnswerWithIndex(num));
            WaitForPageReadyWithShifts();
        }

        private void AnswerQuestionNoWait(int num)
        {
            ResilientClick(CramPage.JewelWithIndex(num));
            ResilientClick(CramPage.AnswerWithIndex(num));
        }

        private void BeginGame()
        {
            GoToURL(URL);
            Console.WriteLine("Clean up screen");
            Console.ReadLine();
            ResilientClick(CramPage.PlayGame);
        }

        private void OptimizeRun(IList<IWebElement> jewels)
        {
            for (int i = 0; i < 15; i++)
            {
                var gameState = GetGameState(jewels);
                int optimalChoice = FindOptimalChoice(gameState);
                AnswerQuestion(optimalChoice);
            }
        }

        private IEnumerable<Jewel> GetGameState(IList<IWebElement> jewels) => jewels
            .Select(jewel => new Jewel(jewel.Location, GetJewelColor(jewel), IDNumberFromWebElement(jewel)));

        private int IDNumberFromWebElement(IWebElement web) => System.Convert.ToInt32(web.GetAttribute("id").Replace("col1card", ""));

        //private void PrintGameState(List<IWebElement> gameState)
        //{
        //    for (int i = 0; i < gameState.ForEach; i++)
        //    {
        //        for (int j = 0; j < gameState.GetLength(1); j++)
        //        {
        //            Console.Write(gameState[i, j] + "\t");
        //        }
        //        Console.WriteLine();
        //    }
        //}

        private string GetJewelColor(IWebElement jewel) => jewel.FindElement(CramPage.JewelImage).GetAttribute("src")
            .Replace("https://www.cram.com/fce/images/jewel/jewel-", "").Replace(".png", "");

        private int FindOptimalChoice(IEnumerable<Jewel> gameState)
        {
            int maxNumberCollected = 0;
            foreach(Jewel jewel in gameState)
            {
                List<Jewel> list = new List<Jewel>() { jewel };
                maxNumberCollected = NumberOfCollectedJewelsFromSelection(gameState, list);
            }
            return maxNumberCollected;
        }

        private int NumberOfCollectedJewelsFromSelection(IEnumerable<Jewel> gameState, List<Jewel> list)
        {
            var newGameState = DropEachJewelAboveSelected(gameState, list);
            List<Jewel> matchingJewels = ListOfJewelsRemoved(newGameState);
            return 0;
        }

        private List<Jewel> ListOfJewelsRemoved(List<Jewel> newGameState)
        {
            throw new NotImplementedException();
        }

        private List<Jewel> DropEachJewelAboveSelected(IEnumerable<Jewel> gameState, List<Jewel> list)
        {
            var alteredGameState = gameState.ToList();
            foreach (Jewel jewel in list){               
                var point = jewel.Location;
                alteredGameState.Remove(jewel);
                alteredGameState.Where(j => j.Location.X == jewel.Location.X && j.Location.Y < jewel.Location.Y)
                    .ToList()
                    .ForEach(j => j.Location = new Point(j.Location.X, j.Location.Y + 65));
            }
            return alteredGameState;
        }

        private void EasyRun()
        {
            var nums = new List<int>(Enumerable.Range(0, 15));
            nums.ForEach(i => AnswerQuestion(i));
        }

        protected List<int> GetCardOrder()
        {
            var jewels = RetryElementsUntilPresent(CramPage.AllJewels);
            jewels.OrderBy(web => web.Location.Y);
            return GetIDNumbers(jewels);
        }

        private List<int> GetIDNumbers(IList<IWebElement> jewels) => new List<IWebElement>(jewels).Select(jewel => IDNumberFromWebElement(jewel))
            .Reverse().ToList();
    }
}