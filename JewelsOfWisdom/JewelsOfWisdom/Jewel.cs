using System.Drawing;
using OpenQA.Selenium;

namespace JewelsOfWisdom
{
    public class Jewel
    {
        private Point location;
        private string color;
        private int id;
        private IWebElement jewel;

        public Point Location { get; set; }
        public string Color { get; }
        public int ID { get; }

        public Jewel(Point location, string color, int ID)
        {
            this.location = location;
            this.color = color;
            this.id = ID;
        }

        public Jewel(IWebElement jewel)
        {
            this.location = jewel.Location;
            this.color = GetJewelColor(jewel);
            this.id = IDNumberFromWebElement(jewel);
        }

        private int IDNumberFromWebElement(IWebElement web) => System.Convert.ToInt32(web.GetAttribute("id").Replace("col1card", ""));

        private string GetJewelColor(IWebElement jewel) => jewel.FindElement(CramPage.JewelImage).GetAttribute("src")
            .Replace("https://www.cram.com/fce/images/jewel/jewel-", "").Replace(".png", "");
    }
}