class Program
{
    static Dictionary<string, int> PlaceVals = new Dictionary<string, int>();
    static Dictionary<string, string> Pl = new Dictionary<string, string>();
    static void Main()
    {
        //Set win condition variable and dictionary of space values.
        bool Wins = false;

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
            Console.WriteLine($"{Pl["C1"]} | {Pl["C2"]} | {Pl["C3"]}"); ;
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

                    //Comp makes move based on heatmap.

                    var MaxSpace = PlaceVals.MaxBy(kvp => kvp.Value);
                    var SP = MaxSpace.Key;
                    if (PlaceVals[SP] != -1)
                    {
                        PlaceVals[SP] = -1;
                        Pl[SP] = " O";
                        MoveCalc(PlaceVals, SP);
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid move.");
            }

        }
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

}

