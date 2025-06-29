using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireForce = 500f;
    [SerializeField] GameObject Turret;
    [SerializeField] GameObject Muzelfash;
    [SerializeField] GameObject Cannon;
    [SerializeField] float rotationSpeed;
    [SerializeField] float targetAngle;
    //[SerializeField] float BulletSpeed;
    //[SerializeField] float BulletHitTime;


    public void FireAt(Vector3 targetPosition)
    {
        CannonControl cannonControl = Cannon.GetComponent<CannonControl>();
        if (cannonControl != null)
        {
            cannonControl.isTracking = true;
            cannonControl.isTurning = true; // Ensure turret starts turning
            cannonControl.targetposition = targetPosition;
          
            targetAngle = cannonControl.Auto_Elevation_Angle(targetPosition);

        }
        else
        {
            Debug.LogError("CannonControl script not found on the Cannon GameObject!");
        }
        

        // Instantiate and fire the projectile
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (targetPosition - firePoint.position).normalized;
        rb.AddForce(direction * fireForce,ForceMode.Impulse);
        Destroy(projectile, 5f);
        GameObject muzzleFlashInstance = Instantiate(Muzelfash, firePoint.position, Quaternion.identity);
        Destroy(muzzleFlashInstance, 0.5f);


    }

    //private float force(Vector3 targetPosition, float elevationAngle, float bullet_mass)
    //{
    //    Vector3 directionToTarget = targetPosition - firePoint.position;

    //    // Horizontal direction (X and Z)
    //    Vector3 horizontalDirection = new Vector3(directionToTarget.x, 0f, directionToTarget.z).normalized;

    //    // Apply elevation angle
    //    float horizontalDistance = new Vector2(directionToTarget.x, directionToTarget.z).magnitude;
    //    float VerticalDistance = directionToTarget.y;
    //    float velcoity = Mathf.Sqrt((VerticalDistance * 2 * Physics.gravity.y) / Mathf.Pow(Mathf.Sin(elevationAngle * Mathf.Deg2Rad), 2f));

    //    float force = bullet_mass * (velcoity / BulletHitTime);
    //    return force;



    //}
}