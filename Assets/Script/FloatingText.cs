using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        
    }

  
}
