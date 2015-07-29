namespace Showcase.Server.DataTransferModels.Statistics
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    using Showcase.Data.Models;
    
    public class CountByDateModel
    {
        public static Expression<Func<IGrouping<int, Project>, CountByDateModel>> FromProjectGrouping
        {
            get
            {
                return gr => new CountByDateModel
                {
                    Date = gr.FirstOrDefault().CreatedOn,
                    Count = gr.Count()
                };
            }
        }

        public DateTime Date { get; set; }

        public int Count { get; set; }
    }
}