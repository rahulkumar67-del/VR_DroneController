using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI FocusIndex;
    public TextMeshProUGUI CoinCounter;
    public NeuropypeServer Neurodata;
    private void Update()
    {
        float focusindex = Neurodata.GetFocusIndex();
        FocusIndex.text = "Focus Score:" + focusindex;
        CoinCounter.text = "Game Score:" + GroundStation.Instance.GetCoinCount();
    }
}
