using System;
using System.Collections.Generic;
using System.Text;

namespace Ipro.Platform.CodingTest.App
{
    abstract class Document
    {
        protected string _fileName, _path;
        protected Dictionary<string, int> _uniqueWordOccurences;

        public string GetFileName()
        {
            return _fileName;
        }
        public string GetFilePath()
        {
            return _path;
        }

        protected abstract void ProcessDocument();
        public abstract int GetHitCount(string key);

        protected abstract void SetUniqueWordDictionary(Dictionary<string,int> uniquewordDictionary);


    }
}
