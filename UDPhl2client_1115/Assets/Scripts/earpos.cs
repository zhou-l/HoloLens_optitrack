using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earpos : MonoBehaviour
{

    public TrackingCtrlUDP trackingCtrlUDP;
    string recvDataCube;
    string hhhCube;
    // Start is called before the first frame update
    void Start()
    {
        //string recvDataCube = trackingCtrlUDP.recvData;
        //hhhCube = GameObject.Find("UDP Communication").gameObject.GetComponent<TrackingCtrlUDP>().hhh;

        //Debug.Log("cube" + hhhCube);
        //Debug.Log("cube");
        //string a = "-0.542,0.511,0.667; 1.165,0.502,1.896; 329.265,320.905,44.081; 0.000,0.000,0.000; 0.000,0.000,0.000; 0.000,0.000,0.000; 0.000,0.000,0.000; 0.000,0.000,0.000";
        //string[] b = a.Split(';');
        //Debug.Log(b[1]);
        //foreach (string c in b)
        //    //Console.WriteLine(c);
        //Debug.Log(c);
        
    }

    // Update is called once per frame
    void Update()
    {
        recvDataCube = GameObject.Find("UDP Communication").gameObject.GetComponent<TrackingCtrlUDP>().recvData;
        string[] cubeposall = recvDataCube.Split(';');
        //Debug.Log(cubeposall[1]);
        string[] cubepos = cubeposall[1].Split(',');
        //Debug.Log("cubestart" + recvDataCube);
        //transform.position += new Vector3(1.5f * Time.deltaTime, 0, 0);
        string[] cubeposf = cubeposall[0].Split(',');
        transform.forward = new Vector3(float.Parse(cubeposf[0]), float.Parse(cubeposf[1]), float.Parse(cubeposf[2]));
        transform.localPosition = new Vector3(float.Parse(cubepos[0]), float.Parse(cubepos[1]), float.Parse(cubepos[2]));
    }
}