using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteViewModel : MonoBehaviour
{
    [SerializeField] private Transform _directionTransform;
    private SpriteRenderer _noteSprite;
    private Note _noteData;
    private Lane _lane;

    private void Awake()
    {
        _noteSprite = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Note noteData, Lane lane)
    {
        _noteData = noteData;
        _lane = lane;

        _directionTransform.Rotate(0, 0, (int)noteData.Direction * 90);

        _noteSprite.color = _lane == Lane.Left ? new Color(0.1f, 0.5f, 1.0f) : new Color(1.0f, 0.1f, 0.0f);
    }
}
