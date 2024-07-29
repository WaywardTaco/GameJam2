using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string OpenTriggerEvent = "";
    [SerializeField] private string CloseTriggerEvent = "";
    [SerializeField] private AudioClip openSound;
    private AudioSource audioSource;

    private void Start() {
        if (OpenTriggerEvent != "")
            EventBroadcaster.Instance.AddObserver(OpenTriggerEvent, OpenDoor);
        if (CloseTriggerEvent != "")
            EventBroadcaster.Instance.AddObserver(CloseTriggerEvent, CloseDoor);

        this.audioSource = GetComponent<AudioSource>();
        this.audioSource.clip = this.openSound;
    }

    private void OpenDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.audioSource.Play();
    }

    private void CloseDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
