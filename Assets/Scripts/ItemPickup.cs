using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private float pickupDistance;
    [SerializeField] private float speed;
    [SerializeField] private GameObject itemPoint;
    
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
        this.speed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckInteract();

        if(this.interactPress)
        {
            Debug.Log("E Pressed");

            //If user currently has no held object, then check if user
            //is pointing at an item and make it the held object
            if(this.currentItem == null)
            {
                this.currentItem = this.GetHitObject();
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

    //Taken from APDEV :D
    private GameObject GetHitObject(){
        GameObject hitObject = null;
        int layerMask = 1 << 6; //only check for objects in user layer 6

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
                this.currentItem.transform.position = Vector3.MoveTowards(this.currentItem.transform.position, this.itemPoint.transform.position, this.speed * Time.deltaTime);
            }
        }

        //If user is releasing the object then enable gravity and set currentItem to null
        else if(this.itemRelease && this.currentItem != null)
        {
            if (this.currentItem.GetComponent<Rigidbody>() != null)
            {
                this.currentItem.GetComponent<Rigidbody>().useGravity = true;
                this.currentItem = null;
            }
        }
    }

}
