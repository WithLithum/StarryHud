// Copyright (C) WithLithum 2024.
// Licensed under the GNU General Public License, version 3 or (at your opinion)
// any later version. See COPYING.txt.

namespace StarryHud;

using LemonUI.Elements;
using System.Linq;

internal static class Util
{
    // Get heading source: https://codereview.stackexchange.com/questions/1287/calculating-compass-direction

    private static readonly string[] Directions =
    [
        "N", "NE", "E", "SE", "S", "SW", "W", "NW", "N"
    ];
    
    internal static string GetHeadingText(int heading)
    {
        var index = (heading + 23) / 45;
        return Directions[index];
    }

    internal static float GetLinesBackgroundWidth(params ScaledText[] texts)
    {
        var baseValue = 0f;

        foreach (var width in texts.Select(x => x.Width))
        {
            if (width > baseValue)
            {
                baseValue = width;
            }
        }

        baseValue += 10f;
        return baseValue;
    }

    internal static float GetTextBackgroundWidth(params ScaledText[] texts)
    {
        var baseValue = 5f;
        var first = false;

        foreach (var text in texts)
        {
            if (first)
            {
                baseValue += 10f;
            }

            baseValue += text.Width;
            first = true;
        }

        baseValue += 5f;
        return baseValue;
    }
}