namespace StarryHud.Components;

using LemonUI;
using LemonUI.Elements;
using StarryHud.Helpers;
using System;
using System.Drawing;

internal class ClockHud : IProcessable
{
    private readonly ScaledText _textClock = new(new PointF(15, 5), "--:--:--", Constants.TextScale);
    private readonly ScaledText _textFps = new(new PointF(97, 5), "-- FPS", Constants.TextScale);
    private readonly ScaledRectangle _background = new(new PointF(10, 5), new SizeF(85f + 65f + 25f, Constants.TextHeight));

    public bool Visible { get; set; }

    public void Initialize()
    {
        Visible = true;
        _background.Color = Constants.BackgroundColor;
    }


    public void Process()
    {
        if (!Visible) return;

        _textClock.Draw();
        _textFps.Draw();
        _background.Draw();
    }

    public void Update()
    {
        _textClock.Text = DateTime.Now.ToString("HH:mm:ss");
        _textFps.Text = $"{FpsUpdateScript.Fps} FPS";
    }
}
