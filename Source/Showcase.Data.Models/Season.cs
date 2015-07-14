using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Showcase.Data.Models
{
    public class Season
    {
        public Season()
        {
            this.Projects = new HashSet<Project>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
