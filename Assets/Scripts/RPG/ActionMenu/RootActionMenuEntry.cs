using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootActionMenuEntry : MenuEntry
{
    ActionMenuController.ActionMenus _rootMenuType;
    [SerializeField]
    private string _rootEntryDescription;

    public void Initialise(ActionMenuController.ActionMenus rootMenuType, ActionMenuController actionMenuController)
    {
        _rootMenuType = rootMenuType;
        _actionMenuController = actionMenuController;
        _entryDescription = _rootEntryDescription;
    }

    public override void SelectMenuEntry()
    {
        _actionMenuController.NavigateToActionMenu(_rootMenuType);
    }
}