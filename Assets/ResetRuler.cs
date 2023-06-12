using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRuler : MonoBehaviour
{
    [SerializeField] GameObject rulerStart;
    Vector3 startPosition;
    [SerializeField] GameObject rulerEnd;
    Vector3 endPosition;
    
    bool initialized = false;
    void Start()
    {
        startPosition = rulerStart.transform.position;
        endPosition = rulerEnd.transform.position;
        initialized = true;
    }

    public void Reset()
    {
        if (initialized)
        {
            rulerStart.transform.position = startPosition;
            rulerEnd.transform.position = endPosition;
        }
    }
}
