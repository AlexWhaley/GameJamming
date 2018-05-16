using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionMenuEntry : MenuEntry
{
    private Action _actionData;

    public void Initialise(Action actionData, ActionMenuController actionMenuController)
    {
        _actionData = actionData;
        _actionNameText.text = _actionData.ActionName;
        _actionMenuController = actionMenuController;
        _entryDescription = _actionData.ActionDescription;
    }

    public override void SelectMenuEntry()
    {
        _actionMenuController.BeginActionSelection(_actionData);
    }
}