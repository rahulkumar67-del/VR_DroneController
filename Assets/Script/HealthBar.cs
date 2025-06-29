using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider EaseHealthSlider;
    private float LerpSpeed = 0.05f;

    public PlayerHealth playerhealth;
    private void Awake()
    {
        HealthSlider.value = playerhealth.health;
    }
    private void Update()
    {
        HealthSlider.value = playerhealth.CurrentHealth;

        if (HealthSlider.value != EaseHealthSlider.value)
        {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value , playerhealth.CurrentHealth, LerpSpeed);
        }
        
    }
}
