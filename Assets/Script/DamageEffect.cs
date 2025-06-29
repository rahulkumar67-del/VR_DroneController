using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class DamageEffect : MonoBehaviour
{
    public GameObject Fps_cam; // Camera with the Volume component
    private Volume _volume; // URP Volume component
    private UnityEngine.Rendering.Universal.Vignette _vignette;// Vignette effect
    private UnityEngine.Rendering.Universal.DepthOfField _depthOfField; // For URP
                                                                        // Depth of Field effect

    public PlayerHealth playerHealth; // Reference to the player's health script
    [SerializeField] private float Maxhealth = 200f;
    [SerializeField] private float AlertHealth = 100f;
    [SerializeField] private float MinHealth = 100f;

    public float minVignetteIntensity = 0f;
    public float maxVignetteIntensity = 0.6f;
    public float minBlur = 10f; // Minimum blur (Focus Distance)
    public float maxBlur = 1f; // Maximum blur (Focus Distance)

    [Tooltip("No of coin from which player will get thier +50 health back ")][SerializeField] float BoostCoin;
    [SerializeField] bool HealthEffecct = false;
    [Tooltip("Time that player need to recovery from blood effect adb blur effect")][SerializeField] float RecoveryTime;





    // effect from neuro data
    private float No_of_coin;
    public NeuropypeServer Neurodata;
    private float FocusIndex;
 

    private void Start()
    {
        // Get the Volume component from the camera
        _volume = Fps_cam.GetComponent<Volume>();
        if (_volume == null)
        {
            Debug.LogError("Volume component not found on Fps_cam!");
            return;
        }

        // Try to get the Vignette effect from the Volume's profile
        if (!_volume.profile.TryGet(out _vignette))
        {
            Debug.LogError("Vignette effect not found in the Volume profile!");
        }

        // Try to get the Depth of Field effect from the Volume's profile
        if (!_volume.profile.TryGet(out _depthOfField))
        {
            Debug.LogError("Depth of Field effect not found in the Volume profile!");
        }
    }

    private void Update()
    {
        No_of_coin = Coin.coininstance.totalCoins;
        FocusIndex = Neurodata.GetFocusIndex();
        // Check if health is below the max health
        if (FocusIndex<5f && !HealthEffecct)
        {
            
            Engage();
        }
        if (HealthEffecct)
        {
            StartCoroutine(HealthDamageEffect());
            Engage();
        }
        
        Debug.Log("Current focus" + (FocusIndex));
    }

   

    private IEnumerator HealthDamageEffect()
    {
        if (_vignette == null || _depthOfField == null) yield break;
        if (GroundStation.Instance.OnAttack && HealthEffecct)
        {
            // Player is under attack, apply damage effects
            float vignetteIntensity = Mathf.Lerp(maxVignetteIntensity, minVignetteIntensity, playerHealth.CurrentHealth / Maxhealth);
            float blur = Mathf.Lerp(maxBlur, minBlur, playerHealth.CurrentHealth / Maxhealth);

            _vignette.intensity.Override(vignetteIntensity);
            _depthOfField.focusDistance.Override(blur);
        }
        if (!GroundStation.Instance.OnAttack && HealthEffecct && playerHealth.CurrentHealth > MinHealth)
        {
            // Gradually decrease the effects to zero
            float currentVignette = _vignette.intensity.value;
            float currentBlur = _depthOfField.focusDistance.value;

            currentVignette = Mathf.MoveTowards(currentVignette, 0f, Time.deltaTime * RecoveryTime); // Adjust speed as needed
            currentBlur = Mathf.MoveTowards(currentBlur, 10f, Time.deltaTime * RecoveryTime);

            _vignette.intensity.Override(currentVignette);
            _depthOfField.focusDistance.Override(currentBlur);
        }

        if (playerHealth.CurrentHealth < MinHealth && HealthEffecct)
        {
            // Player is under attack, apply damage effects
            float vignetteIntensity = 0.5f;
            float blur = 5f;

            _vignette.intensity.Override(vignetteIntensity);
            _depthOfField.focusDistance.Override(blur);
        }


        yield return null; // Ensure smooth transitions



    }


   
    private void Engage()
    {
        if (FocusIndex < 1f && !HealthEffecct && FocusIndex >0f)
        {
            GroundStation.Instance.Time_int = 5;    
        }
        if (FocusIndex <2f  && !HealthEffecct && FocusIndex >1f)
        {
            GroundStation.Instance.Time_int = 3;
        }
        if (FocusIndex < 10f && !HealthEffecct && FocusIndex > 2f)
        {
            GroundStation.Instance.Time_int = 2;
        }
        if ( No_of_coin>= BoostCoin)
        {
            playerHealth.CurrentHealth += 50f;
            No_of_coin = 0;
            BoostCoin = BoostCoin *2f;
        }
    }
}

//if (HealthEffecct)
//{
//    if (playerHealth.CurrentHealth < AlertHealth)
//    {



//    }
//    else
//    {

//    }
//}
