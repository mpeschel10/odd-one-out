using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuShowHide : MonoBehaviour
{
    public InputActionReference menuButtonReference;
    RoomTracker roomTracker;
    void Awake()
    {
        if (roomTracker == null)
            roomTracker = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerRoomTracker>().roomTracker;
        menuButtonReference.action.performed += FlipVisiblity;
        gameObject.SetActive(false);
    }

    // [SerializeField] GameObject peakHints;
    [SerializeField] GameObject oddOneOutHints;

    void FlipVisiblity(InputAction.CallbackContext context)
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf && roomTracker.lastRoom != null)
        {
            // peakHints.SetActive(false);
            oddOneOutHints.SetActive(false);
            if (roomTracker.lastRoom.name == "Peak Room")
            {
                // peakHints.SetActive(true);
            } else if (roomTracker.lastRoom.name == "Odd One Out Room")
            {
                oddOneOutHints.SetActive(true);
            }
        }
    }
}
