using System;
using System.Collections.Generic;

namespace CatHacks8
{
    class MainFile
    {
        static void Main(string[] args)
        {
            Sound music = new Sound();

            string[] cheatCodes = { "capitalism", "ghostseatcarrots", "allseeingeye", "upupdowndownleftrightleftrightabstart", "seenoevil"};

            Console.Clear();
            Render render = new Render();
            int[] numLevelsPerDifficulty = { 2, 3, 5, 7 };

            int[,] numRoomsPerDifficulty = new int[,] { { 3, 4 }, { 4, 5 }, { 5, 6 }, { 5, 8 } };
            int[,] sideLengthPerDifficulty = new int[,] { { 12, 17 }, { 17, 22 }, { 27, 30 }, { 30, 40 } };
            int[,] roomSizePerDifficulty = new[,] { { 1, 2 }, { 1, 3 }, { 2, 3 }, { 3, 4 } };
            int[,] numGhostsPerDifficulty = new[,] { { 1, 2 }, { 2, 3 }, { 3, 5 }, { 4, 7 } };

            int maxGhostPositionAttempts = 100;
            int possibleEndPositions = 10;
            int playerSight = 4;
            int ghostSight = 3;
            int ghostLoseDist = 6;
            int pointsPerCoin = 100;

            int currPoints = 0;

            bool taxEvasion = false;
            bool invincible = false;

            for(int i = 0; i < args.Length; i++)
            {
                if(args[i] == cheatCodes[0])
                {
                    taxEvasion = true;
                } else if(args[i] == cheatCodes[1])
                {
                    ghostSight = Int32.MaxValue;
                } else if(args[i] == cheatCodes[2])
                {
                    render.SetSeeAll(true);
                } else if(args[i] == cheatCodes[3])
                {
                    invincible = true;
                } else if(args[i] == cheatCodes[4])
                {
                    for(int j = 0; j < numGhostsPerDifficulty.GetLength(0); j++)
                    {
                        for(int k = 0; k < numGhostsPerDifficulty.GetLength(1); k++)
                        {
                            numGhostsPerDifficulty[j, k] = 0;
                        }
                    }
                }
            }

            //Main Menu
            //Title

            if (taxEvasion)
            {
                PrintTaxEvasionTitle();
                music.Run(1);
            }
            else
            {
                music.Run(0);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("      ___         ___           ___           ___                         ___           ___     ");
                Console.WriteLine("     /\\  \\       /\\  \\         /\\  \\         /\\  \\                       /\\  \\         /\\  \\    ");
                Console.WriteLine("    /::\\  \\      \\:\\  \\       /::\\  \\        \\:\\  \\         ___         /::\\  \\       |::\\  \\   ");
                Console.WriteLine("   /:/\\:\\__\\      \\:\\  \\     /:/\\:\\  \\        \\:\\  \\       /\\__\\       /:/\\:\\  \\      |:|:\\  \\  ");
                Console.WriteLine("  /:/ /:/  /  ___ /::\\  \\   /:/ /::\\  \\   _____\\:\\  \\     /:/  /      /:/  \\:\\  \\   __|:|\\:\\  \\ ");
                Console.WriteLine(" /:/_/:/  /  /\\  /:/\\:\\__\\ /:/_/:/\\:\\__\\ /::::::::\\__\\   /:/__/      /:/__/ \\:\\__\\ /::::|_\\:\\__\\");
                Console.WriteLine(" \\:\\/:/  /   \\:\\/:/  \\/__/ \\:\\/:/  \\/__/ \\:\\~~\\~~\\/__/  /::\\  \\      \\:\\  \\ /:/  / \\:\\~~\\  \\/__/");
                Console.WriteLine("  \\::/__/     \\::/__/       \\::/__/       \\:\\  \\       /:/\\:\\  \\      \\:\\  /:/  /   \\:\\  \\      ");
                Console.WriteLine("   \\:\\  \\      \\:\\  \\        \\:\\  \\        \\:\\  \\      \\/__\\:\\  \\      \\:\\/:/  /     \\:\\  \\     ");
                Console.WriteLine("    \\:\\__\\      \\:\\__\\        \\:\\__\\        \\:\\__\\          \\:\\__\\      \\::/  /       \\:\\__\\    ");
                Console.WriteLine("     \\/__/       \\/__/         \\/__/         \\/__/           \\/__/       \\/__/         \\/__/    \n\n");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("      ___           ___           ___           ___                       ___           ___     ");
                Console.WriteLine("     /\\  \\         /\\  \\         /\\  \\         /\\__\\                     /\\  \\         /\\  \\    ");
                Console.WriteLine("    |::\\  \\       /::\\  \\        \\:\\  \\       /:/ _/_       ___         /::\\  \\        \\:\\  \\   ");
                Console.WriteLine("    |:|:\\  \\     /:/\\:\\  \\        \\:\\  \\     /:/ /\\  \\     /\\__\\       /:/\\:\\  \\        \\:\\  \\  ");
                Console.WriteLine("  __|:|\\:\\  \\   /:/ /::\\  \\   _____\\:\\  \\   /:/ /::\\  \\   /:/__/      /:/  \\:\\  \\   _____\\:\\  \\ ");
                Console.WriteLine(" /::::|_\\:\\__\\ /:/_/:/\\:\\__\\ /::::::::\\__\\ /:/_/:/\\:\\__\\ /::\\  \\     /:/__/ \\:\\__\\ /::::::::\\__\\");
                Console.WriteLine(" \\:\\~~\\  \\/__/ \\:\\/:/  \\/__/ \\:\\~~\\~~\\/__/ \\:\\/:/ /:/  / \\/\\:\\  \\__  \\:\\  \\ /:/  / \\:\\~~\\~~\\/__/");
                Console.WriteLine("  \\:\\  \\        \\::/__/       \\:\\  \\        \\::/ /:/  /   ~~\\:\\/\\__\\  \\:\\  /:/  /   \\:\\  \\      ");
                Console.WriteLine("   \\:\\  \\        \\:\\  \\        \\:\\  \\        \\/_/:/  /       \\::/  /   \\:\\/:/  /     \\:\\  \\     ");
                Console.WriteLine("    \\:\\__\\        \\:\\__\\        \\:\\__\\         /:/  /        /:/  /     \\::/  /       \\:\\__\\    ");
                Console.WriteLine("     \\/__/         \\/__/         \\/__/         \\/__/         \\/__/       \\/__/         \\/__/    \n\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nPlease input your selection:");
            //Info Page Header
            //Difficulty Labels
            Console.WriteLine("\nDifficulties:");
            Console.WriteLine("\t1: Easy\n\t2: Medium\n\t3: Hard\n\t4: Impossible\n\n5: Help Menu\n6: Quit");
            //Takes Input

            int inputNum = -1;
            do
            {
                string input = Console.ReadLine();
                try
                {
                    inputNum = Int32.Parse(input);
                }
                catch
                {
                    bool found = false;

                    for(int i = 0; i < cheatCodes.Length; i++)
                    {
                        if(input == cheatCodes[i])
                        {
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        Console.WriteLine("Please input a valid number");
                        inputNum = -1;
                    } else
                    {
                        string[] newArgs = new string[args.Length + 1];
                        for(int i = 0; i < args.Length; i++)
                        {
                            newArgs[i] = args[i];
                        }

                        newArgs[args.Length] = input;

                        Main(newArgs);
                        return;
                    }
                }
            } while (inputNum < 0 || inputNum > 6);
            //Info Page (calls main after taking more input)
            if (inputNum == 5)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                  ___           ___           ___     ");
                Console.WriteLine("                 /\\  \\         /\\__\\         /\\  \\    ");
                Console.WriteLine("    ___          \\:\\  \\       /:/ _/_       /::\\  \\   ");
                Console.WriteLine("   /\\__\\          \\:\\  \\     /:/ /\\__\\     /:/\\:\\  \\  ");
                Console.WriteLine("  /:/__/      _____\\:\\  \\   /:/ /:/  /    /:/  \\:\\  \\ ");
                Console.WriteLine(" /::\\  \\     /::::::::\\__\\ /:/_/:/  /    /:/__/ \\:\\__\\");
                Console.WriteLine(" \\/\\:\\  \\__  \\:\\~~\\~~\\/__/ \\:\\/:/  /     \\:\\  \\ /:/  /");
                Console.WriteLine("  ~~\\:\\/\\__\\  \\:\\  \\        \\::/__/       \\:\\  /:/  / ");
                Console.WriteLine("     \\::/  /   \\:\\  \\        \\:\\  \\        \\:\\/:/  /  ");
                Console.WriteLine("     /:/  /     \\:\\__\\        \\:\\__\\        \\::/  /   ");
                Console.WriteLine("     \\/__/       \\/__/         \\/__/         \\/__/    \n\n");

                Console.ForegroundColor = ConsoleColor.White;
                if (!taxEvasion)
                {
                    Console.WriteLine("A treasure is hidden in the top floor of this haunted house.\nGo and find it! But watch out for ghosts!");

                    Console.WriteLine("Tips:\n1. Don't touch the ghosts\n2. The ghosts will chase you if you get too close, don't get cornered\n3. Collect coins to score higher");
                    Console.WriteLine("4. You can see slightly further than the ghosts, use that to your advantage\n5. Use WASD to move\n");
                    Console.Write("Good luck!");
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(" You'll need it...\n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nCharacters: \n.: Floor\n■: Wall\nH: Exit\n¶: Ghost\nS: Start Position\nP: Player\n0: Coin\n");
                    Console.WriteLine("Enter anything to go back to the main menu.\n");
                } else
                {
                    PrintTaxEvasionInfo();
                }
                Console.ReadLine();
                Main(args);
            } else if(inputNum == 6)
            {
                System.Environment.Exit(0);
            }
            //Decide numLevels based on selected difficulty
            int difficulty = inputNum - 1;
            int numLevels = numLevelsPerDifficulty[difficulty];
            //Create array of empty maps, size of numLevels
            /*
             * LOOP FOR NUMLEVELS
            */

            Vector2Int startPos = new Vector2Int();
            Vector2Int endPos = new Vector2Int();

            for (int run = 0; run < numLevels; run++)
            {
                Player player = new Player();
                TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
                Random rand = new Random((int)t.TotalSeconds);

                int numRooms = rand.Next(numRoomsPerDifficulty[difficulty, 1] - numRoomsPerDifficulty[difficulty, 0]) + numRoomsPerDifficulty[difficulty, 0];
                int sideLength = rand.Next(sideLengthPerDifficulty[difficulty, 1] - sideLengthPerDifficulty[difficulty, 0]) + sideLengthPerDifficulty[difficulty, 0];

                if (run == 0)
                {
                    int wall = rand.Next(4);
                    int offset = rand.Next(sideLength);

                    switch (wall)
                    {
                        case 0:
                            startPos = new Vector2Int(offset, 0);
                            break;
                        case 1:
                            startPos = new Vector2Int(offset, sideLength - 1);
                            break;
                        case 2:
                            startPos = new Vector2Int(0, offset);
                            break;
                        case 3:
                            startPos = new Vector2Int(sideLength - 1, offset);
                            break;
                        default:
                            Console.WriteLine("Switch Error");
                            return;
                    }
                }

                bool found = false;
                Vector2Int[] possibleEndPos = new Vector2Int[possibleEndPositions];
                for(int i = 0; i < possibleEndPositions; i++)
                {
                    if (!found)
                    {
                        possibleEndPos[i] = new Vector2Int(rand.Next(sideLength), rand.Next(sideLength));

                        if (possibleEndPos[i].Distance(startPos) >= sideLength / 2)
                        {
                            endPos = possibleEndPos[i];
                            found = true;
                        }
                    }
                }

                if(!found)
                {
                    Vector2Int max = possibleEndPos[0];

                    for(int i = 1; i < possibleEndPositions; i++)
                    {
                        if(max.Distance(startPos) < possibleEndPos[i].Distance(startPos))
                        {
                            max = possibleEndPos[i];
                        }
                    }

                    endPos = max;
                }

                player.SetPosition(startPos);
                player.SetSightDist(playerSight);

                //Create the map
                RealMap gameMap = new RealMap(startPos, endPos, sideLength);
                gameMap.GenerateMap(numRooms, roomSizePerDifficulty[difficulty, 1], roomSizePerDifficulty[difficulty, 0], sideLength * 2);

                render.InitRender(gameMap, player);
                

                //MAKE numGhost GHOSTS

                int numGhosts = rand.Next(numGhostsPerDifficulty[difficulty, 1] - numGhostsPerDifficulty[difficulty, 0]) + numGhostsPerDifficulty[difficulty, 0];
                for(int i = 0; i < numGhosts; i++)
                {
                    int numTimes = 0;
                    Vector2Int position;

                    do
                    {
                        position = new Vector2Int(rand.Next(sideLength), rand.Next(sideLength));
                        numTimes++;
                    } while ((position.Distance(startPos) <= sideLength / 3 || gameMap.GetNodeByPos(position).GetBlocked()) && numTimes <= maxGhostPositionAttempts);
                    
                    if(numTimes <= maxGhostPositionAttempts)
                    {
                        Ghost newGhost = new Ghost(position);
                        newGhost.SetSightDist(ghostSight);
                        newGhost.SetLoseDist(ghostLoseDist);
                        gameMap.AddGhost(newGhost);
                    } else
                    {
                        numGhosts--;
                    }
                }
                //END - MAP GENERATION


                /*
                 * LOOP WHILE PLAYER POSITION IS NOT END POSITION
                */

                while (player.GetPos() != endPos)
                {
                    List<Ghost> ghosts = gameMap.GetGhostPositions();

                    if (ghosts != null)
                    {
                        for (int i = 0; i < ghosts.Count; i++)
                        {
                            if (ghosts[i].GetPos().Distance(player.GetPos()) < 1.5f && !invincible)
                            {
                                PlayerDie(currPoints, args, taxEvasion);
                                return;
                            }
                        }

                        //Ghost AI
                        for (int i = 0; i < ghosts.Count; i++)
                        {
                            if ((ghosts[i].GetPos().Distance(player.GetPos()) <= ghosts[i].GetSightDist() || ghosts[i].GetSeen()))
                            {
                                ghosts[i].MovePosition(player.GetPos(), gameMap);
                                ghosts[i].SetSeen(true);
                            }
                            else
                            {
                                ghosts[i].MovePosition(gameMap, i);
                                ghosts[i].SetSeen(false);
                            }

                            if (ghosts[i].GetPos() == player.GetPos() && !invincible)
                            {
                                PlayerDie(currPoints, args, taxEvasion);
                                return;
                            }
                        }
                    }

                    //Map Display
                    Console.Clear();
                    render.Input(gameMap, player);

                    //Player Movement 
                    //Display direction options
                    //Check for valid move position, false = game lost
                    //Check if move lands on exit, win game

                    Console.WriteLine("Points: " + currPoints);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("     ^     ");
                    Console.WriteLine("     |     ");
                    Console.WriteLine("     W     ");
                    Console.WriteLine("<--A   D-->");
                    Console.WriteLine("     S     ");
                    Console.WriteLine("     |     ");
                    Console.WriteLine("     v     ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Please input which direction you would like to go (W, S, D, A) or enter Q to return to the menu:");

                    string movementInput;

                    do
                    {
                        movementInput = Console.ReadLine();
                        movementInput = movementInput.ToLower();

                        if(movementInput == "q")
                        {
                            Main(args);
                            return;
                        }

                        if (movementInput != "w" && movementInput != "s" && movementInput != "d" && movementInput != "a")
                        {
                            Console.WriteLine("Please input a valid direction.");
                        } else
                        {
                            int moveNum;
                            Vector2Int offset;
                            switch (movementInput)
                            {
                                case "d":
                                    moveNum = 0;
                                    offset = new Vector2Int(0, 1);
                                    break;
                                case "a":
                                    moveNum = 1;
                                    offset = new Vector2Int(0, -1);
                                    break;
                                case "s":
                                    moveNum = 3;
                                    offset = new Vector2Int(1, 0);
                                    break;
                                case "w":
                                    moveNum = 2;
                                    offset = new Vector2Int(-1, 0);
                                    break;
                                default:
                                    Console.WriteLine("Switch Error");
                                    return;
                            }
                            MapNode possibleNode = gameMap.GetNodeByPos(player.GetPos() + offset);

                            if (possibleNode != null)
                            {
                                if(!possibleNode.GetBlocked())
                                {
                                    player.Move(gameMap.GetEnd(), moveNum);

                                    if (ghosts != null)
                                    {
                                        for (int i = 0; i < ghosts.Count; i++)
                                        {
                                            if (player.GetPos() == ghosts[i].GetPos() && !invincible)
                                            {
                                                PlayerDie(currPoints, args, taxEvasion);
                                                return;
                                            }
                                        }
                                    }
                                } else
                                {
                                    Console.WriteLine("That area is blocked! Please select again.");
                                    movementInput = "";
                                }
                            } else
                            {
                                Console.WriteLine("That is not a valid tile! Please select again.");
                                movementInput = "";
                            }
                        }
                    } while (movementInput != "w" && movementInput != "s" && movementInput != "d" && movementInput != "a");

                    List<Vector2Int> coinPositions = gameMap.GetCoinPositions();

                    for(int i = 0; i < coinPositions.Count; i++)
                    {
                        if(player.GetPos() == coinPositions[i])
                        {
                            coinPositions.RemoveAt(i);
                            currPoints += pointsPerCoin;
                            gameMap.SetCoinPositions(coinPositions);
                        }
                    }
                }

                startPos = endPos;

                if(run >= numLevels - 1)
                {
                    PlayerWin(currPoints, args, taxEvasion);
                    return;
                }

                bool found1 = false;
                Vector2Int[] possibleEndPos1 = new Vector2Int[possibleEndPositions];
                for (int i = 0; i < possibleEndPositions; i++)
                {
                    if (!found1)
                    {
                        possibleEndPos1[i] = new Vector2Int(rand.Next(sideLength), rand.Next(sideLength));

                        if (possibleEndPos1[i].Distance(startPos) >= sideLength / 2)
                        {
                            endPos = possibleEndPos1[i];
                            found1 = true;
                        }
                    }
                }

                if (!found1)
                {
                    Vector2Int max = possibleEndPos1[0];

                    for (int i = 1; i < possibleEndPositions; i++)
                    {
                        if (max.Distance(startPos) < possibleEndPos1[i].Distance(startPos))
                        {
                            max = possibleEndPos1[i];
                        }
                    }

                    endPos = max;
                }
            }
        }

        static void PlayerDie(int points, string[] args, bool taxEvasion)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("      ___           ___           ___           ___     ");
            Console.WriteLine("     /\\__\\         /\\  \\         /\\  \\         /\\__\\    ");
            Console.WriteLine("    /:/ _/_       /::\\  \\       |::\\  \\       /:/ _/_   ");
            Console.WriteLine("   /:/ /\\  \\     /:/\\:\\  \\      |:|:\\  \\     /:/ /\\__\\  ");
            Console.WriteLine("  /:/ /::\\  \\   /:/ /::\\  \\   __|:|\\:\\  \\   /:/ /:/ _/_ ");
            Console.WriteLine(" /:/__\\/\\:\\__\\ /:/_/:/\\:\\__\\ /::::|_\\:\\__\\ /:/_/:/ /\\__\\");
            Console.WriteLine(" \\:\\  \\ /:/  / \\:\\/:/  \\/__/ \\:\\~~\\  \\/__/ \\:\\/:/ /:/  /");
            Console.WriteLine("  \\:\\  /:/  /   \\::/__/       \\:\\  \\        \\::/_/:/  / ");
            Console.WriteLine("   \\:\\/:/  /     \\:\\  \\        \\:\\  \\        \\:\\/:/  /  ");
            Console.WriteLine("    \\::/  /       \\:\\__\\        \\:\\__\\        \\::/  /   ");
            Console.WriteLine("     \\/__/         \\/__/         \\/__/         \\/__/    \n\n");

            Console.WriteLine("      ___                         ___           ___     ");
            Console.WriteLine("     /\\  \\          ___          /\\__\\         /\\  \\    ");
            Console.WriteLine("    /::\\  \\        /\\  \\        /:/ _/_       /::\\  \\   ");
            Console.WriteLine("   /:/\\:\\  \\       \\:\\  \\      /:/ /\\__\\     /:/\\:\\__\\  ");
            Console.WriteLine("  /:/  \\:\\  \\       \\:\\  \\    /:/ /:/ _/_   /:/ /:/  /  ");
            Console.WriteLine(" /:/__/ \\:\\__\\  ___  \\:\\__\\  /:/_/:/ /\\__\\ /:/_/:/__/___");
            Console.WriteLine(" \\:\\  \\ /:/  / /\\  \\ |:|  |  \\:\\/:/ /:/  / \\:\\/:::::/  /");
            Console.WriteLine("  \\:\\  /:/  /  \\:\\  \\|:|  |   \\::/_/:/  /   \\::/~~/~~~~ ");
            Console.WriteLine("   \\:\\/:/  /    \\:\\__|:|__|    \\:\\/:/  /     \\:\\~~\\     ");
            Console.WriteLine("    \\::/  /      \\::::/__/      \\::/  /       \\:\\__\\    ");
            Console.WriteLine("     \\/__/        ~~~~           \\/__/         \\/__/    \n\n");

            Console.ForegroundColor = ConsoleColor.White;

            if (!taxEvasion)
            {
                Console.WriteLine("You were taken by the ghosts! I pray for your soul.");
                Console.WriteLine("You lost after getting " + points + " points. What a shame...");
            } else
            {
                Console.WriteLine("You've been caught by the IRS! Now you'll have to pay income tax for the rest of your life. What a shame...");
                Console.WriteLine("You would've escaped with " + points + " dollars if you hadn't got yourself caught.");
            }

            Console.WriteLine("Enter anything to return to the main menu.");
            Console.ReadLine();
            Main(args);
        }
        
