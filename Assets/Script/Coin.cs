using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public static Coin coininstance;

    
    [SerializeField] GameObject Spaekeffect;
    [SerializeField]  AudioClip coinSound; // Sound effect for coin collection
    [SerializeField]  AudioSource audioSource; // Audio source to play the sound
    public int totalCoins = 0;

    private void Awake()
    {
        coininstance = this;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(coinSound);
            Instantiate(Spaekeffect,transform.position,transform.rotation);
            GroundStation.Instance.IncrementCoinCount();
            Destroy(gameObject); 
            
            GroundStation.Instance.Coins.Remove(gameObject); 
        }
    }
  
}
