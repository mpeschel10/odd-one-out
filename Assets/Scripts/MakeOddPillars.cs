using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MakeOddPillars : MonoBehaviour, PillarList
{
    float[] pillarHeights = { 0.2f, 0.2f, 0.1f, 0.1f, 0.8f, 0.8f, 0.4f, 0.4f, 0.6f, 0.9f, 0.9f, 0.2f, 0.2f, 0.2f, 0.2f, 0.1f, 0.1f, 1.0f, 1.0f, 0.8f, 0.8f, 0.4f, 0.4f };
    public ClickToReveal[] hiddenPillars;
    public float pillarScale = 4;
    [SerializeField] Material winMaterial;

    TMP_Text costText;
    GameObject fireworkBattery;
    int cost;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject canonPillar = transform.GetChild(0).gameObject;
        costText = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TMP_Text>();
        fireworkBattery = transform.GetChild(2).gameObject;
        
        cost = 0; won = false;
        UpdateCostText();
        
        canonPillar.SetActive(false);
        Vector3 ft = canonPillar.transform.position;
        Quaternion fr = canonPillar.transform.rotation;
        Vector3 pillarOffset = new Vector3(0, 0, -1);

        hiddenPillars = new ClickToReveal[pillarHeights.Length];
        for (int i = 0; i < pillarHeights.Length; i++)
        {
            float height = pillarHeights[i];
            GameObject newObject = Object.Instantiate(canonPillar, ft + pillarOffset * i, fr, transform);
            
            TMP_Text text = newObject.GetComponentInChildren<TMP_Text>();
            text.text = i.ToString();

            ClickToReveal hiddenPillar = newObject.GetComponentInChildren<ClickToReveal>();
            hiddenPillars[i] = hiddenPillar;
            hiddenPillar.index = i;
            
            PillarVisibleFlag visiblePillar = newObject.GetComponentInChildren<PillarVisibleFlag>(true);
            Vector3 s = visiblePillar.transform.localScale;
            visiblePillar.transform.localScale = new Vector3(s.x, height * pillarScale, s.z);
            visiblePillar.transform.localPosition = new Vector3(0.5f, height * pillarScale / 2, 0.5f);
            
            newObject.SetActive(true);
        }


    }
    public ClickToReveal Get(int i) { return hiddenPillars[i]; }
    public int Count() { return pillarHeights.Length; }

    public void Increment() {
        cost++;
        UpdateCostText();
    }
    public void UpdateCostText() { costText.text = "Cost: " + cost; }

    bool InBounds(int i) { return i >= 0 && i < hiddenPillars.Length; }
    bool Revealed(int i) { return !hiddenPillars[i].gameObject.activeSelf; }

    bool won;
    public void CheckWin() {
        if (won) return;
        int windex = -1;
        for (int i = 0; i < pillarHeights.Length - 1; i += 2)
        {
            float a = pillarHeights[i];
            float b = pillarHeights[i + 1];
            if (a != b)
            {
                windex = i;
                break;
            }
        }

        if (pillarHeights.Length >= 3)
        {
            int ultimate = pillarHeights.Length - 1;
            int penultimate = pillarHeights.Length - 1;
            if (pillarHeights[ultimate] != pillarHeights[penultimate])
            {
                windex = ultimate;
            }
        }

        if (windex == -1)
        { throw new System.Exception("No winning cell found in array."); }

        List<int> indices = new List<int>();
        if (InBounds(windex - 1)) indices.Add(windex - 1);
        if (InBounds(windex))     indices.Add(windex);
        if (InBounds(windex + 1)) indices.Add(windex + 1);
        
        // Proof requires that windex and both of its neighbors are visible or out of bounds
        if (indices.TrueForAll(Revealed))
        {
            Debug.Log("A winner is you");
            won = true;
            foreach (int i in indices)
            {
                ClickToReveal hiddenPillar = hiddenPillars[i];
                hiddenPillar.visiblePillar.GetComponent<Renderer>().material = winMaterial;
            }

            ClickToReveal winningPillar = hiddenPillars[windex];
            fireworkBattery.transform.Translate(Vector3.right * winningPillar.transform.parent.localPosition.x);
            SetFireworks(true);
            Invoke(nameof(StopFireworks), 7);
        }
    }

    void StopFireworks() { Debug.Log("Stopping fireworks."); SetFireworks(false); }
    void SetFireworks(bool enabled)
    {
        foreach(ParticleSystem p in fireworkBattery.GetComponentsInChildren<ParticleSystem>())
        {
            ParticleSystem.EmissionModule em = p.emission;
            em.enabled = enabled; // Why is this not a one-liner???
        }
    }

    bool hintPairs = false;
    public void SetHintPairs(bool active) { hintPairs = active; }
    public void Click(int index)
    {
        hiddenPillars[index].Reveal();
        
        if (hintPairs)
        {
            if (index > 0)
                hiddenPillars[index - 1].Reveal();
            else if (index + 1 < hiddenPillars.Length)
                hiddenPillars[index + 1].Reveal();
        }
        CheckWin();
    }

    public void Reset()
    {
        foreach (ClickToReveal hiddenPillar in hiddenPillars)
        {
            Destroy(hiddenPillar.transform.parent.gameObject);
        }
        StopFireworks();

        Start();
    }
}
