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

    int _currentStepIndex;

    
    public List<Step> Steps;
    public string Description => _description;
    public string DisplayName => _displayName;
    public Sprite Sprite => _sprite;

    internal void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if (currentStep.HasAllObjectivesCompleted())
        {
            _currentStepIndex++;
            // do whatever we do when a quest progresses  like update the UI
        }
    }


    Step GetCurrentStep() => Steps[_currentStepIndex];
}

[Serializable]
public class Step
{
    [SerializeField] string _instructions;
    public string Instructions => _instructions;
    public List<Objective> Objectives;

    public bool HasAllObjectivesCompleted()
    {
        return Objectives.TrueForAll(t => t.IsCompleted);
    }
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

    public bool IsCompleted { get; }

    public override string ToString()
    {
        return _objectiveType.ToString();
    }
}