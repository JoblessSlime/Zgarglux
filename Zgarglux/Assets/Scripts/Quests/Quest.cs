using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Quest : MonoBehaviour
{
    public enum questTypeEnum
    {
        PlantCrops, GrowCrops, DefeatMonster
    }
    public enum questStateEnum
    {
        Won, Lost, Ongoing
    }

    public string questDescription;
    public int progress, totalNeeded;
    public bool isActiveQuest;
    public questTypeEnum questType;
    public questStateEnum questState;
    public string type;
    // quest giver

    // Update is called once per frame
    void Update()
    {
        // win quest
        if(progress >= totalNeeded)
        {
            isActiveQuest = false;
            questState = questStateEnum.Won;
        }

        // lose
    }

    public void UpdateQuest(int actionProgress, string action, string actionType)
    {
        if (questType == questTypeEnum.PlantCrops && action == "PlantCrops")
        {
            if(type == "any")
            {
                progress += actionProgress;
            }
            else if(type == actionType)
            {
                progress += actionProgress;
            }
        }
        else if (questType == questTypeEnum.GrowCrops && action == "GrowCrops")
        {
            if (type == "any")
            {
                progress += actionProgress;
            }
            else if (type == actionType)
            {
                progress += actionProgress;
            }
        }
        else if (questType == questTypeEnum.DefeatMonster && action == "DefeatMonster")
        {
            if (type == "any")
            {
                progress += actionProgress;
            }
            else if (type == actionType)
            {
                progress += actionProgress;
            }
        }

        if (progress >= totalNeeded)
        {
            isActiveQuest = false;
            questState = questStateEnum.Won;
        }
    }
}
