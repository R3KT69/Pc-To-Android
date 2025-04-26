using UnityEngine;
using UnityEngine.EventSystems;

public class TouchScreenCameraMovement : MonoBehaviour
{
    public float sensitivity = 0.5f;
    public Transform playerBody;
    public static bool inAndroid = false; // ***TOGGLE ANDROID MODE***
    private Vector2 lastTouchPos;
    private bool isDragging = false;
    private float verticalRotation = 0f;

    void Update()
    {
        if (inAndroid)
        {
            #if UNITY_EDITOR || UNITY_STANDALONE
            // Simulate with mouse in Editor or PC
            if (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2)
            {
                lastTouchPos = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButton(0) && isDragging)
            {
                Vector2 delta = (Vector2)Input.mousePosition - lastTouchPos;
                playerBody.Rotate(Vector3.up, delta.x * sensitivity);

                verticalRotation -= delta.y * sensitivity;
                verticalRotation = Mathf.Clamp(verticalRotation, -75f, 75f);
                transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);

                lastTouchPos = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            #else
            // Real Android touch input
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);

                    // Skip touch if it's over UI (like joystick)
                    if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        continue;

                    // Only respond to right-side touches
                    if (touch.position.x < Screen.width / 2) continue;

                    if (touch.phase == TouchPhase.Began)
                    {
                        lastTouchPos = touch.position;
                        isDragging = true;
                    }
                    else if (touch.phase == TouchPhase.Moved && isDragging)
                    {
                        Vector2 delta = touch.deltaPosition;
                        playerBody.Rotate(Vector3.up, delta.x * sensitivity);

                        verticalRotation -= delta.y * sensitivity;
                        verticalRotation = Mathf.Clamp(verticalRotation, -75f, 75f);
                        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0f);

                        lastTouchPos = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        isDragging = false;
                    }

                    break; // Only use the first valid touch
                }
            }
            #endif
        }
    }
}
