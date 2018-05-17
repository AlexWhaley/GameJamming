using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionMenuController : MonoBehaviour, InputCommandHandler
{
    private PlayerUIController _uiController;
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

    [Space(10), SerializeField]
    private TextMeshProUGUI _menuTitle;
    [SerializeField]
    private TextMeshProUGUI _actionDescription;

    [SerializeField] private GameObject _readyMessage;
    [SerializeField] private Image _borderImage;

    private ActionMenus _currentMenu;
    private int _currentSelectionIndex;
    private int _rootSelectionIndex;
     
    public enum ActionMenus
    {
        Root,
        Attack,
        Assist,
        Heal
    }

    private Dictionary<ActionMenus, List<MenuEntry>> _menus;

    public void InitialisePlayer(PlayerCharacter playerCharacter)
    {
        _attatchedPlayer = playerCharacter;
    }

    private void Awake()
    {
        _uiController = GetComponentInParent<PlayerUIController>();
        _attatchedPlayer = _uiController.AttatchedPlayer;
        _borderImage.color = _uiController.HudColor;

    }

    private void Start()
    {
        BuildActionMenu();
        RevealActionMenu(true);
    }

    private void BuildActionMenu()
    {
        _menus = new Dictionary<ActionMenus, List<MenuEntry>>();
        BuildRootMenu();
        BuildActionMenus();
    }

    public void RevealActionMenu(bool freshOpen)
    {
        if (freshOpen)
        {
            NavigateToActionMenu(ActionMenus.Root, true);
        }
        else
        {
            NavigateToActionMenu(_currentMenu, false);
        }
        gameObject.SetActive(true);
        RegisterInputHandlers();
    }

    private void HideActionMenu()
    {
        gameObject.SetActive(false);
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
        List<MenuEntry> _attackEntries = new List<MenuEntry>();
        foreach (var attack in _attatchedPlayer.AttackActions)
        {
            _attackEntries.Add(CreateMenuAction(attack, _attackMenuContainer.transform));
        }
        _menus.Add(ActionMenus.Attack, _attackEntries);

        List<MenuEntry> _assistEntries = new List<MenuEntry>();
        foreach (var assist in _attatchedPlayer.AssistActions)
        {
            _assistEntries.Add(CreateMenuAction(assist, _assistMenuContainer.transform));
        }
        _menus.Add(ActionMenus.Assist, _assistEntries);

        List<MenuEntry> _healEntries = new List<MenuEntry>();
        foreach (var heal in _attatchedPlayer.HealActions)
        {
            _healEntries.Add(CreateMenuAction(heal, _healMenuContainer.transform));
        }
        _menus.Add(ActionMenus.Heal, _healEntries);
    }

    private MenuEntry CreateMenuAction(Action action, Transform parent)
    {
        GameObject menuEntryObject = GameObject.Instantiate(_menuEntryPrefab, parent);
        var menuEntry = menuEntryObject.GetComponent<ActionMenuEntry>();
        menuEntry.Initialise(action, this);

        return menuEntry;
    }

    public void NavigateToActionMenu(ActionMenus destinationMenu, bool resetSelection)
    {
        _currentMenu = destinationMenu;
        if (resetSelection)
        {
            _currentSelectionIndex = 0;
        }
        
        switch (destinationMenu)
        {
            case ActionMenus.Attack:
                _rootMenuContainer.SetActive(false);
                _attackMenuContainer.SetActive(true);
                _menuTitle.text = "Attack Menu";
                break;
            case ActionMenus.Assist:
                _rootMenuContainer.SetActive(false);
                _assistMenuContainer.SetActive(true);
                _menuTitle.text = "Assist Menu";
                break;
            case ActionMenus.Heal:
                _rootMenuContainer.SetActive(false);
                _healMenuContainer.SetActive(true);
                _menuTitle.text = "Heal Menu";
                break;
            case ActionMenus.Root:
                _currentSelectionIndex = _rootSelectionIndex;
                DisableActionMenus();
                _rootMenuContainer.SetActive(true);
                _menuTitle.text = "Action Menu";
                break;
        }
        _readyMessage.SetActive(false);
        HighlightedMenuEntry.HighlightMenuEntry();
    }

    private void DisableActionMenus()
    {
        if (_attackMenuContainer.activeSelf) { _attackMenuContainer.SetActive(false); }
        if (_assistMenuContainer.activeSelf) { _assistMenuContainer.SetActive(false); }
        if (_healMenuContainer.activeSelf) { _healMenuContainer.SetActive(false); }
    }

    private MenuEntry HighlightedMenuEntry
    {
        get
        {
            return _menus[_currentMenu][_currentSelectionIndex];
        }
    }

    public void SetActionDescriptionText(string newDescription)
    {
        _actionDescription.text = newDescription;
    }

    public void BeginActionSelection(Action selectedAction)
    {
        UnregisterInputHandlers();
        _uiController.InitiateTargettingSequence(selectedAction);

    }

    public void DisplayReadyMessage()
    {
        DisableActionMenus();
        _readyMessage.SetActive(true);
    }


    public void HideReadyMessage()
    {
        NavigateToActionMenu(_currentMenu, false);
    }

    public void RegisterInputHandlers()
    {
        var playerInputHandler = InputManager.Instance.GetPlayerInputHandler(_attatchedPlayer);
        playerInputHandler.ConfirmButtonPress += HandleConfirmButton;
        playerInputHandler.BackButtonPress += HandleBackButton;
        playerInputHandler.UpButtonPress += HandleUpButton;
        playerInputHandler.DownButtonPress += HandleDownButton;
    }

    public void UnregisterInputHandlers()
    {
        var playerInputHandler = InputManager.Instance.GetPlayerInputHandler(_attatchedPlayer);
        playerInputHandler.ConfirmButtonPress -= HandleConfirmButton;
        playerInputHandler.BackButtonPress -= HandleBackButton;
        playerInputHandler.UpButtonPress -= HandleUpButton;
        playerInputHandler.DownButtonPress -= HandleDownButton;

    }

    // Menu controls
    public void HandleBackButton()
    {
        if (_currentMenu != ActionMenus.Root)
        {
            HighlightedMenuEntry.UnHighlightMenuEntry();
            NavigateToActionMenu(ActionMenus.Root, true);
        }
    }

    public void HandleConfirmButton()
    {
        HighlightedMenuEntry.SelectMenuEntry();
    }
    
    public void HandleUpButton()
    {
        StepThroughActionMenu(-1);
    }

    public void HandleDownButton()
    {
        StepThroughActionMenu(1);
    }

    public void HandleLeftButton()
    {
    }
    public void HandleRightButton()
    {
    }

    private void StepThroughActionMenu(int indexChange)
    {
        var currentEntryList = _menus[_currentMenu];
        currentEntryList[_currentSelectionIndex].UnHighlightMenuEntry();

        int entryCount = _menus[_currentMenu].Count;
        _currentSelectionIndex = (((_currentSelectionIndex + indexChange) % entryCount) + entryCount) % entryCount;
        currentEntryList[_currentSelectionIndex].HighlightMenuEntry();

        if (_currentMenu == ActionMenus.Root)
        {
            _rootSelectionIndex = _currentSelectionIndex;
        }
    }
}
