using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GroundStation : MonoBehaviour
{
    public static GroundStation Instance;
    [SerializeField] GameObject[] Tanks;
    public List<GameObject> Coins; // List of coins
    private Vector3 dronePosition;
    [SerializeField] GameObject drone;
    [SerializeField] float collisionRadius;
    [Tooltip("Range in which Tank can detect the player ")][SerializeField] float drone_det_Range;
    [Tooltip("Range in which Those Tank under fall protect the coin ")][SerializeField] float Coin_protection;
    [Tooltip("Time int between the tank fire")] public float Time_int = 2f;

    [HideInInspector]
    [Tooltip("Player is on Attack ")]public bool OnAttack = false;
    public float time;
    private int totalCoins;
    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        
        if (drone != null )
        {
            dronePosition = drone.transform.position;
        }
        else
        {
            Debug.Log("player is die ");
        }
        time += Time.deltaTime;

        if (time > Time_int)
        {
            AlertTankNearPlayer(dronePosition);
            time = 0f;
        }

        for (int i = Coins.Count - 1; i >= 0; i--)
        {
            float distanceToCoin = Vector3.Distance(dronePosition, Coins[i].transform.position);

            if (distanceToCoin < drone_det_Range)
            {
                
                if (time > Time_int)
                {
                    if (Coins[i] != null)
                    {
                        AlertTanksNearCoin(Coins[i].transform.position);
                    }
                    time = 0f;
                }
            }
        }
    }
    public void IncrementCoinCount()
    {
        totalCoins++;
        Debug.Log($"Total Coins Collected: {totalCoins}");
    }

    public int GetCoinCount()
    {
        return totalCoins;
    }


    public void AlertTanksNearCoin(Vector3 coinPosition)
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            float distanceToTank = Vector3.Distance(coinPosition, Tanks[i].transform.position);

            if (distanceToTank < Coin_protection)
            {
                Tanks[i].GetComponent<TankManager>().FireAt(dronePosition);
                Debug.Log($"Tank {i} firing at the player near coin!");
                OnAttack = true;
            }
            else
            {
                OnAttack = false;
            }
        }
    }

    public void AlertTankNearPlayer(Vector3 playerPosition)
    {
        for (int i = 0; i < Tanks.Length; i++)
        {
            float distance = Vector3.Distance(playerPosition, Tanks[i].transform.position);
            //Debug.Log(distance);

            if (distance < drone_det_Range)
            {
                Tanks[i].GetComponent<TankManager>().FireAt(dronePosition);
                //Debug.Log($"Tank {i} firing at the player ");
                OnAttack = true;
            }
            else
            {
                OnAttack = false;
            }
        }
    }
}
