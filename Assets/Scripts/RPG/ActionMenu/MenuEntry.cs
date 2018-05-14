using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEntry : MonoBehaviour
{
    [SerializeField]
    private TextMesh _actionNameText;
    private Action _actionData;

    public void Initialise(Action actionData)
    {
        _actionData = actionData;
        _actionNameText.text = _actionData.ActionName;
    }

    public void HighlightMenuEntry()
    {
        // Code to highlight entry.
        // Change prompt text
    }
}
