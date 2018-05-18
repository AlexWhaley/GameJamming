using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuEntry : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI _actionNameText;
    [SerializeField]
    protected GameObject _highlight;

    protected ActionMenuController _actionMenuController;
    protected string _entryDescription;

    public void HighlightMenuEntry()
    {
        _actionNameText.fontStyle = FontStyles.Underline;
        _actionMenuController.SetActionDescriptionText(_entryDescription);
        _highlight.SetActive(true);
    }

    public void UnHighlightMenuEntry()
    {
        _actionNameText.fontStyle = FontStyles.Normal;
        _highlight.SetActive(false);
    }

    public virtual void SelectMenuEntry() { }
}
