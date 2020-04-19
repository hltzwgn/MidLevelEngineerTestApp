using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace Ipro.Platform.CodingTest.App
{
    class ResourceSearchManager
    {
        private List<Resource> _resourceList = new List<Resource>();
        public ResourceSearchManager()
        {
            PopulateResourceList();
        }
        public List<TxtResult> SearchResource(string searchTerm)
        {
            List<TxtResult> results = new List<TxtResult>();
            foreach(Resource r in _resourceList)
            {
                TxtResult result = new TxtResult();
                result.Occurrences = r.GetTermOccurences(searchTerm);
                result.FileName = r.GetResourceName();
                results.Add(result);
            }
            return results;
        }
        private void PopulateResourceList()
        {
            string[] resources = GetTextResources();
            foreach(string resource in resources)
            {
                AddResourceToList(CreateResource(resource));
            }
        }
        private string[] GetTextResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string folder = $"{assembly.GetName().Name}.Resources";
            return assembly.GetManifestResourceNames().Where(r => r.StartsWith(folder) && r.EndsWith(".txt")).ToArray();
        }
        private Resource CreateResource(string resourceName)
        {
            return new Resource(resourceName);
        }
        private void AddResourceToList(Resource resource)
        {
            _resourceList.Add(resource);
        }
    }
}
