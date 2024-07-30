using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathPlayer : MonoBehaviour
{
    
    [SerializeField] private float breathInterval;
    [SerializeField] private AudioClip[] breathSounds;    // an array of breath sounds that will be randomly selected from.
    [SerializeField] private AudioSource breathAudioSource;

    public void startTiredBreathLoop(float volume){
        this.StartCoroutine(PlayTiredBreath(volume));
    }

    public void stopTiredBreathLoop(){
        this.StopAllCoroutines();
    }

    private IEnumerator PlayTiredBreath(float volume){
        Debug.Log("Coroutine entered, will wait " + this.breathInterval);

        yield return new WaitForSeconds(breathInterval);

        PlayBreathSound(volume);
        this.StartCoroutine(PlayTiredBreath(volume));
    }

    public void PlayBreathSound(float volume)
    {
        breathAudioSource.volume = volume;

        // pick & play a random breath sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, breathSounds.Length);
        breathAudioSource.clip = breathSounds[n];
        breathAudioSource.PlayOneShot(breathAudioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        breathSounds[n] = breathSounds[0];
        breathSounds[0] = breathAudioSource.clip;
    }
}