        static void PlayerWin(int points, string[] args, bool taxEvasion)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("                  ___           ___                    ___                       ___     ");
            Console.WriteLine("                 /\\  \\         /\\  \\                  /\\  \\                     /\\  \\    ");
            Console.WriteLine("      ___       /::\\  \\        \\:\\  \\                _\\:\\  \\       ___          \\:\\  \\   ");
            Console.WriteLine("     /|  |     /:/\\:\\  \\        \\:\\  \\              /\\ \\:\\  \\     /\\__\\          \\:\\  \\  ");
            Console.WriteLine("    |:|  |    /:/  \\:\\  \\   ___  \\:\\  \\            _\\:\\ \\:\\  \\   /:/__/      _____\\:\\  \\ ");
            Console.WriteLine("    |:|  |   /:/__/ \\:\\__\\ /\\  \\  \\:\\__\\          /\\ \\:\\ \\:\\__\\ /::\\  \\     /::::::::\\__\\");
            Console.WriteLine("  __|:|__|   \\:\\  \\ /:/  / \\:\\  \\ /:/  /          \\:\\ \\:\\/:/  / \\/\\:\\  \\__  \\:\\~~\\~~\\/__/");
            Console.WriteLine(" /::::\\  \\    \\:\\  /:/  /   \\:\\  /:/  /            \\:\\ \\::/  /   ~~\\:\\/\\__\\  \\:\\  \\      ");
            Console.WriteLine(" ~~~~\\:\\  \\    \\:\\/:/  /     \\:\\/:/  /              \\:\\/:/  /       \\::/  /   \\:\\  \\     ");
            Console.WriteLine("      \\:\\__\\    \\::/  /       \\::/  /                \\::/  /        /:/  /     \\:\\__\\    ");
            Console.WriteLine("       \\/__/     \\/__/         \\/__/                  \\/__/         \\/__/       \\/__/    \n\n");

