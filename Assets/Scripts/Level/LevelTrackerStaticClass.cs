using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelTrackerStaticClass
{

    public static int updateHackTracker = 0;

    public static int levelNum = 0; // what level are they on? (what level is unlocked for them)

    public static int currentLevel = 0; // regardless of what they unlocked, what are they on (could go back to previous levels)
}
