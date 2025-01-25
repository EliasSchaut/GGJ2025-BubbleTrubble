using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private float speed = 90;

    public void Move(Vector2 direction)
    {
        turret.ChangeAzimuth(direction.x * Time.deltaTime * speed);
        turret.ChangeElevation(direction.y * Time.deltaTime * speed);
    }

    public void Fire()
    {
        turret.Fire();
    }

    public void Y()
    {
        turret.ToggleOpenForAmmo();
    }
    
    public void X()
    {
        turret.SelectAmmoSlot(0);
    }
    
    public void A()
    {
        turret.SelectAmmoSlot(1);
    }
    
    public void B()
    {
        turret.SelectAmmoSlot(2);
    }
}
