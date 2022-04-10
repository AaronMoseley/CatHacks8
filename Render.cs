using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatHacks8
{
    class Render
    {

        //Allow toggling of visibility
        public void SetSeeAll(bool seeAll)
        {
            this.seeAll = seeAll;
        }

        //Run this at the first instance of a level to populate whole map
        public void InitRender(RealMap mapNodes, Player player)
        {
            //Initialize map data arrays
            int sideLength = mapNodes.GetSideLength();
            vis_data = new int[sideLength, sideLength];
            vis_sight = new int[sideLength, sideLength];

            //Populate everything static in the map
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    //Populate the node data type (texture)
                    vis_data[i, j] = mapNodes.GetNodeByPos(new Vector2Int(i, j)).GetChar();

                    //Initialize vis_sight as zero
                    if (!seeAll)
                        vis_sight[i, j] = 0;
                    else
                        vis_sight[i, j] = 3;
                }
            }

            //Handle coins
            List<Vector2Int> newCoins = mapNodes.GetCoinPositions();
            coins = new Vector2Int[newCoins.Count];

            for (int c = 0; c < newCoins.Count; c++)
            {
                coins[c] = newCoins[c];
            }
        }

        //Take in 2d array of nodes
        public void Input(RealMap mapNodes, Player player)
        {
            int sideLength = mapNodes.GetSideLength();

            //Player location
            Vector2Int playerPos = player.GetPos();
            int sightDistance = player.GetSightDist();

            coins = mapNodes.GetCoinPositions().ToArray();

            //Check every index
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    //Only operate on cells within player sight distance
                    Vector2Int nodeTracker = new Vector2Int(i, j);  //Create a temporary vector for sight distance
                    float distance = playerPos.Distance(nodeTracker);

                    //Remaining logic is only performed on nodes around player
                    if ((int)distance <= sightDistance + 3)
                    {
                        //Set gray scale if the player has seen it
                        if (mapNodes.GetNodeByPos(new Vector2Int(i, j)).GetSeen())
                            vis_sight[i, j] = 1;

                        //Put textures back together that were previously broken
                        vis_data[i, j] = mapNodes.GetNodeByPos(new Vector2Int(i, j)).GetChar();


                        if ((int)distance <= sightDistance)
                        {
                            //Set nodes in sight range 
                            vis_sight[i, j] = 2;
                            mapNodes.GetNodeByPos(new Vector2Int(i, j)).SetSeen(true);

                            //Overlay ghosts 
                            if (mapNodes.GetGhostPositions() != null)
                            {
                                List<Ghost> newGhosts = mapNodes.GetGhostPositions();
                                ghosts = new Vector2Int[newGhosts.Count];

                                for (int g = 0; g < newGhosts.Count; g++)
                                {
                                    ghosts[g] = newGhosts[g].GetPos();
                                }
                            }
                        }
                    }
                }
            }

            //Overlay player in post
            vis_data[playerPos.GetX(), playerPos.GetY()] = 6;

            Interpret();

        }

        //Convert the data that controls the map 
        //Takes an array that contains positions of ghosts
        public void Interpret()
        {
            vis_disp = new string[vis_data.GetLength(0), vis_data.GetLength(1)];

            //Add coins
            for (int c = 0; c < coins.Length; c++)
                if ((vis_sight[coins[c].GetX(), coins[c].GetY()] == 2) && (vis_data[coins[c].GetX(), coins[c].GetY()] != 6))
                {
                    vis_disp[coins[c].GetX(), coins[c].GetY()] = type[7];
                    vis_data[coins[c].GetX(), coins[c].GetY()] = 7;
                }

            //Populate the standard map (Things that don't move)
            for (int x = 0; x < vis_data.GetLength(0); x++)
            {
                for (int y = 0; y < vis_data.GetLength(1); y++)
                {
                    vis_disp[x, y] = type[vis_data[x, y]];
                }
            }

            //Add the ghosts afterwards
            if(ghosts != null)
            {
                for (int g = 0; g < ghosts.Length; g++)
                {
                    if (vis_sight[ghosts[g].GetX(), ghosts[g].GetY()] == 2)
                    {
                        vis_disp[ghosts[g].GetX(), ghosts[g].GetY()] = type[4];
                        vis_data[ghosts[g].GetX(), ghosts[g].GetY()] = 4;
                    }
                }
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
                    //Visibility color handler
                    if (vis_sight[x, y] == 0)
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (vis_sight[x, y] == 1)
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if ((vis_sight[x, y] == 2))
                        Console.ForegroundColor = ConsoleColor.DarkYellow;

                    //Ghosts are colored white only if they are in visible range
                    if ((vis_sight[x, y] == 2) && (vis_data[x, y] == 4))
                        Console.ForegroundColor = ConsoleColor.White;
                    //Stairs are colored green only if they are in visible range
                    else if ((vis_sight[x, y] == 2) && (vis_data[x, y] == 3))
                        Console.ForegroundColor = ConsoleColor.Green;
                    //Coins are colored magenta only if they are in visible range
                    else if ((vis_sight[x, y] == 2) && (vis_data[x, y] == 7))
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    //The player is colored red
                    else if (vis_data[x, y] == 6)
                        Console.ForegroundColor = ConsoleColor.Red;

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

        //Map control arrays
        private int[,] vis_data;
        private string[,] vis_disp;
        private int[,] vis_sight;

        //Symbol types
        private string[] type = { "  ", " ■", " .", " H", " ¶", " S", " P", " 0" };

        //Arrays that store ghosts and coins
        private Vector2Int[] ghosts;
        private Vector2Int[] coins;

        //Toggle total visibility
        private bool seeAll;
    }
}