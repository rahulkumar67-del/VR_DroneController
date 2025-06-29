using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static Data Datainstance;
    public float Score;
    public GroundStation GStation;
    public float No_of_BulletHit;
    public float TreeCollisions;
    public float Rockscollisions;
    public float GamePlayTime { get; private set; }

    private void Awake()
    {
        Datainstance = this;
        if (GStation == null)
        {
            GStation = GStation.GetComponent<GroundStation>();
            if (GStation == null)
            {
                Debug.LogError("GroundStation object not found in the scene.");
            }
        }
    }

    private void Update()
    {
        if (GStation != null)
        {
            Score = Coin.coininstance.totalCoins;
        }
        Rockscollisions = PlayerHealth.HealthInstance.RocksCollisions;
        TreeCollisions = PlayerHealth.HealthInstance.TreeCollisions;    


        // Increment gameplay time
        GamePlayTime += Time.deltaTime;
    }
   

    public void Bullethit()
    {
        No_of_BulletHit++;
    }

    public string GetFormattedGamePlayTime()
    {
        int minutes = Mathf.FloorToInt(GamePlayTime / 60);
        int seconds = Mathf.FloorToInt(GamePlayTime % 60);
        return $"{minutes:00}:{seconds:00}";
    }

    public void SaveGameTime()
    {
        PlayerPrefs.SetFloat("GamePlayTime", GamePlayTime);
        PlayerPrefs.Save();
        Debug.Log("Game time saved: " + GamePlayTime);
    }

    public void LoadGameTime()
    {
        GamePlayTime = PlayerPrefs.GetFloat("GamePlayTime", 0);
        Debug.Log("Game time loaded: " + GamePlayTime);
    }



}
