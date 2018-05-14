using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootActionMenuEntry : MenuEntry
{
    ActionMenuController.ActionMenus _rootMenuType;
    private ActionMenuController _actionMenuController;

    public void Initialise(ActionMenuController.ActionMenus rootMenuType, ActionMenuController actionMenuController)
    {
        _rootMenuType = rootMenuType;
        _actionMenuController = actionMenuController;
    }

    public override void SelectMenuEntry()
    {
        _actionMenuController.NavigateToActionMenu(_rootMenuType);
    }
}