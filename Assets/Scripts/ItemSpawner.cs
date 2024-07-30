using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private string spawnEventName;
    [SerializeField] private GameObject templateObject;
    [SerializeField] private List<Transform> spawnLocs;

    // Start is called before the first frame update
    void Start()
    {

        int childCount = gameObject.transform.childCount;
        for(int i = 0; i < childCount; i++){
            Transform child = gameObject.transform.GetChild(i);
            spawnLocs.Add(child);
        } 

        if(templateObject != null){
            templateObject.SetActive(false);
            EventBroadcaster.Instance.AddObserver(spawnEventName, this.Spawn);
        }
    }

    private void OnDisable() {
        EventBroadcaster.Instance.RemoveObserver(spawnEventName);
    }

    private void Spawn(){
        int random = Random.Range(0, spawnLocs.Count - 1);
        Transform chosenSpawn = spawnLocs[random];

        GameObject spawn = GameObject.Instantiate(templateObject);
        spawn.transform.position = chosenSpawn.transform.position;
        spawn.SetActive(true);
    }
}
