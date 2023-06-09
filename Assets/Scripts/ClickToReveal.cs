using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToReveal : MonoBehaviour, CameraSelector.Hoverable, CameraSelector.Clickable
{
    public void Hover() { GetComponent<LayeredOutline>().AddLayer("can-click"); }
    public void Unhover() { GetComponent<LayeredOutline>().SubtractLayer("can-click"); }
    public GameObject GetGameObject() { return gameObject; }

    [SerializeField] public GameObject visiblePillar;
    public int index;
    public static bool revealPairs = false;
    PillarList GetPillars() { return transform.parent.parent.gameObject.GetComponent<PillarList>(); }
    public void Click()
    {
        PillarList pillars = GetPillars();
        this.Reveal();
        
        if (revealPairs)
        {
            if (index > 0 && pillars.Get(index - 1).gameObject.activeSelf)
                pillars.Get(index - 1).Reveal();
            else if (index + 1 < pillars.Count())
                pillars.Get(index + 1).Reveal();
        }
    }

    public void Reveal()
    {
        GetPillars().Increment();
        gameObject.SetActive(false);
        visiblePillar.SetActive(true);
        GetPillars().CheckWin();
    }
}
