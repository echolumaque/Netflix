using System;
using System.Collections.Generic;
using System.Text;
using Prism.Commands;
using SQLite;

namespace Netflix.Models
{
    public class MyListModel
    {
        public string Title { get; set; }
        public string Image { get; set; }
        public string Casts { get; set; }
        public string Synopsis { get; set; }
        [Ignore]
        public DelegateCommand<MovieModel> ShowInfo { get; set; }
    }
}
