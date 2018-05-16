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

    List<Character> _charactersTargetting;

    public void SetActiveTargetingIndicator(Character targetedBy, bool setActive)
    {
        if (targetedBy is PlayerCharacter)
        {
            switch (((PlayerCharacter)targetedBy).CharacterName)
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
        /*
        else if (targetedBy is EnemyCharacter)
        {

        }
        */
    }

    public bool IsTargetedBy(Character character)
    {
        return _charactersTargetting.Contains(character);
    }
}
