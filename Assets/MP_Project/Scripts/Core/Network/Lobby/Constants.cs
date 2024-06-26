using System.Collections.Generic;

public class Constants
{
    public const string JoinKey = "j";
    public const string DifficultyKey = "d";
    public const string GameTypeKey = "t";

    public static readonly List<string> GameTypes = new() { "Normal" };
    public static readonly List<string> Difficulties = new() { "Normal" };
}