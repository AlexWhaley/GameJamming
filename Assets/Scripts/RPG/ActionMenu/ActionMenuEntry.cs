using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuEntry : MenuEntry
{
    [SerializeField]
    private TextMesh _actionNameText;
    private Action _actionData;
    private ActionMenuController _actionMenuController;

    public void Initialise(Action actionData, ActionMenuController actionMenuController)
    {
        _actionData = actionData;
        _actionNameText.text = _actionData.ActionName;
        _actionMenuController = actionMenuController;
    }

    public override void SelectMenuEntry()
    {
        _actionMenuController.BeginActionSelection(_actionData);
    }
}