using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemProgress : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private ItemPickup playerReference;
    [SerializeField] private string prevProgressEvent = "";
    [SerializeField] private string nextProgressEvent;
    [SerializeField] private float slideSpeed = 0.03f;
    [SerializeField] private float rotSpeed = 5.0f;
    private GameObject displayHolder;
    private GameObject displayObject;
    private GameObject detectedItem = null;
    private Collider detectionBox;

    private void Awake() {
        this.displayHolder = this.gameObject.transform.GetChild(0).gameObject;
        this.displayObject = displayHolder.transform.GetChild(0).gameObject;

        this.detectionBox = GetComponent<Collider>();
        if(detectionBox == null)
            Debug.LogError("Pedestall Missing Collider!");

        if(prevProgressEvent != ""){
            this.displayObject.SetActive(true);
            this.displayHolder.SetActive(false);
            this.detectionBox.enabled = false;
            EventBroadcaster.Instance.AddObserver(prevProgressEvent, this.ActivatePedestal);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Item Detected!");
        AudioSource itemSound = other.gameObject.GetComponent<AudioSource>();
        if(itemSound != null)
            itemSound.volume = 0.4f;

        this.DeactivatePedestal(other.gameObject);
    }

    private void ActivatePedestal(){
        this.displayHolder.SetActive(true);
        this.detectionBox.enabled = true;
        this.detectedItem = null;
    }

    private void DeactivatePedestal(GameObject item){
        Debug.Log("Pedestal Deactivated");
        this.detectedItem = item;
        playerReference.ReleaseItemForPedestal();
        detectedItem.layer = LayerMask.NameToLayer("Props");
        detectedItem.transform.parent = this.displayHolder.transform;
        this.detectionBox.enabled = false;
        this.StartCoroutine(this.moveItem());
    }

    private void FirePorgressEvent(){
        EventBroadcaster.Instance.PostEvent(nextProgressEvent);
    }

    private IEnumerator moveItem(){
        yield return new WaitForFixedUpdate();
        this.detectedItem.transform.position = Vector3.MoveTowards(this.detectedItem.transform.position, this.displayObject.transform.position, slideSpeed);
        this.detectedItem.transform.rotation = Quaternion.RotateTowards(this.detectedItem.transform.rotation, this.displayObject.transform.rotation, rotSpeed);
        
        if(this.detectedItem.transform.position == this.displayObject.transform.position && this.detectedItem.transform.rotation == this.displayObject.transform.rotation){
            this.detectedItem.transform.position = this.displayObject.transform.position;
            this.detectedItem.transform.rotation = this.displayObject.transform.rotation;
            this.displayObject.SetActive(false);
            this.FirePorgressEvent();
            Debug.Log("Final Position");
            yield return null;
        }
        else{
            Debug.Log("Moving Item");
            this.detectedItem.GetComponent<Collider>().enabled = false;
            this.StartCoroutine(this.moveItem());
        }
    }
}
