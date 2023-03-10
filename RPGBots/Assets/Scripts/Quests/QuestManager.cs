using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestPanel _questPanel;

    List<Quest> _activeQuests = new List<Quest>();
    public static QuestManager Instance { get; private set; }

    void Awake() => Instance = this;

    public void AddQuest(Quest quest)
    {
        _activeQuests.Add(quest);
        _questPanel.SelectQuest(quest);
    }
}
