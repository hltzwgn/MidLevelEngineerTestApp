namespace Ipro.Platform.CodingTest.App
{
    public class TxtResult : Interfaces.ISearchResult
    {
        public string FileName { get; set; }
        public long Occurrences { get; set; }
        public string GetShortName()
        {
            string[] filename = FileName.Split(".");
            return filename[filename.Length - 2];
        }
    }
}
