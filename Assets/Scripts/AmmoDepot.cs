using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmmoDepot : MonoBehaviour
{
    private List<GameObject> ammo = new List<GameObject>();
    
    [SerializeField] private bool cheatMode;
    [SerializeField] private GameObject cheatAmmoPrefab;

    [SerializeField] private GameObject markerOpen;
    [SerializeField] private GameObject markerSelection;
    
    private int currentAmmoIndex;
    private bool isOpen = false;


    public bool IsOpenForAmmo
    {
        get => isOpen;
        set { isOpen = value; UpdateMarkers(); }
    }
    
    private void Start()
    {
        CheatAddBubble(BubbleColor.Red);
        CheatAddBubble(BubbleColor.Yellow);
        CheatAddBubble(BubbleColor.Blue);
        
        UpdateMarkers();
    }

    public void SetCurrentAmmoIndex(int ammoIndex)
    {
        currentAmmoIndex = ammoIndex;
        
        UpdateMarkers();
    }

    public int GetCurrentAmmoIndex()
    {
        return currentAmmoIndex;
    }
    
    public BubbleColor TryUseAmmo()
    {
        if (currentAmmoIndex >= ammo.Count) {
            return BubbleColor.NoColor;
        }

        Bubble ammoBubble = ammo[currentAmmoIndex].GetComponent<Bubble>();
        BubbleColor ammoColor = ammoBubble.GetColor();
        var destroyed = ammoBubble.UseShot();

        if (destroyed) {
            ammo.RemoveAt(currentAmmoIndex);
            for (int index = currentAmmoIndex; index < ammo.Count; index++) {
                ammo[index].transform.localPosition = new Vector3(index * 1.1f, 0, 0);
            }
        }
            
        return ammoColor;
    }
    
    public bool HasCapacity => ammo.Count < 3;
    
    public void AddAmmo(GameObject bubble)
    {
        if (HasCapacity) {
            bubble.GetComponent<Bubble>().SetState(BubbleState.OnTurret);
            bubble.transform.parent = transform;
            bubble.transform.localPosition = GetAmmoPosition(ammo.Count);
            ammo.Add(bubble);
        }
    }

    private Vector3 GetAmmoPosition(int ammoIndex)
    {
        return new Vector3(ammoIndex * 1.1f, 0, 0);
    }
    
    private void CheatAddBubble(BubbleColor color = BubbleColor.White)
    {
        if (cheatMode) {
            GameObject bubble = Instantiate(cheatAmmoPrefab, transform);
            bubble.GetComponent<Bubble>().MixWithColor(color);
            AddAmmo(bubble);
        }
    }

    private void UpdateMarkers()
    {
        markerOpen.SetActive(IsOpenForAmmo);

        markerSelection.transform.localPosition = GetAmmoPosition(currentAmmoIndex);
    }
}
