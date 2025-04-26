using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonReleaseHandler : MonoBehaviour, IPointerUpHandler
{
    public string buttonId; // Assign this in the Inspector

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Released button: " + buttonId);

        switch (buttonId)
        {
            case "Block":
                PlayerAction.instance.CancelConstrain();
                break;
        }
    }
}

