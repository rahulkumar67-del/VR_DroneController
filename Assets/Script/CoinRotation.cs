using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour
{
    [SerializeField] float RotationSpeed;
    
    private void Update()
    {

        transform.Rotate(0f, RotationSpeed * Time.deltaTime, 0f);
    }



}

