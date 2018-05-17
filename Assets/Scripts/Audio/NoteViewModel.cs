using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteViewModel : MonoBehaviour
{
    private Image _noteImage;
    private Note _noteData;
    private Lane _lane;

    private void Awake()
    {
        _noteImage = GetComponent<Image>();
    }

    public void Initialize(PlayerID playerID, Note noteData, Lane lane)
    {
        _noteData = noteData;
        _lane = lane;

        transform.Rotate(0, 0, (int)noteData.Direction * -90);

        _noteImage.sprite = AssetManager.Instance.GetNoteSprite(playerID, lane);
    }
}
