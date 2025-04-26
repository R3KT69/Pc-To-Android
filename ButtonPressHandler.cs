using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressHandler : MonoBehaviour, IPointerDownHandler
{
    public string buttonId; 

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Released button: " + buttonId);

        switch (buttonId)
        {
            case "Block":
                PlayerAction.instance.Block();
                break;
        }
    }
}
