using TMPro;
using UnityEngine;

public class Login_script : MonoBehaviour
{
    public TMP_InputField usernameInputField; // Assign the TMP InputField for username
    public TMP_InputField ageInputField;// Assign the age input field in the Inspector
    
    public string Username { get; private set; }
    public int Age { get; private set; }


   
    
    private void Start()
    {
        // Add listeners to update the values when the input fields change
        usernameInputField.onEndEdit.AddListener(UpdateUsername);
        ageInputField.onEndEdit.AddListener(UpdateAge);
    }

    private void UpdateUsername(string value)
    {
        Username = value;
        PlayerInfo.Username = usernameInputField.text;
        Debug.Log("Username updated: " + Username);
    }

    private void UpdateAge(string value)
    {
        if (int.TryParse(value, out int age))
        {
            Age = age;
            PlayerInfo.Age = age;
            Debug.Log("Age updated: " + Age);
        }
        else
        {
            Debug.LogWarning("Invalid age entered!");
        }
    }
}



