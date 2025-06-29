using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] float damage = 50f;
    [SerializeField] GameObject BulletBrokenPrefab; // Renamed for clarity
    [SerializeField] float DestroyTime;
    private bool hit = false;
   
    private float hitTime;
    private GameObject instantiatedBrokenBullet; // To store the instance of BulletBroken

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bullet collided with: " + other.name);

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            Data playerHit = other.GetComponent<Data>();

            if (playerHit != null)
            {
                playerHit.Bullethit();
                //Debug.Log("No of bullets hit to player: " + playerHit.No_of_BulletHit);
            }
            else
            {
                Debug.LogWarning("Data component is missing on the Player object: " + other.name);
            }

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // Instantiate the broken bullet and store the instance
                instantiatedBrokenBullet = Instantiate(BulletBrokenPrefab, other.transform.position, Quaternion.identity);
                Debug.Log("Player current health: " + playerHealth.CurrentHealth);
                hit = true;
            }
            if(playerHealth = null)
            {
                Debug.LogWarning("PlayerHealth component is missing on the Player object: " + other.name);
            }
        }
    }

    private void Update()
    {
        if (hit)
        {
            hitTime += Time.deltaTime;
            if (DestroyTime < hitTime)
            {
                // Destroy the bullet and the instantiated broken bullet instance
                Destroy(gameObject);
                if (instantiatedBrokenBullet != null)
                {
                    Destroy(instantiatedBrokenBullet);
                }
            }
        }
        float FocusIndex = NeuropypeServer.instance.GetFocusIndex();
        if (FocusIndex < 1f && FocusIndex > 0f)
        {
            damage = 10f;
        }
        if (FocusIndex < 2f  && FocusIndex > 1f)
        {
            damage = 15f;
        }
        if (FocusIndex < 10f  && FocusIndex > 2f)
        {
            damage = 20f;
        }
    }
}
