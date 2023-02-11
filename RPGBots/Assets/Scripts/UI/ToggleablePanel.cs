using System.Collections.Generic;
using UnityEngine;

public class ToggleablePanel : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    static HashSet<ToggleablePanel> _visablePanels = new HashSet<ToggleablePanel>();
    public static bool IsVisible => _visablePanels.Count > 0;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show()
    {
        _visablePanels.Add(this);
        _canvasGroup.alpha = 0.5f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
    public void Hide()
    {
        _visablePanels.Remove(this);
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}