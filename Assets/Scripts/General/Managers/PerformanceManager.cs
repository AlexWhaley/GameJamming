using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    [SerializeField]
    List<TrackViewModel> _trackViewmodels;


    private static PerformanceManager _instance;
    public static PerformanceManager Instance
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

    public float GetCharacterPerformance(PlayerCharacter character)
    {
        switch (character.CharacterName)
        {
            case PlayerCharacter.CharacterNames.Pick:
                return (float)_trackViewmodels[0].NotesHit / _trackViewmodels[0].TotalNotes;
            case PlayerCharacter.CharacterNames.Nome:
                return (float)_trackViewmodels[0].NotesHit / _trackViewmodels[1].TotalNotes;
            case PlayerCharacter.CharacterNames.Four:
                return (float)_trackViewmodels[0].NotesHit / _trackViewmodels[2].TotalNotes;
            case PlayerCharacter.CharacterNames.Yama:
                return (float)_trackViewmodels[0].NotesHit / _trackViewmodels[3].TotalNotes;
            default:
                return 1.0f;
        }
    }
}
