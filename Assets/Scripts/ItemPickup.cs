using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool interactPress;

    // Start is called before the first frame update
    void Start()
    {
        this.interactPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckInteract();

        if(this.interactPress)
        {
            //this.GetHitObject(); get middle of screen
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

        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            hitObject = hit.collider.gameObject;

        return hitObject;
    }

}
