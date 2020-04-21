using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;
namespace  Ipro.Platform.CodingTest.App
{
    class Resource
    {
        private string _rName;
        private Dictionary<string, long> _rWordOccurences = new Dictionary<string, long>();
        public Resource(string name)
        {
            SetResrouceName(name);
            PopulateDictionary();
        }
        public long GetTermOccurences(string searchTerm)
        {
            string key = searchTerm.ToLower();
            return  GetOccurences(key);
        }
        public string GetResourceName()
        {
            return _rName;
        }
        private void SetResrouceName(string name)
        {
            _rName = name;
        }
        private void PopulateDictionary()
        {
            ProcessStringList(GetResourceTextToStringList());
        }
        private long GetOccurences(string key)
        {
            if (_rWordOccurences.ContainsKey(key))
                return _rWordOccurences[key];
            else
                return 0;
        }
        private List<string> GetResourceTextToStringList()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resource = _rName;
            List<string> text = new List<string>();
            using (Stream stream = assembly.GetManifestResourceStream(resource))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (reader.Peek() > -1)
                {
                    text.Add(reader.ReadLine());
                }
            }
            return text;
        }
        private void ProcessStringList(List<string> stringList)
        {
            foreach(string s in stringList)
            {
                string modifiedString = s.ToLower();
                RegExRemove("[^a-z0-9 ]", ref modifiedString,"");
                AddToWordDictionary(modifiedString);
            }
        }
        private void RegExRemove(string regPattern,ref string text, string replaceValue)
        {
            string modifiedText = text;
            Regex reg_exp = new Regex(regPattern);
            modifiedText = reg_exp.Replace(modifiedText,replaceValue);
            text = modifiedText;
        }
        private void AddToWordDictionary(string text)
        {
            Dictionary<string,long> wordDictionary = _rWordOccurences;
            string[] words = text.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
            foreach(string word in words)
            {
                if(!wordDictionary.ContainsKey(word))
                    wordDictionary.Add(word,1);
                else
                    wordDictionary[word] += 1;
            }
            _rWordOccurences = wordDictionary;
        }
    }
}