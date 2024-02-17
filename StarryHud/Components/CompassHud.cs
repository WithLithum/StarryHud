// Copyright (C) WithLithum 2024.
// Licensed under the GNU General Public License, version 3 or (at your opinion)
// any later version. See COPYING.txt.

using System;
using System.Drawing;
using GTA;
using GTA.UI;
using LemonUI;
using LemonUI.Elements;
using LemonUI.Tools;

namespace StarryHud.Components;

internal class CompassHud : IProcessable
{
    private readonly ScaledRectangle _background = new(new PointF(10, 10), new SizeF(25, 25));
    private readonly ScaledText _text = new(new PointF(12.5f, 12.5f), "--");
    private readonly ScaledText _degrees = new(new PointF(12.5f, 12.5f), "---°");
    
    private readonly ScaledText _zone = new(PointF.Empty, "---");
    private readonly ScaledText _street = new(PointF.Empty, "---");
    private readonly ScaledRectangle _zoneBackground = new(PointF.Empty, new SizeF(25, 25));

    private const float AboveMinimapHeight = 1080f - 205f;
    private const float MinimapLeft = 30f;

    public void Initialize()
    {
        _background.Color = Constants.BackgroundColor;

        // Y: 1080 - 20 - 25f
        _background.Position = SafeZone.GetSafePosition(MinimapLeft, AboveMinimapHeight);
        _background.Size = new SizeF(42f, 30f);

        _text.Position = SafeZone.GetSafePosition(new PointF(MinimapLeft + 5f, AboveMinimapHeight + 0.25f));
        _text.Scale = Constants.TextScale;

        _degrees.Position = SafeZone.GetSafePosition(new PointF(MinimapLeft + 25f, AboveMinimapHeight + 0.25f));
        _degrees.Scale = Constants.TextScale;

        _zoneBackground.Color = Constants.BackgroundColor;
        _zoneBackground.Position = SafeZone.GetSafePosition(MinimapLeft, AboveMinimapHeight + 30f);
        _zoneBackground.Size = new SizeF(42f, 30f);

        _zone.Position = SafeZone.GetSafePosition(MinimapLeft + 5, AboveMinimapHeight + 30f + 0.25f);
        _zone.Scale = Constants.TextScale;
        _zone.Color = Color.Orange;

        _street.Position = SafeZone.GetSafePosition(MinimapLeft + 15, AboveMinimapHeight + 30f + 0.25f);
        _street.Scale = Constants.TextScale;
        _street.Color = Color.LightCyan;
    }
    
    public void Process()
    {
        if (Visible)
        {
            if (Hud.IsRadarVisible)
            {
                _background.Draw();
                _text.Draw();
                _degrees.Draw();
            }

            if (Game.IsEnabledControlPressed(Control.MultiplayerInfo))
            {
                _zoneBackground.Draw();
                _zone.Draw();
                _street.Draw();
            }
        }
    }

    internal void Update()
    {
        var heading = (int)Math.Round(Game.Player.Character.Heading);

        _text.Text = Util.GetHeadingText(heading);
        _degrees.Position = SafeZone.GetSafePosition(new PointF(MinimapLeft + 5f + _text.Width + 10f, AboveMinimapHeight + 0.25f));
        _degrees.Text = $"{heading}°";

        _background.Size = new SizeF(5f + _text.Width + 10f + _degrees.Width + 5f, Constants.TextHeight);

        _zone.Text = World.GetZoneLocalizedName(Game.Player.Character.Position);

        _street.Position = SafeZone.GetSafePosition(MinimapLeft + 5f,
            _zone.Position.Y + 30f);
        _street.Text = World.GetStreetName(Game.Player.Character.Position);
        _zoneBackground.Size = new(Util.GetLinesBackgroundWidth(_zone, _street), Constants.TextHeight * 2);
        
        // Final expanded effect:

        // NW  100°
        // Zone
        // Street
    }

    public bool Visible { get; set; } = true;
}