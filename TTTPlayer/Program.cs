
//Set win condition variable and dictionary of space values.
bool Wins = false;
Dictionary<string, int> PlaceVals = new Dictionary<string, int>();
Dictionary<string, string> Pl = new Dictionary<string, string>();

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
        Console.WriteLine("Test Passed!");
        return;
    }
    else
    {
        Console.WriteLine("Invalid move.");
    }
}

public static Dictionary<string, int> {

}
