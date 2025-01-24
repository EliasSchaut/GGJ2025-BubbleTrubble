using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Turret turret;
    [SerializeField] private float speed = 90;
    
    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        
        float azimuth = 0;
        
        if (keyboard.aKey.isPressed)
        {
            azimuth = -1;
        }
        else if (keyboard.dKey.isPressed)
        {
            azimuth = 1;
        }
        
        turret.ChangeAzimuth(azimuth * Time.deltaTime * speed);
        
        float elevation = 0;
        
        if (keyboard.wKey.isPressed)
        {
            elevation = -1;
        }
        else if (keyboard.sKey.isPressed)
        {
            elevation = 1;
        }
        
        turret.ChangeElevation(elevation * Time.deltaTime * speed);
        
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            turret.Fire();
        }
    }
}
