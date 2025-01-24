using UnityEngine;

public class AmmoDepot : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    [SerializeField] private int ammoPerBubble = 10;
    [SerializeField] private BubbleColor bubbleColor = BubbleColor.White;
    
    [SerializeField] private bool cheatMode;
    
    private int currentAmmo;
    
    private void Start()
    {
        Reload();
    }
    
    public void Reload()
    {
        currentAmmo = ammoPerBubble;
        
        UpdateDepot();
        
        BubbleColors.SetObjectColor(ammo, bubbleColor);
    }
    
    public bool TryUseAmmo()
    {
        if (currentAmmo <= 0) 
            return false;

        currentAmmo--;
        UpdateDepot();
        
        if (cheatMode && currentAmmo == 0)
            Reload();
            
        return true;
    }

    private void UpdateDepot()
    {
        float scale = Mathf.Clamp01(currentAmmo / (float)ammoPerBubble);

        ammo.transform.localScale = new Vector3(scale, scale, scale);
    }
    
    public void InitializeProjectile(GameObject projectile)
    {
        Projectile shot = projectile.GetComponent<Projectile>();
        
        shot.Initialize(bubbleColor);
    }
}
