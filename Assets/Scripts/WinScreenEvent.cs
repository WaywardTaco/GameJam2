using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenEvent : MonoBehaviour
{
    [SerializeField] private float waitTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.StartCoroutine(this.WaitTransitionToTitle());
    }

    IEnumerator WaitTransitionToTitle(){
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene("TitleScreen");
    }
}
