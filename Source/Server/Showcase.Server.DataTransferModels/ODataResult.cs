namespace Showcase.Server.DataTransferModels
{
    using System.Collections.Generic;

    public class ODataResult<T>
    {
        public ODataResult(IEnumerable<T> results, long? count = null)
        {
            this.Results = results;
            this.Count = count;
        }

        public IEnumerable<T> Results { get; set; }

        public long? Count { get; set; }
    }
}
