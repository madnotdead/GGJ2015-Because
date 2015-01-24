using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum PlayerState
{
    Idle,
    Active,
    Working,
    Returning,
    Unconscious
}

public enum PlayerTaskType
{
    Singing,
    FixingConsole,
    HoldingWall,
    CatchingCeiling,
    CatchingLight,
    ExtinguishingFire
}

public enum PlayerMoodTypes
{
    Normal,
    Worried,
    Afraid,
    Happy,
}
