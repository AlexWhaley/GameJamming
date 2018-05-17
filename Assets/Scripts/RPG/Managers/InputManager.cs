using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private List<CharacterInputMapping> _characterInputMappings;
    private Dictionary<Character, PlayerInputHandler> _characterInputDictionary;


    private static InputManager _instance;
    public static InputManager Instance
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
            Initialise();
        }
    }

    private void Initialise()
    {
        _characterInputDictionary = new Dictionary<Character, PlayerInputHandler>();
        foreach (var characterInputMapping in _characterInputMappings)
        {
            _characterInputDictionary[characterInputMapping.Character] = characterInputMapping.PlayerInputHandler;
        }
    }

    public PlayerInputHandler GetPlayerInputHandler(Character attatchedCharacter)
    {
        PlayerInputHandler attachedInputHandler = null;
        if (!_characterInputDictionary.TryGetValue(attatchedCharacter, out attachedInputHandler))
        {
            Debug.LogError(string.Format("There is no input handler for {0}", attatchedCharacter.name));
        }

        return attachedInputHandler;
    }
}

[Serializable]
public class CharacterInputMapping
{
    public Character Character;
    public PlayerInputHandler PlayerInputHandler;
}
