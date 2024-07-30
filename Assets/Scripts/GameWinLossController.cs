using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWinLossController : MonoBehaviour
{
    [SerializeField] private bool inDebug = false;
    [SerializeField] private bool runWin = false;
    [SerializeField] private bool runLose = false;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver("GAME_WIN", OnWin);
        EventBroadcaster.Instance.AddObserver("GAME_LOSE", OnLose);
    }

    void Update(){
        if (!inDebug) return;

        if(runWin) {
            runWin = false;
            this.OnWin();
        }

        if(runLose) {
            runLose = false;
            this.OnLose();
        }
    }

    private void OnWin(){
        SceneManager.LoadScene("WinScreen");
    }

    private void OnLose(){
        SceneManager.LoadScene("LoseScreen");
    }
}
