using System;
using GTA;
using GTA.UI;
using LemonUI;
using StarryHud.Components;

// Copyright (C) WithLithum 2024.
// Licensed under the GNU General Public License, version 3 or (at your opinion)
// any later version. See COPYING.txt.

namespace StarryHud;

public class MainScript : Script
{
    private int _ticks;
    
    private readonly CompassHud _compass = new();
    private readonly ClockHud _clock = new();
    private readonly ObjectPool _pool = new();
    
    public MainScript()
    {
        _pool.Add(_compass);
        _pool.Add(_clock);
        
        Tick += OnTick;
        _compass.Initialize();
    }

    private void OnTick(object sender, EventArgs e)
    {
        _pool.Process();

        _ticks++;

        if (_ticks >= 16)
        {
            _compass.Update();
            _clock.Update();
            _ticks = 0;
        }
    }
}
