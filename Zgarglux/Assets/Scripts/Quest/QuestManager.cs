using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Quest;

public class QuestManager : MonoBehaviour
{
    public List<Quest> questList = new List<Quest>();

    public List<int> progress, totalNeeded;
    public List<bool> isActiveQuest;
    public List<questTypeEnum> questType;
    public List<questStateEnum> questState;
    public List<string> type;
    public List<string> questDescription;

    public TextMeshProUGUI TMPActiveQuest;

    public GameObject MenuQuest1;
    public GameObject MenuQuestLayout;
    // Start is called before the first frame update

    void OnEnable()
    {
        // Subscribe to the event when the object is enabled
        CropPlanting.EventOnCropPlanted += UpdateQuests;
        PlayerHarvesting.EventOnCropHarvested += UpdateQuests;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled
        CropPlanting.EventOnCropPlanted -= UpdateQuests;
        PlayerHarvesting.EventOnCropHarvested += UpdateQuests;
    }

    void Start()
    {
        if (progress.Count == totalNeeded.Count && progress.Count == isActiveQuest.Count && progress.Count == questType.Count && progress.Count == questState.Count && progress.Count == type.Count && progress.Count == questDescription.Count)
        {
            for (int i = 0; i < progress.Count; i++)
            {
                Quest quest = new Quest();
                quest.progress = progress[i];
                quest.totalNeeded = totalNeeded[i];
                quest.isActiveQuest = isActiveQuest[i];
                quest.questType = questType[i];
                quest.questState = questState[i];
                quest.type = type[i];
                quest.questDescription = questDescription[i];

                questList.Add(quest);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(questList[0].progress);
        ShowActiveQuest();
    }

    void ShowActiveQuest()
    {
        Quest activeQuest = SetActiveQuest();
        TMPActiveQuest.text = activeQuest.questDescription;
        MenuQuest1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = activeQuest.questDescription;
        MenuQuest1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = activeQuest.progress.ToString() + "/" + activeQuest.totalNeeded.ToString();

        // show progression of quest
    }

    public Quest SetActiveQuest()
    {
        List<Quest> activeQuests = new List<Quest>();
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i].isActiveQuest)
            {
                if (questList[i].questState == questStateEnum.Ongoing)
                {
                    activeQuests.Add(questList[i]);
                }
                else
                {
                    questList[i].isActiveQuest = false;
                }
            }
        }
        Debug.Log("activeQuests.count " + activeQuests.Count);
        if (activeQuests.Count > 1)
        {
            for(int i = 1; i < activeQuests.Count; i++)
            {
                activeQuests[i].isActiveQuest = false;
            }
            return activeQuests[0];
        }
        else if (activeQuests.Count < 1)
        {
            for (int i = 0; i < questList.Count; i++)
            {
                if (questList[i].questState == questStateEnum.Ongoing)
                {
                    questList[i].isActiveQuest = true;
                    return questList[i];
                }
            }
            Quest returnQuest = new Quest();
            returnQuest.questDescription = "No quest currently available";
            return returnQuest;
        }
        return activeQuests[0];
    }

    void SortQuests()
    {
        MenuQuest1.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = questList[0].questDescription;
        MenuQuest1.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = questList[0].progress.ToString() + "/" + questList[0].totalNeeded.ToString();
        for (int i = 1; i < questList.Count; i++)
        {
            GameObject quest = Instantiate(MenuQuest1, MenuQuestLayout.transform);
            quest.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = questList[i].questDescription;
            quest.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = questList[i].progress.ToString() + "/" + questList[i].totalNeeded.ToString();
        }
    }

    void UpdateQuests(int actionProgress, string action, string actionType)
    {
        Debug.Log("UpdateQuests");
        for(int i = 0; i < questList.Count; i++)
        {
            Debug.Log("inside for loop");
            if (questList[i].questState == questStateEnum.Ongoing)
            {
                Debug.Log("quest state is ongoing");
                questList[i].UpdateQuest(actionProgress, action, actionType);
                Debug.Log("Updated Quest");
            }
        }
    }
}
