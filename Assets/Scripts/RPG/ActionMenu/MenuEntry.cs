using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuEntry : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI _actionNameText;

    protected ActionMenuController _actionMenuController;
    protected string _entryDescription;

    public void HighlightMenuEntry()
    {
        _actionNameText.fontStyle = FontStyles.Underline;
        _actionMenuController.SetActionDescriptionText(_entryDescription);
    }

    public void UnHighlightMenuEntry()
    {
        _actionNameText.fontStyle = FontStyles.Normal;
    }

    public virtual void SelectMenuEntry() { }
}
