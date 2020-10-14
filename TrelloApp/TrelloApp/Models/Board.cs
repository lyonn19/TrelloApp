using System;
using System.Collections.Generic;
using System.Text;

namespace TrelloApp.Models
{
    public class Board
    {
        public string id { get; set; }
        public string name { get; set; }
        public bool closed { get; set; }
        public int pos { get; set; }
        public object softLimit { get; set; }
        public string idBoard { get; set; }
        public bool subscribed { get; set; }
    }
}
