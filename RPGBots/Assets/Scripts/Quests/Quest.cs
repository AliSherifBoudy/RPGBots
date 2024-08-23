using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest")]
public class Quest : ScriptableObject
{
    public event Action Changed;

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

    public Step CurrentStep => Steps[_currentStepIndex];

    void OnEnable()
    {
        _currentStepIndex = 0;
        foreach (var step in Steps)
            foreach (var objective in step.Objectives)
            {
                if (objective.BoolGameFlag != null)
                    objective.BoolGameFlag.Changed += HandleFlagChanged;
                if (objective.IntGameFlag != null)
                    objective.IntGameFlag.Changed += HandleFlagChanged;
            }
    }

    void HandleFlagChanged()
    {
        TryProgress();
        Changed?.Invoke();
    }

    internal void TryProgress()
    {
        var currentStep = GetCurrentStep();
        if (currentStep.HasAllObjectivesCompleted())
        {
            _currentStepIndex++;
            Changed?.Invoke();
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
    [SerializeField] BoolGameFlag _boolGameFlag;

    [Header("Int Game Flags")]
    [SerializeField] IntGameFlag _intGameFlag;

    [Tooltip("Required amount for the counted integer game flag.")]
    [SerializeField] int _required;

    public BoolGameFlag BoolGameFlag => _boolGameFlag;
    public IntGameFlag IntGameFlag => _intGameFlag;

    public enum ObjectiveType
    {
        BoolFlag,
        CountedIntFlag,
        Item,
        Kill,
    }

    public bool IsCompleted 
    {   get 
        {
            switch (_objectiveType)
            {
                case ObjectiveType.BoolFlag: return _boolGameFlag.Value;
                case ObjectiveType.CountedIntFlag: return _intGameFlag.Value >= _required;
                default: return false;
            }
        } 
    }

    public override string ToString()
    {
        switch (_objectiveType)
        {
            case ObjectiveType.BoolFlag: return _boolGameFlag.name;
            case ObjectiveType.CountedIntFlag: return $"{_intGameFlag.name} ({_intGameFlag.Value}/{_required})";
            default: return _objectiveType.ToString();
        }
    } 
}