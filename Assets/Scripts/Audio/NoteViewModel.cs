using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteViewModel : MonoBehaviour
{
    [SerializeField] private Trigger2DCallback _noteTrigger;
    [SerializeField] private Trigger2DCallback _tailTrigger;
    [SerializeField] private Image _noteImage;
    [SerializeField] private Image _tailImage;

    private PlayerCharacter _attachedCharacter;
    private Note _noteData;
    private Lane _lane;
    private Vector2 _spawnPosition;
    private Vector2 _destructPosition;

    private LaneViewModel _laneViewModel;

    private bool _hasBeenHit = false;
    public bool HasBeenHit
    {
        get
        {
            return _hasBeenHit;
        }
        set
        {
            _hasBeenHit = value;

            if (_hasBeenHit)
            {
                if (_noteData.Duration <= 0)
                {
                    Debug.Log("Note hit correctly.");
                    _laneViewModel.NoteInCatcher = null;
                    Destroy(gameObject);
                }
                else
                {

                }
            }
        }
    }

    public bool HasTail
    {
        get
        {
            return _noteData != null ? _noteData.Duration > 0 : false;
        }
    }

    public Direction Direction
    {
        get
        {
            return _noteData != null ? _noteData.Direction : Direction.Up;
        }
    }

    private void Awake()
    {
        _noteTrigger.OnEnter += OnNoteTriggerEnter;
        _noteTrigger.OnStay += OnNoteTriggerStay;
        _noteTrigger.OnExit += OnNoteTriggerExit;
        _tailTrigger.OnEnter += OnTailTriggerEnter;
        _tailTrigger.OnStay += OnTailTriggerStay;
        _tailTrigger.OnExit += OnTailTriggerExit;
    }

    public void Initialize(PlayerCharacter attachedCharacter, LaneViewModel laneVM, Note noteData, Lane lane, Vector2 spawn, Vector2 destruct)
    {
        _attachedCharacter = attachedCharacter;
        _laneViewModel = laneVM;
        _noteData = noteData;
        _lane = lane;

        transform.Rotate(0, 0, (int)noteData.Direction * -90);

        _noteImage.sprite = AssetManager.Instance.GetNoteSprite(_attachedCharacter.CharacterName, lane);

        _spawnPosition = spawn;
        _destructPosition = destruct;

        transform.position = _spawnPosition;

        float trackHeight = _spawnPosition.y - _destructPosition.y;

        _tailImage.GetComponent<RectTransform>().sizeDelta = new Vector2(
            _tailImage.GetComponent<RectTransform>().sizeDelta.x,
            _noteData.Duration * trackHeight / TrackManager.Instance.BeatsShownInAdvance);
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

    private void OnNoteTriggerEnter(Collider2D collision)
    {
        _laneViewModel.NoteInCatcher = this;
    }

    private void OnNoteTriggerStay(Collider2D collision)
    {

    }

    private void OnNoteTriggerExit(Collider2D collision)
    {
        if (_laneViewModel.NoteInCatcher == this)
        {
            _laneViewModel.NoteInCatcher = null;
        }

        if (!HasBeenHit)
        {
            Debug.Log("Player is shit.");
        }
    }

    private void OnTailTriggerEnter(Collider2D collision)
    {
    }

    private void OnTailTriggerStay(Collider2D collision)
    {
    }

    private void OnTailTriggerExit(Collider2D collision)
    {
    }
}
