using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
using UnityEngine.UI; // For UI elements
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioMixerSnapshot defaultSnapshot;
    [SerializeField] AudioMixerSnapshot pauseSnapshot;
    [SerializeField] float fadeTimer = 2f;

    private bool isPaused = false;

   void Start()
    {
      pauseMenu.SetActive(false);
    } //don't need this since pauseMenu will instantiate as false. nvm
    void Update()
    {
        // Toggle pause menu when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        Time.timeScale = 1f; // Resume game time
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; //gone
        pauseSnapshot.TransitionTo(fadeTimer);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true); // Show the pause menu
        Time.timeScale = 0f; // Pause game time
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; //free the Cursor
        Cursor.visible = true; //seen
        defaultSnapshot.TransitionTo(fadeTimer);
    }
}
