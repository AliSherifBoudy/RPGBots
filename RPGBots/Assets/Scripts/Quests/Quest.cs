using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string _displayName;
    [SerializeField] string _description;

    [Tooltip("Designer/programmer notes, not visable to player.")]
    [SerializeField] string _notes;
    [SerializeField] Sprite _sprite;

    
    public List<Step> Steps;
    public string Description => _description;
    public string DisplayName => _displayName;
    public Sprite Sprite => _sprite;
}

[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public string Instructions => _instructions;
    public List<Objective> Objectives;
}

[Serializable]
public class Objective
{
    [SerializeField] ObjectiveType _objectiveType;
    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }

    public override string ToString()
    {
        return _objectiveType.ToString();
    }
}