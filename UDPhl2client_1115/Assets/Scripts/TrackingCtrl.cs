
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TrackingCtrl : MonoBehaviour
{

    [Tooltip("IP address to send to")]
    public string ServerIP = "192.168.1.107";

    public TMP_Text textBar;

    /// <summary>
    /// everything start from here
    /// </summary>
    public abstract void f_Init();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
