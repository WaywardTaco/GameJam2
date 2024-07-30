using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenEvent : MonoBehaviour
{
    private float waitTime = 3.0f;
    AsyncOperation titlePreload;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(DoNotDestroyOnLoad.Instance.gameObject);
        this.StartCoroutine(this.WaitTransitionToTitle());
        titlePreload = SceneManager.LoadSceneAsync("TitleScreen");
        titlePreload.allowSceneActivation = false;

    }

    IEnumerator WaitTransitionToTitle(){
        yield return new WaitForSeconds(waitTime);
        titlePreload.allowSceneActivation = true;
    }
}
