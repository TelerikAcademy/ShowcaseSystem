namespace Showcase.Server.DataTransferModels.Statistics
{
    using System.Collections.Generic;

    public class LineChartResponseModel
    {
        public LineChartResponseModel()
        {
            this.Labels = new List<string>();
            this.Values = new List<int>();
        }

        public IList<string> Labels { get; set; }

        public IList<int> Values { get; set; }
    }
}