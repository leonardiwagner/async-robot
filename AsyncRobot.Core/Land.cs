using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core {
    public class Land {
        public IList<LandPosition> Map { get; private set; }

        public Land(int width, int height) {
            this.Map = new List<LandPosition>();
            int area = width * height;
            int y = 0;
            int x = 0;

            for (int i = 0; i < area; i++) {
                if (x == height) {
                    x = 0;
                    y++;
                }

                this.Map.Add(new LandPosition(x, y));
                x++;
            }
        }

        public void SetPositionType(int x, int y, LandPositionType positionType) {
            var point = Map.Single(position => position.X == x &&
                                               position.Y == y);
            point.PositionType = positionType;
        }

        public LandPositionType GetPositionType(LandPosition landPosition)
        {
            return GetPositionType(landPosition.X, landPosition.Y);
        }

        public LandPositionType GetPositionType(int x, int y) {
            var landPosition =  this.Map.SingleOrDefault(position => position.X == x &&
                                                                     position.Y == y);
            if (landPosition != null) {
                return landPosition.PositionType;
            }
            else {
                return LandPositionType.OUT_OF_LIMITS;
            }
        }
    }
}
