using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _attatchedPlayer;
    [SerializeField]
    private GameObject _menuEntryPrefab;

    [Header("Action Menu Containers")]
    [SerializeField]
    private GameObject _rootMenuContainer;
    [SerializeField]
    private GameObject _attackMenuContainer;
    [SerializeField]
    private GameObject _assistMenuContainer;
    [SerializeField]
    private GameObject _healMenuContainer;

    [Header("Root Menu Entries")]
    [SerializeField]
    private RootActionMenuEntry _attackMenuEntry;
    [SerializeField]
    private RootActionMenuEntry _assistMenuEntry;
    [SerializeField]
    private RootActionMenuEntry _healMenuEntry;

    private ActionMenus _currentMenu;
    private MenuEntry _highlightedMenuEntry;
    //private ActionSelectionProcessor _actionSelectionProcessor;

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
        //_actionSelectionProcessor = GetComponent<ActionSelectionProcessor>();
        InitialiseActionMenu();
    }

    private void InitialiseActionMenu()
    {
        _menus = new Dictionary<ActionMenus, List<MenuEntry>>();
        BuildRootMenu();
        BuildActionMenus();
        InitialiseMenuState();
    }

    private void BuildRootMenu()
    {
        List<MenuEntry> rootMenu = new List<MenuEntry>();
        // Initialising attack menu entry in the root menu
        _attackMenuEntry.Initialise(ActionMenus.Attack, this);
        rootMenu.Add(_attackMenuEntry);

        // Initialising assist menu entry in the root menu
        _assistMenuEntry.Initialise(ActionMenus.Assist, this);
        rootMenu.Add(_assistMenuEntry);

        // Initialising assist menu entry in the root menu
        _healMenuEntry.Initialise(ActionMenus.Heal, this);
        rootMenu.Add(_healMenuEntry);

        _menus.Add(ActionMenus.Root, rootMenu);
    }

    private void BuildActionMenus()
    {
        foreach (var attack in _attatchedPlayer.AttackActions)
        {
            CreateMenuAction(attack, _attackMenuContainer.transform);
        }
        foreach (var assist in _attatchedPlayer.AssistActions)
        {
            CreateMenuAction(assist, _assistMenuContainer.transform);
        }
        foreach (var heal in _attatchedPlayer.HealActions)
        {
            CreateMenuAction(heal, _healMenuContainer.transform);
        }
    }

    private MenuEntry CreateMenuAction(Action action, Transform parent)
    {
        GameObject menuEntryObject = GameObject.Instantiate(_menuEntryPrefab, parent);
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
                _rootMenuContainer.SetActive(false);
                _attackMenuContainer.SetActive(true);
                break;
            case ActionMenus.Assist:
                _rootMenuContainer.SetActive(false);
                _assistMenuContainer.SetActive(true);
                break;
            case ActionMenus.Heal:
                _rootMenuContainer.SetActive(false);
                _healMenuContainer.SetActive(true);
                break;
            case ActionMenus.Root:
                DisableActionMenus();
                _rootMenuContainer.SetActive(true);
                break;
        }
        _currentMenu = destinationMenu;
    }

    private void DisableActionMenus()
    {
        if (_attackMenuContainer.activeSelf) { _attackMenuContainer.SetActive(false); }
        if (_assistMenuContainer.activeSelf) { _assistMenuContainer.SetActive(false); }
        if (_healMenuContainer.activeSelf) { _healMenuContainer.SetActive(false); }
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
