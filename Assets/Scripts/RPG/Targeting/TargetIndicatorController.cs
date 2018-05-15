using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicatorController : MonoBehaviour
{
    [SerializeField]
    private GameObject _pickIndicator;
    [SerializeField]
    private GameObject _nomeIndicator;
    [SerializeField]
    private GameObject _yamaIndicator;
    [SerializeField]
    private GameObject _fourIndicator;

    private void Awake()
    {
        InitialiseTargetIndicators();
    }

    private void InitialiseTargetIndicators()
    {
        // Set colours from some kind of manager?
    }

    public void SetActiveTargetingIndicator(PlayerCharacter.CharacterNames targetedBy, bool setActive)
    {
        switch (targetedBy)
        {
            case PlayerCharacter.CharacterNames.Pick:
                _pickIndicator.SetActive(setActive);
                break;
            case PlayerCharacter.CharacterNames.Nome:
                _nomeIndicator.SetActive(setActive);
                break;
            case PlayerCharacter.CharacterNames.Yama:
                _yamaIndicator.SetActive(setActive);
                break;
            case PlayerCharacter.CharacterNames.Four:
                _fourIndicator.SetActive(setActive);
                break;
        }
    }
}
