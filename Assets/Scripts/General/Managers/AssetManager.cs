using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public static AssetManager Instance;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> LeftLaneNote;
    [SerializeField] private List<Sprite> RightLaneNote;

    [SerializeField] private List<Sprite> LeftLaneTail;
    [SerializeField] private List<Sprite> RightLaneTail;

    [Header("Prefabs")]
    public GameObject NotePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetNoteSprite(PlayerCharacter.CharacterNames character, Lane lane)
    {
        switch (lane)
        {
            case Lane.Left:
                return LeftLaneNote[(int)character];
            case Lane.Right:
                return RightLaneNote[(int)character];
        }

        return null;
    }

    public Sprite GetTailSprite(PlayerCharacter.CharacterNames character, Lane lane)
    {
        switch (lane)
        {
            case Lane.Left:
                return LeftLaneTail[(int)character];
            case Lane.Right:
                return RightLaneTail[(int)character];
        }

        return null;
    }
}
