using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _attatchedPlayer;
    [SerializeField]
    private GameObject _menuActionObject;

    [Header("Action Menu Transforms")]
    [SerializeField]
    private Transform _attackMenu;
    [SerializeField]
    private Transform _assistMenu;
    [SerializeField]
    private Transform _healMenu;

    // Update is called once per frame
    private void Awake()
    {
        BuildActionMenus();
    }

    private void BuildActionMenus()
    {
        foreach (var attack in _attatchedPlayer.AttackActions)
        {
            CreateMenuAction(_attackMenu);
        }
        foreach (var assist in _attatchedPlayer.AssistActions)
        {
            CreateMenuAction(_assistMenu);
        }
        foreach (var attack in _attatchedPlayer.AttackActions)
        {
            CreateMenuAction(_healMenu);
        }
    }

    private void CreateMenuAction(Transform parent)
    {
        GameObject.Instantiate(_menuActionObject, parent);
    }
}
