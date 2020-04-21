using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System;


namespace Ipro.Platform.CodingTest.App
{
    class EmbeddedDocument : Document
    {
        private string _fileType;

        public EmbeddedDocument(string filename, string path)
        {
            
            _fileName = filename;
            _path = path;
            _fileType = Path.GetExtension(filename).ToLower();
            _uniqueWordOccurences = new Dictionary<string, int>();
            ProcessDocument();
        }

        protected override void ProcessDocument()
        {
            if(_fileType.ToLower().Equals(".txt"))
            {
                ProcessStringList(ConvertTextResourceToStringList(GetEmbeddedResourceAssembly()));
            }
        }

        public override int GetHitCount(string key)
        {
            if (_uniqueWordOccurences.ContainsKey(key))
                return _uniqueWordOccurences[key];
            else
                return 0;
        }

        protected override void SetUniqueWordDictionary(Dictionary<string, int> uniquewordDictionary)
        {
            _uniqueWordOccurences = uniquewordDictionary;
        }

        private Assembly GetEmbeddedResourceAssembly()
        {
            return Assembly.LoadFile(_path);
        }
        private List<string> ConvertTextResourceToStringList(Assembly assembly)
        {
            List<string> text = new List<string>();
            using (Stream stream = assembly.GetManifestResourceStream(_fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                while (reader.Peek() > -1)
                {
                    text.Add(reader.ReadLine());
                }
            }
            return text;
        }
        private void RegExRemove(string regPattern, ref string text, string replaceValue)
        {
            string modifiedText = text;
            Regex reg_exp = new Regex(regPattern);
            modifiedText = reg_exp.Replace(modifiedText, replaceValue);
            text = modifiedText;
        }
        private void ProcessStringList(List<string> stringList)
        {
            Dictionary<string, int> workingDictionary = new Dictionary<string, int>();
            foreach (string s in stringList)
            {
                string modifiedString = s.ToLower();
                RegExRemove("[^a-z0-9 ]", ref modifiedString, "");
                AddToWordDictionary(modifiedString, ref workingDictionary);
            }
            SetUniqueWordDictionary(workingDictionary);
        }
        private void AddToWordDictionary(string text, ref Dictionary<string, int> workingDictionary)
        {
            string[] words = text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (!workingDictionary.ContainsKey(word))
                    workingDictionary.Add(word, 1);
                else
                    workingDictionary[word] += 1;
            }
        }
    }
}
