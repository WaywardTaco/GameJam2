using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool interactPress;
    private Vector2 screenCenter;
    private GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        this.interactPress = false;
        this.screenCenter = new Vector2 (Screen.width/2, Screen.height/2); //prolly middle of screen :D
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckInteract();

        if(this.interactPress)
        {
            Debug.Log("E Pressed");
            this.item = this.GetHitObject(this.screenCenter);

            if(this.item != null)
                this.item.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
        }
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
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log("object hit");
        }
            
        return hitObject;
    }

}
