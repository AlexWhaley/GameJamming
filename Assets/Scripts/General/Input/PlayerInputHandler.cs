using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [SerializeField]
    private string ConfirmButton;
    [SerializeField]
    private string BackButton;
    [SerializeField]
    private string UpButton;
    [SerializeField]
    private string DownButton;
    [SerializeField]
    private string LeftButton;
    [SerializeField]
    private string RightButton;

    public delegate void ButtonPress();

    public ButtonPress ConfirmButtonPress;
    public ButtonPress BackButtonPress;
    public ButtonPress UpButtonPress;
    public ButtonPress DownButtonPress;
    public ButtonPress LeftButtonPress;
    public ButtonPress RightButtonPress;


    // Have controls tie to this somehow.
    void Awake()
    {
        ConfirmButtonPress = new ButtonPress(() => { });
        BackButtonPress = new ButtonPress(() => { });
        UpButtonPress = new ButtonPress(() => { });
        DownButtonPress = new ButtonPress(() => { });
        LeftButtonPress = new ButtonPress(() => { });
        RightButtonPress = new ButtonPress(() => { });
    }

    void Update()
    {
        if (Input.GetKeyDown(ConfirmButton))
        {
            ConfirmButtonPress();
        }
        else if (Input.GetKeyDown(BackButton))
        {
            BackButtonPress();
        }
        else if (Input.GetKeyDown(UpButton))
        {
            UpButtonPress();
        }
        else if (Input.GetKeyDown(DownButton))
        {
            DownButtonPress();
        }
        else if (Input.GetKeyDown(LeftButton))
        {
            LeftButtonPress();
        }
        else if (Input.GetKeyDown(RightButton))
        {
            RightButtonPress();
        }
    }
}
