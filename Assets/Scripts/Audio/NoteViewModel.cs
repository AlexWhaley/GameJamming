using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NoteViewModel : MonoBehaviour
{
    [SerializeField] private Trigger2DCallback _noteTrigger;
    [SerializeField] private Trigger2DCallback _tailTrigger;
    [SerializeField] private Trigger2DCallback _safeZoneTrigger;
    [SerializeField] private Image _noteImage;
    [SerializeField] private Image _tailImage;

    private PlayerCharacter _attachedCharacter;
    private Note _noteData;
    private Lane _lane;
    private Vector2 _spawnPosition;
    private Vector2 _destructPosition;

    private LaneViewModel _laneViewModel;
    
    public bool HasBeenHit { get; private set; }

    public bool HasTail
    {
        get
        {
            return _noteData != null ? _noteData.Duration > 0 : false;
        }
    }

    public bool InSafeZone { get; private set; }

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
        _safeZoneTrigger.OnEnter += OnSafeZoneTriggerEnter;
        _safeZoneTrigger.OnStay += OnSafeZoneTriggerStay;
        _safeZoneTrigger.OnExit += OnSafeZoneTriggerExit;
    }

    public void Initialize(PlayerCharacter attachedCharacter, LaneViewModel laneVM, Note noteData, Lane lane, Vector2 spawn, Vector2 destruct)
    {
        _attachedCharacter = attachedCharacter;
        _laneViewModel = laneVM;
        _noteData = noteData;
        _lane = lane;

        transform.Rotate(0, 0, (int)noteData.Direction * -90);

        _noteImage.sprite = AssetManager.Instance.GetNoteSprite(_attachedCharacter.CharacterName, lane);
        _tailImage.sprite = AssetManager.Instance.GetTailSprite(_attachedCharacter.CharacterName, lane);

        _spawnPosition = spawn;
        _destructPosition = destruct;

        transform.position = _spawnPosition;

        float trackHeight = _spawnPosition.y - _destructPosition.y;

        Vector2 tailSize = new Vector2(
            _tailImage.GetComponent<RectTransform>().sizeDelta.x,
            _noteData.Duration * trackHeight / TrackManager.Instance.BeatsShownInAdvance);

        _tailImage.GetComponent<RectTransform>().sizeDelta = tailSize;
        _tailImage.GetComponent<BoxCollider2D>().size = tailSize;
        _tailImage.GetComponent<BoxCollider2D>().offset = new Vector2(0, tailSize.y / 2);
        _tailImage.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void HitNote(bool correctHit)
    {
        if (!HasBeenHit)
        {
            HasBeenHit = true;
            if (correctHit)
            {
                if (!HasTail)
                {
                    Debug.Log("Note hit correctly.");
                    _laneViewModel.NotesInCatcher.Dequeue();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Note hit started.");
                    _laneViewModel.NotesInCatcher.Dequeue();
                    _laneViewModel.HeldNotes.Enqueue(this);
                }
            }
            else
            {
                Debug.Log("Note hit incorrectly.");
                _laneViewModel.NotesInCatcher.Dequeue();
                Destroy(gameObject);
            }
        }
    }

    public void ReleaseHeldNote(bool withinAcceptableZone)
    {
        _laneViewModel.HeldNotes.Dequeue();
        if (withinAcceptableZone)
        {
            Debug.Log("Note tail released correctly.");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Note tail released early");
            Destroy(gameObject);
        }
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
        _laneViewModel.NotesInCatcher.Enqueue(this);
    }

    private void OnNoteTriggerStay(Collider2D collision)
    {

    }

    private void OnNoteTriggerExit(Collider2D collision)
    {
        if (!HasBeenHit && _laneViewModel.NotesInCatcher.Any())
        {
            _laneViewModel.NotesInCatcher.Dequeue();
            Destroy(gameObject);
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
        if (_laneViewModel.HeldNotes.NullSafePeek() == this)
        {
            ReleaseHeldNote(true);
        }
    }

    private void OnSafeZoneTriggerEnter(Collider2D collision)
    {
        InSafeZone = true;
    }

    private void OnSafeZoneTriggerStay(Collider2D collision)
    {
    }

    private void OnSafeZoneTriggerExit(Collider2D collision)
    {
        InSafeZone = false;
    }
}
