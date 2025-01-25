using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform muzzle;

    [SerializeField] private AmmoDepot ammo;
    
    [SerializeField] private GameObject projectile;
    
    [SerializeField] private float cooldown = 0.25f;
    [SerializeField] private float spread = 5.0f;
    
    [SerializeField] private Animator animator;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public bool IsOpenForAmmo
    {
        get => ammo.IsOpenForAmmo;
        set => ammo.IsOpenForAmmo = value;
    }

    public void ChangeAzimuth(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(platform.localEulerAngles.y + angle * 0.1f), min: -90f, max: 90f);
        
        platform.localRotation = Quaternion.Euler(x: 0f, newAngle, z: 0f);
    }
    
    public void ChangeElevation(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(ball.localEulerAngles.x - angle * 0.1f), min: -90f, max: 0f);
        
        ball.localRotation = Quaternion.Euler(newAngle, y: 0f, z: 0f);
    }
    
    public void Fire()
    {
        if (timer < cooldown) {
            return;
        }
        
        timer = 0;
        
        BubbleColor color = ammo.TryUseAmmo();
        if (color == BubbleColor.NoColor) {
            return;
        }
        
        Quaternion spread = Quaternion.Euler(Random.Range(-this.spread, this.spread), Random.Range(-this.spread, this.spread), 0);
        
        GameObject shot = Instantiate(projectile, muzzle.position, muzzle.rotation * spread);
        Projectile p = shot.GetComponent<Projectile>();
        
        p.Initialize(color);
        
        animator.SetTrigger("Fire");
    }
    
    public void ToggleOpenForAmmo()
    {
        IsOpenForAmmo = !IsOpenForAmmo;
    }

    public void SelectAmmoSlot(int ammoSlot)
    {
        ammo.SetCurrentAmmoIndex(ammoSlot);
    }

    public void AddAmmo(Bubble bubble)
    {
        ammo.AddAmmo(bubble.gameObject);
    }

    public bool CanAddAmmo => ammo.HasCapacity;
}
