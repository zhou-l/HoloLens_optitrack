using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
using Microsoft.MixedReality.Toolkit;
using MRTKExtensions.QRCodes;
using TMPro;

public class QRPos: MonoBehaviour
{
    [SerializeField]
    private TextMeshPro displayText;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("hh");
    }

    // Update is called once per frame
    void Update()
    {
        // 获取Player变量指定的对象的三围坐标
        GameObject fu = this.transform.parent.gameObject;

        Vector3 player_postion = fu.transform.position;



        // 获取X,Y,Z值

        float x = player_postion.x;

        float y = player_postion.y;

        float z = player_postion.z;



        string strx = Convert.ToString(x);

        string stry = Convert.ToString(y);

        string strz = Convert.ToString(z);


        UnityEngine.Debug.Log(strx);



        this.GetComponent<TMP_Text>().text = "(" + strx + "," + stry + "," + strz + ")";

    }
}
