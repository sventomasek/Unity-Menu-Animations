using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))] // This will automatically add an AudioSource to the GameObject
public class MA_ButtonEvents : MonoBehaviour
{
    [SerializeField] AudioClip pressAudio;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // MA_PlayAudio is not public because if the button is pressed before the Audio is loaded, it will not work until it loads
    void MA_PlayAudio(AudioClip clip)
    {
        // If you want to play the Audio at a specific volume, it is recommended to change audioSource.volume
        audioSource.PlayOneShot(clip);
    }

    public void MA_QuitGame()
    {
        MA_PlayAudio(pressAudio);
        Application.Quit(); // Quits the game
    }

    public void MA_LoadScene(string sceneName)
    {
        MA_PlayAudio(pressAudio);
        SceneManager.LoadScene(sceneName); // Loads another scene
    }

    public void MA_LoadSceneAsync(string sceneName)
    {
        MA_PlayAudio(pressAudio);
        SceneManager.LoadSceneAsync(sceneName); // Loads another scene in the background
    }

    public void MA_ToggleMenu(MA_Menu menu)
    {
        MA_PlayAudio(pressAudio);
        menu.showMenu = !menu.showMenu; // Shows/Hides a Menu that has the MA_Menu script
    }

    public void MA_ToggleMenuOn(MA_Menu menu)
    {
        MA_PlayAudio(pressAudio);
        menu.showMenu = true; // Shows a Menu that has the MA_Menu script
    }

    public void MA_ToggleMenuOff(MA_Menu menu)
    {
        MA_PlayAudio(pressAudio);
        menu.showMenu = false; // Hides a Menu that has the MA_Menu script
    }

    public void MA_SelectUIElement(GameObject uiElement)
    {
        EventSystem.current.SetSelectedGameObject(uiElement);
    }

    public void MA_ToggleGameObject(GameObject target)
    {
        MA_PlayAudio(pressAudio);
        target.SetActive(!target.activeSelf); // Activates/Deactivates a GameObject
    }
}
