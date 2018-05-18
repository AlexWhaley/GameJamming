using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<PlayerUIController> _playerUIControllers;
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            return _instance;
        }
    }
    
    private void Awake()
    {
        // Wont need this just for clarification.
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // If the singleton hasn't been initialized yet
            _instance = this;
        }
    }

    private void Start()
    {
        PhaseManager.Instance.ActionSelectionAndEnemyAttacksStarted += RevealAllActionMenus;
        PhaseManager.Instance.IntroActionSelectionStarted += RevealAllActionMenus;

        PhaseManager.Instance.ActionSelectionAndEnemyAttacksFinished += EndAllActionSelection;
        PhaseManager.Instance.IntroActionSelectionFinished += EndAllActionSelection;
    }

    public void EndAllActionSelection()
    {
        foreach (var uiController in _playerUIControllers)
        {
            uiController.EndActionSelection();

        }
    }

    public void RevealAllActionMenus()
    {
        foreach (var uiController in _playerUIControllers)
        {
            uiController.OpenActionMenu(true);
        }
    }
}