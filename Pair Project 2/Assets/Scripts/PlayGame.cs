using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnButtonPress : MonoBehaviour
{
    // Name of the scene to load
    public string sceneName;

    // Reference to the Button component
    public Button playButton;

    void Start()
    {
        // Ensure the button is assigned
        if (playButton != null)
        {
            // Add a listener to the button to call the LoadScene method when clicked
            playButton.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("Play Button not assigned in the inspector.");
        }
    }

    // Method to load the specified scene
    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}