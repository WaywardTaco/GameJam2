using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool interactPress;
    private bool interactRelease;
    private Vector2 screenCenter;
    //private GameObject hitItem;
    private GameObject currentItem;
    public float pickupDistance;

    // Start is called before the first frame update
    void Start()
    {
        this.interactPress = false;
        this.interactRelease = true;
        this.screenCenter = new Vector2 (Screen.width/2, Screen.height/2); //prolly middle of screen :D
        this.pickupDistance = 2.0f;
        this.currentItem = null;
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckInteract();

        if(this.interactPress)
        {
            Debug.Log("E Pressed");

            if(this.currentItem == null)
            {
                this.currentItem = this.GetHitObject(this.screenCenter);
                this.interactRelease = false;
            }
            else if(this.currentItem != null)
            {
                this.interactRelease = true;
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

    private GameObject GetHitObject(Vector2 screenPoint){
        GameObject hitObject = null;
        int layerMask = 1 << 6; //only check for objects in user layer 6

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        if(Physics.Raycast(ray, out RaycastHit hit, this.pickupDistance, layerMask))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log("object hit");
        }
            
        return hitObject;
    }

    private void HoverItem()
    {
        if (!this.interactRelease && this.currentItem != null)
        {
            this.currentItem.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Acceleration);
        }
    }

}
