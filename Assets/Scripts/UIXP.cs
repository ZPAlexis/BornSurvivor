using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIXP : MonoBehaviour
{
    public Slider slider;
    public TMPro.TextMeshProUGUI levelText;
    private LevelSystem levelSystem;

    private void Awake()
    {
        levelText = transform.Find("LevelText").GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void SetXPBarSize(int experienceToNextLevel, int experience)
    {
        slider.maxValue = experienceToNextLevel;
        slider.value = experience;
    }

    public void SetXP(int experience)
    {
        slider.value = experience;
    }

    public void SetLevelNumber(int levelNumber)
    {
        levelText.text = "LEVEL " + (levelNumber + 1);
    }

    public void SetLevelSystem(LevelSystem system)
    {
        this.levelSystem = system;

        SetLevelNumber(levelSystem.GetLevelNumber());
        SetXPBarSize(levelSystem.GetExperienceToNextLevel(), levelSystem.GetExperience());

        levelSystem.OnExperienceChange += LevelSystem_OnExperienceChange;
        levelSystem.OnLevelChange += LevelSystem_OnLevelChange;
    }

    private void LevelSystem_OnExperienceChange(object sender, System.EventArgs e)
    {
        SetXP(levelSystem.GetExperience());
    }

    private void LevelSystem_OnLevelChange(object sender, System.EventArgs e)
    {
        SetXPBarSize(levelSystem.GetExperienceToNextLevel(), levelSystem.GetExperience());
        SetLevelNumber(levelSystem.GetLevelNumber());
    }

}
