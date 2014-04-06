using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApplication
{
    public class MapPosition
    {
        public int x { get; private set; }
        public int y { get; private set; }
        public char value { get; private set; }

        public MapPosition(int x, int y, char value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
        
    }

    public class Land
    {
        private List<MapPosition> mapList = new List<MapPosition>();
        public MapPosition RobotStartPosition { get; set; }
        private int y = 0;

        public char Point(int x, int y)
        {
            return mapList
                    .Where(horizontal => horizontal.x == x)
                    .Where(vertical => vertical.y == y)
                    .Select(point => point.value).First();
        }

        public Land()
        {
            AddLine("##############E#############################E####");
            AddLine("############## ##############          ##### ####");
            AddLine("############## ############## ### ########## ####");
            AddLine("############## ############## ### ########## ####");
            AddLine("##                            ### ########## ####");
            AddLine("## #################### ######### ########   ####");
            AddLine("## ######### ########## ######### ######## ######");
            AddLine("## ######### ########## ################## ######");
            AddLine("##                 #### ############       ######");
            AddLine("## ############### ################# ## #########");
            AddLine("##         ####### ################# ## #########");
            AddLine("########## #######     #####         ## #########");
            AddLine("########## ################# ##########      ####");
            AddLine("########## ################# ############### ####");
            AddLine("##########                   ############### ####");
            AddLine("########## #### ############ ############### ####");
            AddLine("##         #### ############                 ####");
            AddLine("## ############ ################# ########## ####");
            AddLine("## ############           ####### ########## ####");
            AddLine("##R############################## ###############");

            
        }

        public void AddLine(String line)
        {
            char[] items = line.ToCharArray();
            for (int i = 0; i < items.Length; i++)
            {
                this.mapList.Add(new MapPosition(i, y));

                if(items[i] == 'R')
                    RobotStartPosition = new MapPosition(items[i],y);
            }
            y++;
        }
    }
}
