using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;

namespace WebCrawler
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            WordCount wordCount = await startCrawlerAsync();
            
            int amountOfWordsToDisplay;

            Console.Write("Enter in the number of words to display: ");
            while(!int.TryParse(Console.ReadLine(), out amountOfWordsToDisplay)) {
                Console.Clear();
                Console.WriteLine("ERROR: Please enter a valid number.");
                Console.Write("Enter in the number of words to display: ");
            }
            Console.WriteLine("You want to display " + amountOfWordsToDisplay + " words.");
            Console.WriteLine("");
            
            HashSet<string> wordsToExclude = new HashSet<string>();
            string temp;
            bool keepAsking = true;

            while(keepAsking) {
                Console.Write("Enter a word you would like to exclude (if done, simply press enter): ");
                temp = Console.ReadLine();
                if(temp != "") {
                    wordsToExclude.Add(temp.ToLower());
                    temp = "";
                } else { 
                    keepAsking = false;
                }
            }
            
            Console.WriteLine("You would like to exclude " + wordsToExclude.Count + " words.");
            Console.WriteLine("");

            List<WordFrequency> topWords = wordCount.findMostFrequentWords(amountOfWordsToDisplay, wordsToExclude);

            Console.WriteLine("Top " + amountOfWordsToDisplay + " Most Frequent Words:");
            Console.WriteLine("------------------------------");
            for(int i = 0; i < topWords.Count; i++) {
                Console.WriteLine(topWords[i].word.ToUpper() + " : " + topWords[i].frequency);
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine("Press enter when done.");
            Console.ReadLine();
        }

        private static async Task<WordCount> startCrawlerAsync() {
            string url = "https://en.wikipedia.org/wiki/Microsoft";

            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            List<HtmlNode> body = htmlDoc.DocumentNode.Descendants("div")
                .Where(node => node.GetAttributeValue("class", "").Equals("mw-parser-output")).ToList();
            List<HtmlNode> allNodes = body[0].Descendants().ToList();
            List<HtmlNode> titles = allNodes.Where(node => node.Name == "h2").ToList();
            bool crawlerActive = false;
            WordCount wordCount = new WordCount();

            foreach(HtmlNode node in allNodes) {
                if(node.InnerText == "History" && node.Name == "h2") { crawlerActive = true; }
                if(node.InnerText == "Corporate affairs" && node.Name == "h2") { crawlerActive = false; }
                if(crawlerActive) { wordCount.AddWordsToCount(node.InnerText); }
            }

            return wordCount;
        }
    }
}
