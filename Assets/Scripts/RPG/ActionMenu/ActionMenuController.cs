using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter attatchedPlayer;

    // Menu transforms
    [SerializeField]
    private readonly Transform _attackMenu;
    [SerializeField]
    private readonly Transform _assistMenu;
    [SerializeField]
    private readonly Transform _healMenu;

    // Update is called once per frame
    private void Awake()
    {
        BuildActionMenus();
    }

    private void BuildActionMenus()
    {
        foreach (var attack in attatchedPlayer.AttackActions)
        {
            CreateMenuAction(_attackMenu);
        }
        foreach (var assist in attatchedPlayer.AssistActions)
        {
            CreateMenuAction(_assistMenu);
        }
        foreach (var attack in attatchedPlayer.AttackActions)
        {
            CreateMenuAction(_healMenu);
        }
    }

    private void CreateMenuAction(Transform parent)
    {

    }
}
