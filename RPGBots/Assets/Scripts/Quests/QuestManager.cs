using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestPanel _questPanel;
    [SerializeField] List<Quest> _allQuests;

    List<Quest> _activeQuests = new List<Quest>();

    public static QuestManager Instance { get; private set; }

    void Awake() => Instance = this;

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }

    public void AddQuestByName(string questName)
    {
        var quest = _allQuests.FirstOrDefault(t => t.name == questName);
        if (quest != null)
            AddQuest(quest);
        else
            Debug.LogError($"Missing quest {questName} attempted to add from dialog");

    }
}
