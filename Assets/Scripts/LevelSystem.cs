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
    private int experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 10;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        if(experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if(OnLevelChange != null) OnLevelChange(this, EventArgs.Empty);
        }
        if(OnExperienceChange != null) OnExperienceChange(this, EventArgs.Empty);
    }
    
    public int GetLevelNumber()
    {
        return level;
    }
    public int GetExperience()
    {
        return experience;
    }

    public int GetExperienceToNextLevel()
    {
        return experienceToNextLevel;
    }
}
