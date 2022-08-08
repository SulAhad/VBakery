using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VBakery.MenuDB
{
    public class FirstCourse
    {
        internal static List<FirstCourse> ItemsSource;

        public int Id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
    }
}
