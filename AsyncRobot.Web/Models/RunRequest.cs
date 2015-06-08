using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncRobot.Web.Models
{
    public class RunRequest
    {
        public Land land { get; set; }
        public List<Position> robots { get; set; }
        public string approach { get; set; }
        public int threadCount { get; set; }
    }
}