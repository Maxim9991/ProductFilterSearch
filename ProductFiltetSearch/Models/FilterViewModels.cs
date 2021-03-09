using System;
using System.Collections.Generic;
using System.Text;

namespace BlogForm.Models
{
    public class FilterValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; } = false;
        public override string ToString()
        {
            return Name.ToString();
        }
    }
    public class FilterNameModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCollapsed { get; set; } = true;
        public List<FilterValueModel> Children { get; set; }
    }

}
