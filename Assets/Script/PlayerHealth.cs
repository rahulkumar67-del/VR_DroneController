using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth HealthInstance;
    public NeuropypeServer Neurodata;
    [SerializeField] public float health = 200;
    [Tooltip("how much player will get damage when it collides with the ground")]
    [SerializeField] float damage;
    public int TreeCollisions = 0;
    public int RocksCollisions = 0;
    //[SerializeField] LayerMask CollisionLayer;
    public float CurrentHealth;
    public GameObject GameOver;

    private void Awake()
    {
        CurrentHealth = health;
        HealthInstance = this;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth = CurrentHealth - damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
            //die
            Destroy(gameObject);
            GameOver.SetActive(true);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}, Layer: {collision.gameObject.layer}");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Rocks"))
        {
            Debug.Log("drone collide with rocks");
            TakeDamage( damage);
            RocksCollisions++;


        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Tree"))
        {
            Debug.Log("drone collide with Tree");
            TakeDamage(damage);
            TreeCollisions++;
        }
    }
    private void Update()
    {
        float FocusIndex = Neurodata.GetFocusIndex();
        if (FocusIndex < 1f && FocusIndex > 0f)
        {
            damage = 0f;
        }
        if (FocusIndex < 2f && FocusIndex > 1f)
        {
            damage = 5f;
        }
        if (FocusIndex < 10f && FocusIndex > 2f)
        {
            damage = 10f;
        }
    }
}
