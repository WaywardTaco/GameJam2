using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void SwitchScene()
    {
        switch(this.name)
        {
            case "Start":
                SceneManager.LoadScene("GameScene");
                break;
            case "How to Play":
                SceneManager.LoadScene("Tutorial");
                break;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
