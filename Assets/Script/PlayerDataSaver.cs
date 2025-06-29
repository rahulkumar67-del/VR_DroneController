using System.IO;
using UnityEngine;

public class PlayerDataSaver : MonoBehaviour
{
    public GameObject player;
    private float TreeCollisions;
    private float Rockscollisions;
    private float BulletHits;
    private float Score;
    private string SurvivalTime;
    private string filename;

    void Start()
    {
        // Set the file path for the CSV file
        filename = Application.dataPath + "/PlayerData.csv";

        // Write the header to the CSV file if it doesn't already exist
        if (!File.Exists(filename))
        {
            using (StreamWriter writer = new StreamWriter(filename, false))
            {
                writer.WriteLine("Name,Age,BulletHits,Score,SurvivalTime,RocksCollision,TreeCollision");
            }
        }
    }

    private void Update()
    {
        
        BulletHits =Data.Datainstance.No_of_BulletHit;
        Score = Data.Datainstance.Score;
        SurvivalTime = Data.Datainstance.GetFormattedGamePlayTime();
        TreeCollisions = Data.Datainstance.TreeCollisions;
        Rockscollisions = Data.Datainstance.Rockscollisions;

    }

    void OnApplicationQuit()
    {
        // Save player data when the game is quitting
        SavePlayerData();
        Debug.Log("Game data saved on quit.");
    }

    public void SavePlayerData()
    {
       

        string username = PlayerInfo.Username;
        int age = PlayerInfo.Age;
        //InputManager.Instance.useVRInput = PlayerInfo.Vr;

        if (string.IsNullOrEmpty(username))
        {
            Debug.LogWarning("Username is empty. Please provide a username.");
            return;
        }

        // Append the player's data to the CSV file
        using (StreamWriter writer = new StreamWriter(filename, true))
        {
            writer.WriteLine($"{username},{age},{BulletHits},{Score},{SurvivalTime},{Rockscollisions},{TreeCollisions}");
        }

        Debug.Log("Player data saved to CSV!");
    }
}
