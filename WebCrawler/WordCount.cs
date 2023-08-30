using System;
using System.Collections.Generic;

namespace WebCrawler {

    public class WordCount {
        Dictionary<string,int> count;
        public static HashSet<char> wordChars = new HashSet<char>(new char[] 
            {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z','\''});

        public WordCount() {
            count = new Dictionary<string, int>();
        }

        public void AddWordsToCount(string str) {
            string lowerStr = str.ToLower();
            string word = "";
            
            for(int i = 0; i < lowerStr.Length; i++) {
                if(!wordChars.Contains(lowerStr[i])) {
                    if(word != "") {
                        addOrIncrementCount(word);
                        word = "";
                    }
                }
                else {
                    word += lowerStr[i];
                }
            }
            if(word != "") {
                addOrIncrementCount(word);
                word = "";
            }
        }

        private void addOrIncrementCount(string word) {
            if(count.ContainsKey(word)) { count[word]++; }
            else { count[word] = 1; }
        }

        public List<WordFrequency> findMostFrequentWords(int topXWords, HashSet<string> excludedWords) {
            List<WordFrequency> ret = new List<WordFrequency>();
            WordFrequency temp;


            foreach(KeyValuePair<string,int> kvPair in count) {
                if(excludedWords.Contains(kvPair.Key)) { continue; }
                temp = new WordFrequency(kvPair.Key, kvPair.Value);
                if(ret.Count < topXWords) { addInOrder(ret, temp); }
                else if(temp.frequency > ret[ret.Count - 1].frequency) {
                    ret.RemoveAt(ret.Count - 1);
                    addInOrder(ret, temp);
                }
            }

            return ret;
        }

        private void addInOrder(List<WordFrequency> list, WordFrequency word) {
            int index = 0;

            while(index < list.Count && word.frequency <= list[index].frequency) {
                index++;
            }

            list.Insert(index, word);
        }
    }

    public class WordFrequency {
        public string word;
        public int frequency;

        public WordFrequency(string word, int frequency) {
            this.word = word;
            this.frequency = frequency;
        }
    }
}