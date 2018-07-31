using UnityEngine;

static public class constants {
   static Color[] teamColors = { Color.green, Color.red, Color.white, Color.blue, Color.yellow, Color.black, Color.cyan, Color.magenta, Color.gray };

    static public Color getColorByTeamId(int id)
    {
        return teamColors[id % teamColors.Length];
    }
}
