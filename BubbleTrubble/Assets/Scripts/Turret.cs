using UnityEngine;

public class Turret : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform platform;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform muzzle;

    [SerializeField] private AmmoDepot ammo;
    
    [SerializeField] private GameObject projectile;
    
    public void ChangeAzimuth(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(platform.localEulerAngles.y + angle), min: -90f, max: 90f);
            
        platform.localRotation = Quaternion.Euler(x: 0f, newAngle, z: 0f);
    }
    
    public void ChangeElevation(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(ball.localEulerAngles.x + angle), min: -90f, max: 0f);
        
        ball.localRotation = Quaternion.Euler(newAngle, y: 0f, z: 0f);
    }
    
    public void Fire()
    {
        BubbleColor color = ammo.TryUseAmmo();
        if (color == BubbleColor.NoColor) {
            return;
        }
        
        GameObject shot = Instantiate(projectile, muzzle.position, muzzle.rotation);
        Projectile p = shot.GetComponent<Projectile>();
        
        p.Initialize(color);
    }

    public void SelectAmmoSlot(int ammoSlot)
    {
        ammo.SetCurrentAmmoIndex(ammoSlot);
    }
    
    public void Interact(Player player)
    {
        player.SetInTurret(true);
    }
}
