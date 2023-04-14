using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChange;
    public event EventHandler OnLevelChange;
    private int level;
    private int experience;
    private int[] experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = new int[] {10, 25, 45, 70, 100, 135, 180, 225, 280, 340, 390, 440, 495, 550, 605, 665, 730, 795, 860, 930, 1005, 1080, 1160, 1240, 1325, 1410, 1495, 1590, 1680, 1815, 1835, 1850, 1865, 1875, 1890, 1895, 1900, 1930, 2035, 3740, 4510, 5330, 6190, 7090, 8050, 9050, 10100, 11200, 12350};
    }

    public void AddExperience(int amount)
    {
        if(level >= experienceToNextLevel.Length)
            return;
        experience += amount;
        while(experience >= experienceToNextLevel[level])
        {
            level++;
            experience -= experienceToNextLevel[level - 1];
            if(OnLevelChange != null) OnLevelChange(this, EventArgs.Empty);
            if(level >= experienceToNextLevel.Length)
                break;
        }
        if(OnExperienceChange != null) OnExperienceChange(this, EventArgs.Empty);
    }
    
    public int GetLevelNumber()
    {
        return level;
    }
    public int GetExperience()
    {
        if(level == experienceToNextLevel.Length)
            return 0;
        return experience;
    }

    public int GetExperienceToNextLevel()
    {
        if(level == experienceToNextLevel.Length)
            return 0;
        return experienceToNextLevel[level];
    }
}
