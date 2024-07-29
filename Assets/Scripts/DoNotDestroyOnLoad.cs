using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    public static DoNotDestroyOnLoad Instance;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(this.gameObject);        
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
