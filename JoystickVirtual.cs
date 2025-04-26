using UnityEngine;

public class JoystickVirtual : MonoBehaviour
{
    public FixedJoystick joystick; // drag the joystick here in inspector

    void Update()
    {
        VirtualHorizontal = joystick.Horizontal;
        VirtualVertical = joystick.Vertical;
    }

    public static float VirtualHorizontal { get; private set; }
    public static float VirtualVertical { get; private set; }
}