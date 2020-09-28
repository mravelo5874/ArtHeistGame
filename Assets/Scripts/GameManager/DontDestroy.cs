using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // https://answers.unity.com/questions/408518/dontdestroyonload-duplicate-object-in-a-singleton.html
 public class DontDestroy<Instance> : MonoBehaviour where Instance : DontDestroy<Instance>
 {
    public static Instance instance;
    public bool isPersistant;
    
    public virtual void Awake() 
    {
        if(isPersistant) 
        {
            if(!instance) 
            {
                instance = this as Instance;
            }
            else 
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            instance = this as Instance;
        }
    }
 }