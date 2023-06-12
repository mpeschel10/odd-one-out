using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateScreenMenuPanel : MonoBehaviour
{
    GameObject handMenuPanel;
    void Awake()
    {
        GameObject theirPanel = GameObject.FindGameObjectWithTag("ScreenMenuPanel");
        GameObject handMenuPanel = Object.Instantiate(theirPanel, this.transform, false);
    }
}
