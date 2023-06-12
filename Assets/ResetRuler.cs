using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRuler : MonoBehaviour
{
    [SerializeField] GameObject rulerStart;
    Vector3 startPosition;
    [SerializeField] GameObject rulerEnd;
    Vector3 endPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = rulerStart.transform.position;
        endPosition = rulerEnd.transform.position;
    }

    public void Reset()
    {
        rulerStart.transform.position = startPosition;
        rulerEnd.transform.position = endPosition;
    }
}
