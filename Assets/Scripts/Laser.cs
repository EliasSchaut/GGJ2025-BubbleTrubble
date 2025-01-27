using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer laserLine;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        laserLine.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Projectile", "Enemy")))
        {
            laserLine.SetPosition(1, hit.point);
            
            laserLine.material.color = Color.green;
        }
        else
        {
            laserLine.SetPosition(1, ray.origin + ray.direction * 10000f);
            
            laserLine.material.color = Color.red;
        }
    }
}
