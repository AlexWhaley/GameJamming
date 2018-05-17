using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{

    public static AssetManager Instance;

    [Header("Sprites")]
    [SerializeField] private List<Sprite> LeftLaneNote;
    [SerializeField] private List<Sprite> RightLaneNote;

    [Header("Prefabs")]
    public GameObject NotePrefab;

    private void Awake()
    {
        Instance = this;
    }

    public Sprite GetNoteSprite(int playerIndex, Lane lane)
    {
        switch (lane)
        {
            case Lane.Left:
                return LeftLaneNote[playerIndex];
            case Lane.Right:
                return RightLaneNote[playerIndex];
        }

        return null;
    }
}
