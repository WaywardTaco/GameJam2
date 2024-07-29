using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float pickupDistance;
    [SerializeField] private float pickupSpeed;
    [SerializeField] private float pickupRotSpeed = 10.0f;
    [SerializeField] private GameObject itemPoint;
    [SerializeField] private GameObject pickupPromptRef;
    [SerializeField] private GameObject findItemPromptRef;
    
    private bool interactPress;
    private bool itemRelease;
    private Vector2 screenCenter;
    private GameObject currentItem;

    // Start is called before the first frame update
    void Start()
    {
        this.interactPress = false;
        this.itemRelease = true;
        this.screenCenter = new Vector2 (Screen.width/2, Screen.height/2); //prolly middle of screen :D
        this.pickupDistance = 3.0f;
        this.currentItem = null;
        this.pickupPromptRef.SetActive(false);
        this.findItemPromptRef.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckInteract();
        this.CheckCanPickupItem();

        if(this.interactPress)
        {
            Debug.Log("E Pressed");

            //If user currently has no held object, then check if user
            //is pointing at an item and make it the held object
            if(this.currentItem == null)
            {
                this.currentItem = this.GetHitObject(6);
                if(this.currentItem != null)
                    this.itemRelease = false;
            }
            //If user is holding an object, then release it
            else if(this.currentItem != null)
            {
                this.itemRelease = true;
            }
                
        }
    }

    void FixedUpdate()
    {
        this.HoverItem();
    }

    private void CheckInteract()
    {
        if(Input.GetKeyDown(KeyCode.E))
            this.interactPress = true;
        else
            this.interactPress = false;
    }

    private void CheckCanPickupItem(){
        GameObject item = this.GetHitObject(6);
        if(item != null && this.itemRelease)
            this.pickupPromptRef.SetActive(true);
        else
            this.pickupPromptRef.SetActive(false);

        item = this.GetHitObject(9);
        if(item != null){
            string progressText = item.GetComponent<ItemProgress>().GetProgressText();
            this.findItemPromptRef.GetComponent<Text>().text = progressText;
            this.findItemPromptRef.SetActive(true);
        }
        else
            this.findItemPromptRef.SetActive(false);
    }

    //Taken from APDEV :D
    private GameObject GetHitObject(int targetLayer){
        GameObject hitObject = null;
        int layerMask = 1 << targetLayer; //only check for objects in user layer 6

        Ray ray = Camera.main.ScreenPointToRay(this.screenCenter);
        if(Physics.Raycast(ray, out RaycastHit hit, this.pickupDistance, layerMask))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log("object hit");
        }
            
        return hitObject;
    }

    private void HoverItem()
    {
        //If there is a hitObject and user is not releasing, then move that item to ItemPoint
        if (!this.itemRelease && this.currentItem != null)
        {
            if(this.currentItem.GetComponent<Rigidbody>() != null)
            {
                this.currentItem.GetComponent<Rigidbody>().useGravity = false;
                this.currentItem.GetComponent<Rigidbody>().freezeRotation = true;
                this.currentItem.GetComponent<Collider>().excludeLayers = 1 << 8; //exclude layer 8 (player) from collisions

                this.currentItem.GetComponent<AudioSource>().volume = 0.5f;
                this.currentItem.transform.position = Vector3.MoveTowards(this.currentItem.transform.position, this.itemPoint.transform.position, this.pickupSpeed);
                this.currentItem.transform.rotation = Quaternion.RotateTowards(this.currentItem.transform.rotation, this.itemPoint.transform.rotation, this.pickupRotSpeed);
            }
        }

        //If user is releasing the object then enable gravity and collisions and set currentItem to null
        else if(this.itemRelease && this.currentItem != null)
        {
            if (this.currentItem.GetComponent<Rigidbody>() != null)
            {
                this.currentItem.GetComponent<Rigidbody>().useGravity = true;
                this.currentItem.GetComponent<Rigidbody>().freezeRotation = false;
                this.currentItem.GetComponent<AudioSource>().volume = 0.8f;
                this.currentItem = null;
            }
        }
    }

    public void ReleaseItemForPedestal(){
        if (this.currentItem.GetComponent<Rigidbody>() != null)
        {
            this.currentItem.GetComponent<Rigidbody>().useGravity = false;
            this.currentItem.GetComponent<Rigidbody>().freezeRotation = false;
            this.currentItem = null;
            this.itemRelease = true;
        }
    }
}
