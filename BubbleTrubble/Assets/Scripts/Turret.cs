using UnityEngine;

public class Turret : MonoBehaviour
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
        if (!ammo.TryUseAmmo())
            return;
        
        GameObject shot = Instantiate(projectile, muzzle.position, muzzle.rotation);
        
        ammo.InitializeProjectile(shot);
    }
}
