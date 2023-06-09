using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Battery Started");
    }

    void OnEnable()
    {
        Debug.Log("Battery Enabled");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
