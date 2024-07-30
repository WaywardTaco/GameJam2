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
        if (OpenTriggerEvent != "" && OpenTriggerEvent != "_DisableOnLoad")
            EventBroadcaster.Instance.AddObserver(OpenTriggerEvent, OpenDoor);
        if (CloseTriggerEvent != "")
            EventBroadcaster.Instance.AddObserver(CloseTriggerEvent, CloseDoor);

        this.audioSource = GetComponent<AudioSource>();
        if(this.audioSource != null)
            this.audioSource.clip = this.openSound;

        if(OpenTriggerEvent == "_DisableOnLoad")
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDisable(){
        EventBroadcaster.Instance.RemoveObserver(OpenTriggerEvent);
        EventBroadcaster.Instance.RemoveObserver(CloseTriggerEvent);
    }

    private void OpenDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.audioSource.Play();
    }

    private void CloseDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
