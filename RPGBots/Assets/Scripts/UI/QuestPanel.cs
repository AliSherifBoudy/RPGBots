using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestPanel : ToggleablePanel
{
    [SerializeField] Quest _selectedQuest;
    [SerializeField] TMP_Text _nameText;
    [SerializeField] TMP_Text _descriptionText;
    [SerializeField] TMP_Text _currentObjectivesText;
    [SerializeField] Image _iconImage;

    Step _selectedStep => _selectedQuest.CurrentStep;

    [ContextMenu("Bind")]
    void Bind()
    {

        _iconImage.sprite = _selectedQuest.Sprite;
        _nameText.SetText(_selectedQuest.DisplayName);
        _descriptionText.SetText(_selectedQuest.Description);

        DisplayStepInstructionsAndObjectives();
    }

    void DisplayStepInstructionsAndObjectives()
    {
        StringBuilder builder = new StringBuilder();
        if (_selectedStep != null)
        {
            builder.AppendLine(_selectedStep.Instructions);
            foreach (var objective in _selectedStep.Objectives)
            {
                string rgb = objective.IsCompleted ? "green" : "red";
                builder.AppendLine($"<color={rgb}>{objective}</color>");
            }
        }

        _currentObjectivesText.SetText(builder.ToString());
    }

    public void SelectQuest(Quest quest)
    {
        if(_selectedQuest)
            _selectedQuest.Changed -= DisplayStepInstructionsAndObjectives;

        _selectedQuest = quest;
        Bind();
        Show();

        _selectedQuest.Changed += DisplayStepInstructionsAndObjectives;
    }
}
