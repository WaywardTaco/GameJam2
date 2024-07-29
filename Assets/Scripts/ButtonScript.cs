using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject objectivePanel;

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadTitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
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
