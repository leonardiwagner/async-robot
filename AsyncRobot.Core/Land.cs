using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRobot.Core
{
    public class Land
    {
        public List<MapPosition> mapList = new List<MapPosition>();
        public MapPosition Robot { get; set; }

        private int y = 0;

        public StringBuilder PrintLand()
        {
            var returnString = new StringBuilder();
            var ordenedMap = mapList.OrderBy(vertical => vertical.y).ThenBy(horizontal => horizontal.x).ToList();

            int lastY = 0;
            foreach(var mapItem in ordenedMap)
            {
                if (lastY != mapItem.y)
                {
                    returnString.Append("\n");
                    lastY = mapItem.y;
                }

                returnString.Append(mapItem.value);
            }

            return returnString;
        }

        public char Point(int x, int y)
        {
            return mapList
                .Where(horizontal => horizontal.x == x)
                .Where(vertical => vertical.y == y)
                .Select(point => point.value).FirstOrDefault();
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
                if (items[i] == 'R')
                { 
                    Robot = new MapPosition(i, y, 'R');
                    items[i] = ' ';
                }

                this.mapList.Add(new MapPosition(i, y, items[i]));

                
            }
            
            y++;
        }
    }
}
