namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Collections.Generic;

    public class LineChartResponseModel
    {
        public IList<string> Labels { get; set; }

        public IList<int> Values { get; set; }
    }
}