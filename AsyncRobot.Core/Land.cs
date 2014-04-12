using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class Land
    {
        public List<LandPosition> map = new List<LandPosition>();
        public LandPosition Robot { get; set; }

        private readonly int width;
        private readonly int height;

        public Land(int width, int height)
        {
            this.width = width;
            this.height = height;

            int area = width*height;
            int y = 0;
            int x = 0;
            for (int i = 0; i < area; i++)
            {
                if (x == height)
                {
                    x = 0;
                    y++;
                }

                map.Add(new LandPosition(x,y,'#'));

                x++;
            }
        }

        public void AddTrackPoint(int x, int y)
        {
            var point = map.Where(horizontal => horizontal.x == x).Where(vertical => vertical.y == y).First();
            point.SetValue(' ');
        }

        public List<LandPosition> Read()
        {
            return map.OrderBy(vertical => vertical.y).ThenBy(horizontal => horizontal.x).ToList();
        }

        public char Point(int x, int y)
        {
            return map
                .Where(horizontal => horizontal.x == x)
                .Where(vertical => vertical.y == y)
                .Select(point => point.value).FirstOrDefault();
        }

        

        
    }
}
