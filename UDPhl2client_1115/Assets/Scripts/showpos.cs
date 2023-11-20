using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showpos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("son"+this.gameObject.transform.localPosition);
        Debug.Log("parent"+this.transform.parent.gameObject.transform.position); 
    }
}
