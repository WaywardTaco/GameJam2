using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject TitleCanvas;
    [SerializeField] GameObject TutorialCanvas;
    [SerializeField] private AudioClip[] clickSounds;

    private void Start() {
        TitleCanvas.SetActive(true);
        // controlPanel.SetActive(false);
        // objectivePanel.SetActive(false);
        TutorialCanvas.SetActive(false);
    }

    public void LoadGame()
    {
        PlayClickSound();
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTutorial()
    {
        // SceneManager.LoadScene("Tutorial");
        PlayClickSound();
        this.TitleCanvas.SetActive(false);
        this.TutorialCanvas.SetActive(true);
    }
    public void LoadTitleScreen()
    {
        // SceneManager.LoadScene("TitleScreen");
        PlayClickSound();
        this.TutorialCanvas.SetActive(false);
        this.TitleCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        PlayClickSound();
        Application.Quit();
    }

    private void PlayClickSound()
    {
        AudioSource m_AudioSource = this.gameObject.GetComponent<AudioSource>();
        // pick & play a random click sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, clickSounds.Length);
        m_AudioSource.clip = clickSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        clickSounds[n] = clickSounds[0];
        clickSounds[0] = m_AudioSource.clip;
    }


    // public void SwitchControlPanel()
    // {
    //     if(this.controlPanel.activeInHierarchy) this.controlPanel.SetActive(false);
    //     else this.controlPanel.SetActive(true);
    // }

    // public void SwitchObjectivePanel()
    // {
    //     if (this.objectivePanel.activeInHierarchy) this.objectivePanel.SetActive(false);
    //     else this.objectivePanel.SetActive(true);
    // }
}
