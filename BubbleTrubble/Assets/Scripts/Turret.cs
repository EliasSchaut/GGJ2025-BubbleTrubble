using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [SerializeField] private float aimDistance = 50.0f;
    [SerializeField] private Vector2 aimerBounds = new (50.0f, 50.0f);
    [SerializeField] private GameObject aimer;

    private float timer;

    private void Start()
    {
        Move(Vector2.zero);
    }
    
    public void SetAimEnabled(bool enabled)
    {
        aimer.SetActive(enabled);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public bool IsOpenForAmmo
    {
        get => ammo.IsOpenForAmmo;
        set => ammo.IsOpenForAmmo = value;
    }

    public void Move(Vector2 dir)
    {
        Vector3 aimerPos = aimer.transform.localPosition;
        
        aimerPos.x = Mathf.Clamp(aimerPos.x + dir.x, -aimerBounds.x, aimerBounds.x);
        aimerPos.y = Mathf.Clamp(aimerPos.y + dir.y, -aimerBounds.y, aimerBounds.y);
        aimerPos.z = aimDistance;
        
        aimer.transform.localPosition = aimerPos;
        
        Vector3 target = aimer.transform.position;
        Camera cam = Camera.main;
        
        Ray ray = cam.ScreenPointToRay(cam.WorldToScreenPoint(target));
        
        if (Physics.Raycast(ray, out RaycastHit hit, float.PositiveInfinity, LayerMask.GetMask("Enemy"))) {
            target = hit.point;
        }
        
        Vector3 direction = target - transform.position;
        
        Quaternion globalRotation = Quaternion.LookRotation(direction, Vector3.up);
        Quaternion localRotation = Quaternion.Inverse(transform.rotation) * globalRotation;
        
        Vector3 localEulerAngles = localRotation.eulerAngles + new Vector3(0, 180, 0);

        ChangeAzimuth(localEulerAngles.y);
        ChangeElevation(360.0f - Utilties.NormalizeAngle(localEulerAngles.x));
    }

    private void ChangeAzimuth(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(angle), min: -90f, max: 90f);
        
        platform.localRotation = Quaternion.Euler(x: 0f, newAngle, z: 0f);
    }
    
    private void ChangeElevation(float angle)
    {
        float newAngle = Mathf.Clamp(Utilties.NormalizeAngle(angle), min: 0f, max: 90f);
        
        ball.localRotation = Quaternion.Euler(newAngle, y: 0f, z: 0f);
    }
    
    public void Fire()
    {
        if (timer < cooldown)
        {
            animator.SetBool("Shooting", false);
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
        
        animator.SetBool("Shooting", true);
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
