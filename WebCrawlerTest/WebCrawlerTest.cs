using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCrawler;

namespace WebCrawlerTest
{
    [TestClass]
    public class WebCrawlerTest
    {
        [TestMethod]
        public void Test_WordCount_Well_Well_Returns_TopWord_Well_2()
        {
            // Arrange
            string str = "Well well";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(1, new HashSet<string>());

            // Assert
            Assert.AreEqual("well", mostFrequent[0].word);
            Assert.AreEqual(2, mostFrequent[0].frequency);
        }
        
        [TestMethod]
        public void Test_WordCount_Three_WeAposll_Period_Returns_TopWord_WeAposll_3()
        {
            // Arrange
            string str = "We'll we'll we'll.";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(1, new HashSet<string>());

            // Assert
            Assert.AreEqual("we'll", mostFrequent[0].word);
            Assert.AreEqual(3, mostFrequent[0].frequency);
        }
        
        [TestMethod]
        public void Test_WordCount_AddHello_Then_AddWorldYay_Returns_3Words1Count()
        {
            // Arrange
            string str1 = "Hello";
            string str2 = "World!Yay";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str1);
            count.AddWordsToCount(str2);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(3, new HashSet<string>());

            // Assert
            Assert.AreEqual("hello", mostFrequent[0].word);
            Assert.AreEqual(1, mostFrequent[0].frequency);
            Assert.AreEqual("world", mostFrequent[1].word);
            Assert.AreEqual(1, mostFrequent[1].frequency);
            Assert.AreEqual("yay", mostFrequent[2].word);
            Assert.AreEqual(1, mostFrequent[2].frequency);
        }
        
        [TestMethod]
        public void Test_WordCount_SpecialChars_Returns_3Words1Count()
        {
            // Arrange
            string str1 = "$ @Hello! & 22 ";
            string str2 = "World!Yay";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str1);
            count.AddWordsToCount(str2);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(3, new HashSet<string>());

            // Assert
            Assert.AreEqual("hello", mostFrequent[0].word);
            Assert.AreEqual(1, mostFrequent[0].frequency);
            Assert.AreEqual("world", mostFrequent[1].word);
            Assert.AreEqual(1, mostFrequent[1].frequency);
            Assert.AreEqual("yay", mostFrequent[2].word);
            Assert.AreEqual(1, mostFrequent[2].frequency);
        }
        
        [TestMethod]
        public void Test_WordCount_2Hello_3World_1Yay_GetTop2Words_Returns_World_Hello_InOrder()
        {
            // Arrange
            string str1 = "hello hello";
            string str2 = "world world world yay";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str1);
            count.AddWordsToCount(str2);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(2, new HashSet<string>());

            // Assert
            Assert.AreEqual(2, mostFrequent.Count);
            Assert.AreEqual("world", mostFrequent[0].word);
            Assert.AreEqual(3, mostFrequent[0].frequency);
            Assert.AreEqual("hello", mostFrequent[1].word);
            Assert.AreEqual(2, mostFrequent[1].frequency);
        }
        
        [TestMethod]
        public void Test_WordCount_2Hello_3World_1Yay_GetTop2WordsExcludeHello_Returns_World_Yay_InOrder()
        {
            // Arrange
            string str1 = "hello hello";
            string str2 = "world world world yay";

            // Act
            WordCount count = new WordCount();
            count.AddWordsToCount(str1);
            count.AddWordsToCount(str2);
            List<WordFrequency> mostFrequent = count.findMostFrequentWords(2, new HashSet<string>{"hello"});

            // Assert
            Assert.AreEqual(2, mostFrequent.Count);
            Assert.AreEqual("world", mostFrequent[0].word);
            Assert.AreEqual(3, mostFrequent[0].frequency);
            Assert.AreEqual("yay", mostFrequent[1].word);
            Assert.AreEqual(1, mostFrequent[1].frequency);
        }
    }
}
