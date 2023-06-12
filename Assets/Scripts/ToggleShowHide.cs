using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleShowHide : MonoBehaviour
{
    public Toggle toggle;
    void Awake()
    {
        if (toggle == null)
        {
            toggle = gameObject.GetComponent<Toggle>();
        }
        toggle.onValueChanged.AddListener(delegate { ShowHide(toggle); });
    }

    [SerializeField] GameObject target;
    void ShowHide(Toggle change)
    {
        target.SetActive(change.isOn);
    }
}
