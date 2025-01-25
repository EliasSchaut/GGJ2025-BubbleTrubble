using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AmmoDepot : MonoBehaviour
{
    private List<GameObject> ammo = new List<GameObject>();
    
    [SerializeField] private bool cheatMode;
    [SerializeField] private GameObject cheatAmmoPrefab;
    
    private int currentAmmoIndex;
    
    public bool IsOpenForAmmo { get; set; }
    
    private void Start()
    {
        CheatAddBubble(BubbleColor.Red);
        CheatAddBubble(BubbleColor.Yellow);
        CheatAddBubble(BubbleColor.Blue);
    }

    public void SetCurrentAmmoIndex(int ammoIndex)
    {
        currentAmmoIndex = ammoIndex;
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
    
    public void AddAmmo(GameObject bubble)
    {
        if (ammo.Count < 3) {
            bubble.GetComponent<Bubble>().SetState(BubbleState.OnTurret);
            bubble.transform.parent = transform;
            bubble.transform.localPosition = new Vector3(ammo.Count * 1.1f, 0, 0);
            ammo.Add(bubble);
        }
    }
    
    private void CheatAddBubble(BubbleColor color = BubbleColor.White)
    {
        if (cheatMode) {
            GameObject bubble = Instantiate(cheatAmmoPrefab, transform);
            bubble.GetComponent<Bubble>().MixWithColor(color);
            AddAmmo(bubble);
        }
    }
}
