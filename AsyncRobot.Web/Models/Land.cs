using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AsyncRobot.Web.Models {
    public class Land {
        public int width { get; set; }
        public int height { get; set; }
        public List<Position> track { get; set; }
    }
}