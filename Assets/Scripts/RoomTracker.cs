using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public GameObject lastRoom;
    public bool inRoomNow = false;
    void OnTriggerEnter(Collider other)
    {
        lastRoom = other.gameObject;
        Debug.Log("Entering room " + lastRoom);
        inRoomNow = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting  room " + other.gameObject.name);
        if (lastRoom == other.gameObject)
            inRoomNow = false;
    }
}