            Console.ForegroundColor = ConsoleColor.White;

            if (!taxEvasion)
            {
                Console.WriteLine("You have found the treasure and collected " + points + " points! Congratulations!");
            } else
            {
                Console.WriteLine("You've successfully deleted your tax records and escaped the clutches of the IRS.");
                Console.WriteLine("You also managed to liberate " + points + " dollars that were stolen by the IRS from hard-working citizens like yourself");
            }

            Console.WriteLine("Enter anything to return to the main menu.\n");

            Console.ReadLine();
            Main(args);
        }

        static void PrintTaxEvasionTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("                    ___           ___      ");
            Console.WriteLine("                   /\\  \\         /|  |     ");
            Console.WriteLine("      ___         /::\\  \\       |:|  |     ");
            Console.WriteLine("     /\\__\\       /:/\\:\\  \\      |:|  |     ");
            Console.WriteLine("    /:/  /      /:/ /::\\  \\   __|:|__|     ");
            Console.WriteLine("   /:/__/      /:/_/:/\\:\\__\\ /::::\\__\\_____");
            Console.WriteLine("  /::\\  \\      \\:\\/:/  \\/__/ ~~~~\\::::/___/");
            Console.WriteLine(" /:/\\:\\  \\      \\::/__/          |:|~~|    ");
            Console.WriteLine(" \\/__\\:\\  \\      \\:\\  \\          |:|  |    ");
            Console.WriteLine("      \\:\\__\\      \\:\\__\\         |:|__|    ");
            Console.WriteLine("       \\/__/       \\/__/         |/__/     \n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("      ___                         ___           ___                       ___           ___     ");
            Console.WriteLine("     /\\__\\          ___          /\\  \\         /\\__\\                     /\\  \\         /\\  \\    ");
            Console.WriteLine("    /:/ _/_        /\\  \\        /::\\  \\       /:/ _/_       ___         /::\\  \\        \\:\\  \\   ");
            Console.WriteLine("   /:/ /\\__\\       \\:\\  \\      /:/\\:\\  \\     /:/ /\\  \\     /\\__\\       /:/\\:\\  \\        \\:\\  \\  ");
            Console.WriteLine("  /:/ /:/ _/_       \\:\\  \\    /:/ /::\\  \\   /:/ /::\\  \\   /:/__/      /:/  \\:\\  \\   _____\\:\\  \\ ");
            Console.WriteLine(" /:/_/:/ /\\__\\  ___  \\:\\__\\  /:/_/:/\\:\\__\\ /:/_/:/\\:\\__\\ /::\\  \\     /:/__/ \\:\\__\\ /::::::::\\__\\");
            Console.WriteLine(" \\:\\/:/ /:/  / /\\  \\ |:|  |  \\:\\/:/  \\/__/ \\:\\/:/ /:/  / \\/\\:\\  \\__  \\:\\  \\ /:/  / \\:\\~~\\~~\\/__/");
            Console.WriteLine("  \\::/_/:/  /  \\:\\  \\|:|  |   \\::/__/       \\::/ /:/  /   ~~\\:\\/\\__\\  \\:\\  /:/  /   \\:\\  \\      ");
            Console.WriteLine("   \\:\\/:/  /    \\:\\__|:|__|    \\:\\  \\        \\/_/:/  /       \\::/  /   \\:\\/:/  /     \\:\\  \\     ");
            Console.WriteLine("    \\::/  /      \\::::/__/      \\:\\__\\         /:/  /        /:/  /     \\::/  /       \\:\\__\\    ");
            Console.WriteLine("     \\/__/        ~~~~           \\/__/         \\/__/         \\/__/       \\/__/         \\/__/    \n\n");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("      ___                       ___           ___                         ___                         ___           ___     ");
            Console.WriteLine("     /\\__\\                     /\\  \\         /\\  \\                       /\\  \\                       /\\  \\         /\\  \\    ");
            Console.WriteLine("    /:/ _/_       ___         |::\\  \\        \\:\\  \\                     /::\\  \\         ___         /::\\  \\       /::\\  \\   ");
            Console.WriteLine("   /:/ /\\  \\     /\\__\\        |:|:\\  \\        \\:\\  \\                   /:/\\:\\  \\       /\\__\\       /:/\\:\\  \\     /:/\\:\\__\\  ");
            Console.WriteLine("  /:/ /::\\  \\   /:/__/      __|:|\\:\\  \\   ___  \\:\\  \\   ___     ___   /:/ /::\\  \\     /:/  /      /:/  \\:\\  \\   /:/ /:/  /  ");
            Console.WriteLine(" /:/_/:/\\:\\__\\ /::\\  \\     /::::|_\\:\\__\\ /\\  \\  \\:\\__\\ /\\  \\   /\\__\\ /:/_/:/\\:\\__\\   /:/__/      /:/__/ \\:\\__\\ /:/_/:/__/___");
            Console.WriteLine(" \\:\\/:/ /:/  / \\/\\:\\  \\__  \\:\\~~\\  \\/__/ \\:\\  \\ /:/  / \\:\\  \\ /:/  / \\:\\/:/  \\/__/  /::\\  \\      \\:\\  \\ /:/  / \\:\\/:::::/  /");
            Console.WriteLine("  \\::/ /:/  /   ~~\\:\\/\\__\\  \\:\\  \\        \\:\\  /:/  /   \\:\\  /:/  /   \\::/__/      /:/\\:\\  \\      \\:\\  /:/  /   \\::/~~/~~~~ ");
            Console.WriteLine("   \\/_/:/  /       \\::/  /   \\:\\  \\        \\:\\/:/  /     \\:\\/:/  /     \\:\\  \\      \\/__\\:\\  \\      \\:\\/:/  /     \\:\\~~\\     ");
            Console.WriteLine("     /:/  /        /:/  /     \\:\\__\\        \\::/  /       \\::/  /       \\:\\__\\          \\:\\__\\      \\::/  /       \\:\\__\\    ");
            Console.WriteLine("     \\/__/         \\/__/       \\/__/         \\/__/         \\/__/         \\/__/           \\/__/       \\/__/         \\/__/    \n\n");

            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintTaxEvasionInfo()
        {
            Console.WriteLine("The pesky IRS agents have been stealing money from you in the form of income tax for far too long.\nBreak into their offices and delete your tax records on the top floor to take revenge.\nAll taxation is theft. Good luck!\n");
            Console.WriteLine("Tips:\n1. Don't touch the IRS agents\n2. The agents will chase you if you get too close, don't get cornered\n3. Take back what's rightfully yours to increase your score");
            Console.WriteLine("4. You can see slightly further than the IRS agents, use that to your advantage\n5. Use WASD to move\n");
            Console.WriteLine("\nCharacters: \n.: Floor\n■: Wall\nH: Exit\n¶: IRS agent\nS: Start Position\nP: Player\n0: Income Tax Revenue\n");
            Console.WriteLine("Enter anything to go back to the main menu.");
        }
    }
}
