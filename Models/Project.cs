using System;
using ProjectManager.Data;

namespace ProjectManager.Models
{
    public class Project:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
