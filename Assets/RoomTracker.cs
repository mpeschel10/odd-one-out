using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTracker : MonoBehaviour
{
    public string lastRoom = "none";
    public bool inRoomNow = false;
    void OnTriggerEnter(Collider other)
    {
        lastRoom = other.gameObject.name;
        Debug.Log("Entering room " + lastRoom);
        inRoomNow = true;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exiting  room " + other.gameObject.name);
        if (lastRoom == other.gameObject.name)
            inRoomNow = false;
    }
}
