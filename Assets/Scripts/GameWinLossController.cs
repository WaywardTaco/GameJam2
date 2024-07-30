using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinLossController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver("GAME_WIN", OnWin);
        EventBroadcaster.Instance.AddObserver("GAME_LOSE", OnLose);
    }

    private void OnWin(){
        SceneManager.LoadScene("WinScreen");
    }

    private void OnLose(){
        SceneManager.LoadScene("LoseSceen");
    }
}
