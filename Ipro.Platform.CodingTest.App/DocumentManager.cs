using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace Ipro.Platform.CodingTest.App
{
    class DocumentManager
    {
        private List<EmbeddedDocument> _resourceList;
        public DocumentManager()
        {
            _resourceList = new List<EmbeddedDocument>();
            PopulateEmbeddedDocumentList();
        }
        public List<TxtResult> SearchResource(string searchTerm)
        {
            List<TxtResult> results = new List<TxtResult>();
            foreach (EmbeddedDocument r in _resourceList)
            {
                    TxtResult result = new TxtResult();
                    result.Occurrences = r.GetHitCount(searchTerm);
                    result.FileName = r.GetFileName();
                    if(result.Occurrences > 0)
                        results.Add(result);
            }
            return results;
        }
        private void PopulateEmbeddedDocumentList()
        {
            
            string[] resources = GetAllTextResources();
            foreach (string resource in resources)
            {
                AddResourceToList(CreateResource(resource, @"C:\DEV\MidLevelEngineerTestApp\Ipro.Platform.CodingTest.App\bin\Debug\netcoreapp3.1\Ipro.Platform.CodingTest.App.dll"));
            }
        }
        private string[] GetAllTextResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string folder = $"{assembly.GetName().Name}.Resources";
            return assembly.GetManifestResourceNames().Where(r => r.StartsWith(folder) && r.EndsWith(".txt")).ToArray();
        }
        private EmbeddedDocument CreateResource(string resourceName,string resourcePath)
        {
            return new EmbeddedDocument(resourceName, resourcePath);
        }
        private void AddResourceToList(EmbeddedDocument resource)
        {
            _resourceList.Add(resource);
        }
    }
}
