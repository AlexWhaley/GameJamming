using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _attatchedPlayer;
    [SerializeField]
    private GameObject _menuActionObject;

    [Header("Action Menus")]
    [SerializeField]
    private GameObject _rootMenu;
    [SerializeField]
    private GameObject _attackMenu;
    [SerializeField]
    private GameObject _assistMenu;
    [SerializeField]
    private GameObject _healMenu;

    private ActionMenus _currentMenu;
    private MenuEntry _highlightedMenuEntry;
    private ActionSelectionProcessor _actionSelectionProcessor;

    public enum ActionMenus
    {
        Root,
        Attack,
        Assist,
        Heal
    }

    private Dictionary<ActionMenus, List<MenuEntry>> _menus;

    private void Awake()
    {
        BuildRootMenu();
        BuildActionMenus();
        _actionSelectionProcessor = GetComponent<ActionSelectionProcessor>();

        InitialiseMenuState();
    }

    private void BuildRootMenu()
    {
        List<MenuEntry> rootMenu = new List<MenuEntry>();
        // Initialising attack menu entry in the root menu
        RootActionMenuEntry attackMenuEntry = _attackMenu.GetComponent<RootActionMenuEntry>();
        attackMenuEntry.Initialise(ActionMenus.Attack, this);
        rootMenu.Add(attackMenuEntry);

        // Initialising assist menu entry in the root menu
        RootActionMenuEntry assistMenuEntry = _assistMenu.GetComponent<RootActionMenuEntry>();
        attackMenuEntry.Initialise(ActionMenus.Assist, this);
        rootMenu.Add(assistMenuEntry);

        // Initialising assist menu entry in the root menu
        RootActionMenuEntry healMenuEntry = _assistMenu.GetComponent<RootActionMenuEntry>();
        healMenuEntry.Initialise(ActionMenus.Heal, this);
        rootMenu.Add(healMenuEntry);

        _menus.Add(ActionMenus.Root, rootMenu);
    }

    private void BuildActionMenus()
    {
        foreach (var attack in _attatchedPlayer.AttackActions)
        {
            CreateMenuAction(attack, _attackMenu.transform);
        }
        foreach (var assist in _attatchedPlayer.AssistActions)
        {
            CreateMenuAction(assist, _assistMenu.transform);
        }
        foreach (var heal in _attatchedPlayer.HealActions)
        {
            CreateMenuAction(heal, _healMenu.transform);
        }
    }

    private MenuEntry CreateMenuAction(Action action, Transform parent)
    {
        GameObject menuEntryObject = GameObject.Instantiate(_menuActionObject, parent);
        var menuEntry = menuEntryObject.GetComponent<ActionMenuEntry>();
        menuEntry.Initialise(action, this);

        return menuEntry;
    }

    private void InitialiseMenuState()
    {
        DisableActionMenus();
        _currentMenu = ActionMenus.Root;
    }

    public void NavigateToActionMenu(ActionMenus destinationMenu)
    {
        switch (destinationMenu)
        {
            case ActionMenus.Attack:
                _rootMenu.SetActive(false);
                _attackMenu.SetActive(true);
                break;
            case ActionMenus.Assist:
                _rootMenu.SetActive(false);
                _assistMenu.SetActive(true);
                break;
            case ActionMenus.Heal:
                _rootMenu.SetActive(false);
                _healMenu.SetActive(true);
                break;
            case ActionMenus.Root:
                DisableActionMenus();
                _rootMenu.SetActive(true);
                break;
        }
        _currentMenu = destinationMenu;
    }

    private void DisableActionMenus()
    {
        if (_attackMenu.activeSelf) { _attackMenu.SetActive(false); }
        if (_assistMenu.activeSelf) { _assistMenu.SetActive(false); }
        if (_healMenu.activeSelf) { _healMenu.SetActive(false); }
    }

    public void BeginActionSelection(Action selectedAction)
    {

    }

    public void HandleBackButton()
    {
        if (_currentMenu != ActionMenus.Root)
        {
            NavigateToActionMenu(ActionMenus.Root);
        }
    }

    public void HandleConfirmButton()
    {
        _highlightedMenuEntry.SelectMenuEntry();
    }
}
