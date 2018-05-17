using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private string _playerID;
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

    public AnalogueStick LeftStick;
    public AnalogueStick RightStick;

    // Have controls tie to this somehow.
    void Awake()
    {
        ConfirmButtonPress = new ButtonPress(() => { });
        BackButtonPress = new ButtonPress(() => { });
        UpButtonPress = new ButtonPress(() => { });
        DownButtonPress = new ButtonPress(() => { });
        LeftButtonPress = new ButtonPress(() => { });
        RightButtonPress = new ButtonPress(() => { });

        LeftStick = new AnalogueStick(_playerID, "Left");
        RightStick = new AnalogueStick(_playerID, "Right");
    }

    void Update()
    {
        LeftStick.Update();
        RightStick.Update();

        if (LeftStick.IsHeld)
        {
            Debug.Log(LeftStick.Direction);
        }

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

    public class AnalogueStick
    {
        private string _xAxisName;
        private string _yAxisName;

        private float _xAxis;
        private float _yAxis;

        public float Angle
        {
            get
            {
                return ((Mathf.Rad2Deg * Mathf.Atan2(_xAxis, _yAxis)) + 180) % 360;
            }
        }

        public bool IsHeld
        {
            get
            {
                return _xAxis != 0 || _yAxis != 0;
            }
        }

        public Direction Direction
        {
            get
            {
                float a = Angle;

                if (a >= 315 || a < 45)
                {
                    return Direction.Up;
                }
                else if (a >= 45 && a < 135)
                {
                    return Direction.Left;
                }
                else if (a >= 135 && a < 225)
                {
                    return Direction.Down;
                }
                else  // a >= 225 && a < 315
                {
                    return Direction.Right;
                }
            }
        }

        public AnalogueStick(string playerID, string stickSide)
        {
            _xAxisName = playerID + stickSide + "X";
            _yAxisName = playerID + stickSide + "Y";
        }

        public void Update()
        {
            _xAxis = Input.GetAxis(_xAxisName);
            _yAxis = Input.GetAxis(_yAxisName);
        }
    }
}
