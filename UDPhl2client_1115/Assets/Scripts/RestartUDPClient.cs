using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartUDPClient : TrackingCtrlUDP
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Restart ready");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        Debug.Log("Restart test");
        f_Init();
    }
}
