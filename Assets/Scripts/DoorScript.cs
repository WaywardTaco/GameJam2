using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string OpenTriggerEvent = "";
    [SerializeField] private string CloseTriggerEvent = "";

    private void Start() {
        if (OpenTriggerEvent != "")
            EventBroadcaster.Instance.AddObserver(OpenTriggerEvent, OpenDoor);
        if (CloseTriggerEvent != "")
            EventBroadcaster.Instance.AddObserver(CloseTriggerEvent, CloseDoor);
    }

    private void OpenDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void CloseDoor(){
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
