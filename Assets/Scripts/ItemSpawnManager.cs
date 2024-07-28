using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private string item2SpawnEventName;
    [SerializeField] private string item3SpawnEventName;
    [SerializeField] private string item4SpawnEventName;
    [SerializeField] private string item5SpawnEventName;
    [SerializeField] private string item6SpawnEventName;
    [SerializeField] private string item7SpawnEventName;

    [Header("Debug")]
    [SerializeField] private bool inDebug = false;
    [SerializeField] private bool spawnItem2 = false;
    [SerializeField] private bool spawnItem3 = false;
    [SerializeField] private bool spawnItem4 = false;
    [SerializeField] private bool spawnItem5 = false;
    [SerializeField] private bool spawnItem6 = false;
    [SerializeField] private bool spawnItem7 = false;

    public static ItemSpawnManager Instance;

    void Awake(){
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!inDebug) return;

        this.processDebugSpawns();
    }

    private void processDebugSpawns(){
        if(spawnItem2){
            spawnItem2 = false;
            EventBroadcaster.Instance.PostEvent(item2SpawnEventName);
        }
        if(spawnItem3){
            spawnItem3 = false;
            EventBroadcaster.Instance.PostEvent(item3SpawnEventName);
        }
        if(spawnItem4){
            spawnItem4 = false;
            EventBroadcaster.Instance.PostEvent(item4SpawnEventName);
        }
        if(spawnItem5){
            spawnItem5 = false;
            EventBroadcaster.Instance.PostEvent(item5SpawnEventName);
        }
        if(spawnItem6){
            spawnItem6 = false;
            EventBroadcaster.Instance.PostEvent(item6SpawnEventName);
        }
        if(spawnItem7){
            spawnItem7 = false;
            EventBroadcaster.Instance.PostEvent(item7SpawnEventName);
        }
    }
}
