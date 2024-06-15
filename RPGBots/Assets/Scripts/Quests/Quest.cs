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
                if (objective.GameFlag != null)
                    objective.GameFlag.Changed += HandleFlagChanged;
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
    [SerializeField] GameFlag _gameFlag;

    public GameFlag GameFlag => _gameFlag;

    public enum ObjectiveType
    {
        Flag,
        Item,
        Kill,
    }

    public bool IsCompleted 
    {   get 
        {
            switch (_objectiveType)
            {
                case ObjectiveType.Flag: return _gameFlag.Value;
                default: return false;
            }
        } 
    }

    public override string ToString()
    {
        switch (_objectiveType)
        {
            case ObjectiveType.Flag: return _gameFlag.name;
            default: return _objectiveType.ToString();
        }
    } 
}