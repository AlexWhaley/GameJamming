using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteViewModel : MonoBehaviour
{
    private Image _noteImage;
    private PlayerID _playerID;
    private Note _noteData;
    private Lane _lane;
    private Vector2 _spawnPosition;
    private Vector2 _destructPosition;

    private void Awake()
    {
        _noteImage = GetComponent<Image>();
    }

    public void Initialize(PlayerID playerID, Note noteData, Lane lane, Vector2 spawn, Vector2 destruct)
    {
        _playerID = playerID;
        _noteData = noteData;
        _lane = lane;

        transform.Rotate(0, 0, (int)noteData.Direction * -90);

        _noteImage.sprite = AssetManager.Instance.GetNoteSprite(playerID, lane);

        _spawnPosition = spawn;
        _destructPosition = destruct;
    }

    private void Update()
    {
        if (TrackManager.Instance.PlayingTrack)
        {
            float trackTime = ((TrackManager.Instance.BeatsShownInAdvance - (_noteData.StartTime - AudioManager.Instance.SongPosition)) / TrackManager.Instance.BeatsShownInAdvance);

            transform.position = Vector2.LerpUnclamped(
                _spawnPosition,
                _destructPosition,
                trackTime
            );
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_playerID == PlayerID.Player1)
        {
            if (_noteData.Direction == Direction.Left && GameManager.Instance.Player1LeftPressed)
            {
                Debug.Log("NOTE HIT");
            }
        }
    }
}
