using System.Collections.Generic;
using System.Linq;

namespace AsyncRobot.Core
{
    public class Land
    {
        public List<LandPosition> Map = new List<LandPosition>();

        /// <summary>
        /// Draw full land with "wall"
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Land(int width, int height)
        {
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

                this.Map.Add(new LandPosition(x,y,LandObject.WALL));

                x++;
            }
        }

        /// <summary>
        /// Replace a position with other value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="landObject"></param>
        public void SetPosition(int x, int y, LandObject landObject)
        {
            var point = Map.Where(horizontal => horizontal.X == x).Where(vertical => vertical.Y == y).First();
            point.SetValue(landObject);
        }

        /// <summary>
        /// Read position value
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public LandObject GetPosition(int x, int y)
        {
            return this.Map
                .Where(horizontal => horizontal.X == x)
                .Where(vertical => vertical.Y == y)
                .Select(point => point.Value).FirstOrDefault();
        }
    }
}
