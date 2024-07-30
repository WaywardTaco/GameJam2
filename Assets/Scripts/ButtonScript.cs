using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject TitleCanvas;
    [SerializeField] GameObject TutorialCanvas;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject objectivePanel;

    private void Start() {
        TitleCanvas.SetActive(true);
        controlPanel.SetActive(false);
        objectivePanel.SetActive(false);
        TutorialCanvas.SetActive(false);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTutorial()
    {
        // SceneManager.LoadScene("Tutorial");
        this.TitleCanvas.SetActive(false);
        this.TutorialCanvas.SetActive(true);
    }
    public void LoadTitleScreen()
    {
        // SceneManager.LoadScene("TitleScreen");
        this.TutorialCanvas.SetActive(false);
        this.TitleCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SwitchControlPanel()
    {
        if(this.controlPanel.activeInHierarchy) this.controlPanel.SetActive(false);
        else this.controlPanel.SetActive(true);
    }

    public void SwitchObjectivePanel()
    {
        if (this.objectivePanel.activeInHierarchy) this.objectivePanel.SetActive(false);
        else this.objectivePanel.SetActive(true);
    }
}
