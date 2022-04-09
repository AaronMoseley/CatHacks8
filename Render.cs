using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class Render
    {
        //Take in 2d array of nodes 
        public void Input(RealMap mapNodes)
        {
            int sideLength = mapNodes.GetSideLength();
            vis_data = new int[sideLength, sideLength];

            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    vis_data[i, j] = mapNodes.GetNodeByPos(new Vector2Int(i, j)).GetChar();
                }
            }

            List<Ghost> newGhosts = mapNodes.GetGhostPositions();
            ghosts = new Vector2Int[newGhosts.Count];

            for (int i = 0; i < newGhosts.Count; i++)
            {
                ghosts[i] = newGhosts[i].GetPos();
            }

            Interpret();
        }

        /*public void build()
        {
            //int sideLength = mapNodes.GetSideLength();
            Random rand = new Random();
            //For testing purposes, first populate a data array with random integers
            for (int x = 0; x < vis_data.GetLength(0); x++)
            {
                for (int y = 0; y < vis_data.GetLength(1); y++)
                {
                    vis_data[x, y] = rand.Next(4);
                }
            }
            //interpret();
        }*/

        //Convert the data that controls the map 
        //Takes an array that contains positions of ghosts
        public void Interpret()
        {
            vis_disp = new string[vis_data.GetLength(0), vis_data.GetLength(1)];

            //Populate the standard map (Things that don't move)
            for (int x = 0; x < vis_data.GetLength(0); x++)
            {
                for (int y = 0; y < vis_data.GetLength(1); y++)
                {
                    vis_disp[x, y] = type[vis_data[x, y]];
                }
            }

            //Add the ghosts afterwards
            for (int g = 0; g < ghosts.Length; g++)
            {
                vis_disp[ghosts[g].GetX(), ghosts[g].GetY()] = type[4];
            }

            Print();
        }

        //Console.BackgroundColor = ConsoleColor.White;
        public void Print()
        {
            int sideLength = vis_disp.GetLength(0);
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int top_border = 0; top_border < sideLength + 2; top_border++)
                Console.Write(" ■");
            Console.Write("\n");
            for (int x = 0; x < vis_disp.GetLength(0); x++)
            {
                Console.Write(" ■");
                for (int y = 0; y < vis_disp.GetLength(1); y++)
                {
                    //Color handler
                    if (vis_data[x, y] == 4)
                        Console.ForegroundColor = ConsoleColor.White;

                    //Print to console
                    Console.Write(vis_disp[x, y]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(" ■\n");
            }
            for (int bottom_border = 0; bottom_border < sideLength + 2; bottom_border++)
                Console.Write(" ■");
            Console.Write("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private int[,] vis_data;
        private string[,] vis_disp;

        //Symbol types
        private string[] type = { "  ", " #", " -", " H", " 0", " S" };

        //Array that stores ghosts
        private Vector2Int[] ghosts;
    }
}
