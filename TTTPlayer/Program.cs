using System.Runtime.Serialization;

class Program
{
    static Dictionary<string, int> PlaceVals = new Dictionary<string, int>();
    static Dictionary<string, string> Pl = new Dictionary<string, string>();
    static void Main()
    {
        //Set win condition variable and dictionary of space values.
        bool Wins = false;
        int MoveCounter = 0;

        for (int i = 1; i < 4; i++)
        {
            PlaceVals.Add($"A{i}", 0);
            Pl.Add($"A{i}", $"A{i}");
            PlaceVals.Add($"B{i}", 0);
            Pl.Add($"B{i}", $"B{i}");
            PlaceVals.Add($"C{i}", 0);
            Pl.Add($"C{i}", $"C{i}");
        }

        //Center square is given higher initial value.

        PlaceVals["B2"] = 3;
        MoveCalc(PlaceVals, "B2");

        //Game Starting Conditions.
        Console.WriteLine("Who goes first?");
        var Response = Console.ReadLine();
        bool StartCon = false;
        while (!StartCon)
        {
            if (Response == "You" || Response == "you")
            {
                Console.WriteLine("Very well. I go B2");
                PlaceVals["B2"] = -1;
                Pl["B2"] = " O";
                StartCon = true;
                MoveCounter++;
            }
            else if (Response == "Me" || Response == "me")
            {
                Console.WriteLine("Good! Your move:");
                StartCon = true;
            }
            else
            {
                Console.WriteLine("Which one of us? You or me?");
                Response = Console.ReadLine();
            }
        }

        //Main game loop.
        while (!Wins)
        {
            Console.WriteLine($"{Pl["A1"]} | {Pl["A2"]} | {Pl["A3"]}");
            Console.WriteLine("--------------");
            Console.WriteLine($"{Pl["B1"]} | {Pl["B2"]} | {Pl["B3"]}");
            Console.WriteLine("--------------");
            Console.WriteLine($"{Pl["C1"]} | {Pl["C2"]} | {Pl["C3"]}");
            Console.WriteLine("Your move:");
            Response = Console.ReadLine();
        
        //Checks player response and move posibility. 

            if (Response == "exit")
            {
                return;
            }
            else if (Response.Length != 2)
            {
                Console.WriteLine("That is not a valid move.");
            }
            else if (Char.IsLetter(Response[0]) && Char.IsDigit(Response[1]))
            {
                if (((int)Response[0] > 67 || (int)Response[0] < 65) || ((int)Response[1] < 49 || (int)Response[1] > 51))
                {
                    Console.WriteLine("That is not a space.");
                }
                else if (PlaceVals[Response] == -1)
                {
                    Console.WriteLine("That space is taken.");
                }
                else
                {
                    PlaceVals[Response] = -1;
                    Pl[Response] = " X";
                    MoveCalc(PlaceVals, Response);
                    //Check for win state.

                    if (HasWon() == 1)
                    {
                        Console.WriteLine($"{Pl["A1"]} | {Pl["A2"]} | {Pl["A3"]}");
                        Console.WriteLine("--------------");
                        Console.WriteLine($"{Pl["B1"]} | {Pl["B2"]} | {Pl["B3"]}");
                        Console.WriteLine("--------------");
                        Console.WriteLine($"{Pl["C1"]} | {Pl["C2"]} | {Pl["C3"]}");
                        Console.WriteLine("You have won... Well done.");
                        return;
                    }
                    MoveCounter++;
                    if (MoveCounter >= 9)
                    {
                        Wins = true;
                        break;
                    }
                    //Comp makes move based on heatmap.

                    var MaxSpace = PlaceVals.MaxBy(kvp => kvp.Value);
                    var SP = MaxSpace.Key;
                    if (PlaceVals[SP] != -1)
                    {
                        PlaceVals[SP] = -1;
                        Pl[SP] = " O";
                        MoveCalc(PlaceVals, SP);
                    }
                    if (HasWon() == 2)
                    {
                        Console.WriteLine($"{Pl["A1"]} | {Pl["A2"]} | {Pl["A3"]}");
                        Console.WriteLine("--------------");
                        Console.WriteLine($"{Pl["B1"]} | {Pl["B2"]} | {Pl["B3"]}");
                        Console.WriteLine("--------------");
                        Console.WriteLine($"{Pl["C1"]} | {Pl["C2"]} | {Pl["C3"]}");
                        Console.WriteLine("I have won. Thanks for playing.");
                        return;
                    }
                    MoveCounter++;
                    if (MoveCounter >= 9)
                    {
                        Wins = true;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid move.");
            }

        }
        Console.WriteLine($"{Pl["A1"]} | {Pl["A2"]} | {Pl["A3"]}");
        Console.WriteLine("--------------");
        Console.WriteLine($"{Pl["B1"]} | {Pl["B2"]} | {Pl["B3"]}");
        Console.WriteLine("--------------");
        Console.WriteLine($"{Pl["C1"]} | {Pl["C2"]} | {Pl["C3"]}");
        Console.WriteLine("Tie.");
        return;
    }

    //Generates heatmap of space value based on current piece placement.
    static void MoveCalc(Dictionary<string, int> PlaceVals, string Response)
    {

        char col = Response[0];
        int row = Response[1] - 48;

    //Update adjacent space values.
        for (int c = -1; c < 2; c++)
        {
            for (int r = -1; r < 2; r++)
            {
                try
                {
                    if (PlaceVals[$"{(char)((int)col + c)}{row + r}"] != -1)
                    {
                        PlaceVals[$"{(char)((int)col + c)}{row + r}"] += 1;
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        
    //TODO Check for spaces that are adjacent to two taken spaces.
        
    }

    static int HasWon()
    {
        int XCounter = 0;
        int OCounter = 0;

//Check Cols

        for (int i = 1; i < 4; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Pl[$"{(char)(65 + j)}{i}"] == " X")
                {
                    XCounter++;
                }
                else if (Pl[$"{(char)(65 + j)}{i}"] == " O")
                {
                    OCounter++;
                }
            }
            if (XCounter >= 3)
            {
                return 1;
            }
            else if (OCounter >= 3)
            {
                return 2;
            }
            else
            {
                XCounter = 0;
                OCounter = 0;
            }
        }

//Check Diag

        for (int i = 1; i < 4; i++)
        {

            if (Pl[$"{(char)(64 + i)}{i}"] == " X")
            {
                XCounter++;
            }
            else if (Pl[$"{(char)(64 + i)}{i}"] == " O")
            {
                OCounter++;
            }
        }
        if (XCounter >= 3)
        {
            return 1;
        }
        else if (OCounter >= 3)
        {
            return 2;
        }
        else
        {
            XCounter = 0;
            OCounter = 0;
        }

//Diag Check 2

        int p = 0; 
        for (int i = 1; i < 4; i++)
        {
            if (Pl[$"{(char)(67 - p)}{i}"] == " X")
            {
                XCounter++;
            }
            else if (Pl[$"{(char)(67 - p)}{i}"] == " O")
            {
                OCounter++;
            }
            p++;
        }
        if (XCounter >= 3)
        {
            return 1;
        }
        else if (OCounter >= 3)
        {
            return 2;
        }
        else
        {
            XCounter = 0;
            OCounter = 0;
        }

//Check Rows
        for (int i = 0; i < 3; i++)
        {
            for (int h = 1; h < 4; h++)
            {
                if (Pl[$"{(char)(65 + i)}{h}"] == " X")
                {
                    XCounter++;
                }
                else if (Pl[$"{(char)(65 + i)}{h}"] == " O")
                {
                    OCounter++;
                }
            }
            if (XCounter >= 3)
            {
                return 1;
            }
            else if (OCounter >= 3)
            {
                return 2;
            }
            else
            {
                XCounter = 0;
                OCounter = 0;
            }
        }  
        
        return 0;
    } 
}

