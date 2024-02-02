namespace StarryHud.Helpers;

using GTA;
using System;

public class FpsUpdateScript : Script
{
    public static int Fps { get; private set; }

    public FpsUpdateScript()
    {
        Interval = 1000;
        Tick += FpsUpdateScript_Tick;
    }

    private void FpsUpdateScript_Tick(object sender, EventArgs e)
    {
        Fps = (int)Game.FPS;
    }
}
