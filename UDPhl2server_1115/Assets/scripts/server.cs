using System.Collections;
using System.Collections.Generic;
//引入库
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
//using StructClass;
using UnityEngine.UI;


public class server : MonoBehaviour
{
    //以下默认都是私有的成员
    Socket socket; //目标socket
    EndPoint clientEnd; //客户端
    IPEndPoint ipEnd; //侦听端口
    string recvStr; //接收的字符串
    string sendStr; //发送的字符串
    byte[] recvData = new byte[1024]; //接收的数据，必须为字节
    byte[] sendData = new byte[1024]; //发送的数据，必须为字节
    int recvLen; //接收的数据长度
    Thread connectThread; //连接线程

    //public Dropdown dp_perspective;
    //public Toggle tg_lock;
    //public Slider sli_rotate;
    //public Button btn_Calib;
    float counter = 0;
    public int InPort = 5000;
    public int OutPort = 5001;



    //public OptitrackRigidBody Holo;
    //public OptitrackRigidBody LWrist;
    //public OptitrackRigidBody LElbow;
    //public OptitrackRigidBody LShoulder;
    //public OptitrackRigidBody RShoulder;
    public struct UpperSkeleton
    {
        Transform a;
        Transform b;
    }

    private Vector3[] dataStruct = new Vector3[8];
    bool isClient = false;
    //初始化
    void InitSocket()
    {
        //定义侦听端口,侦听任何IP
        ipEnd = new IPEndPoint(IPAddress.Any, InPort);
        //定义套接字类型,在主线程中定义
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //服务端需要绑定ip
        socket.Bind(ipEnd);
        //定义客户端
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        clientEnd = (EndPoint)sender;
        print("waiting for UDP dgram");

        //开启一个线程连接，必须的，否则主线程卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {
        //Debug.Log(sendStr);
        //清空发送缓存
        sendData = new byte[1024];
        //数据类型转换
        sendData = Encoding.Unicode.GetBytes(sendStr);
        //发送给指定客户端
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);
    }

    //服务器接收
    void SocketReceive()
    {
        //进入接收循环
        while (true)
        {
            //对data清零
            recvData = new byte[1024];
            //获取客户端，获取客户端数据，用引用给客户端赋值
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            print("message from: " + clientEnd.ToString()); //打印客户端信息
                                                            //输出接收到的数据
            recvStr = Encoding.Unicode.GetString(recvData, 0, recvLen);
            print("我是服务器，接收到客户端的数据" + recvStr);
            //将接收到的数据经过处理再发送出去
            sendStr = "From Server: " + recvStr;
            if (recvStr == "Client Ready!")
                isClient = true;
            SocketSend(sendStr);
        }
    }

    //连接关闭
    void SocketQuit()
    {
        //关闭线程
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket
        if (socket != null)
            socket.Close();
        print("disconnect");
    }

    // Use this for initialization
    void Start()
    {
        //dp_perspective.onValueChanged.AddListener(f_dp_perspective);
        //tg_lock.onValueChanged.AddListener(f_tg_lock);
        //sli_rotate.onValueChanged.AddListener(f_sli_rotate);
        //btn_Calib.onClick.AddListener(f_btn_Calib);

        InitSocket(); //在这里初始化server
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
    private void Update()
    {
        if (!isClient)
            return;

    }
    //private void f_dp_perspective(int input)
    //{
    //    sendStr = "perspective," + ((Perspective)input).ToString(); SocketSend(sendStr);
    //}
    //private void f_sli_rotate(float input)
    //{
    //    sendStr = "rotate," + input.ToString(); SocketSend(sendStr);
    //}
    //private void f_tg_lock(bool input)
    //{
    //    sendStr = "lock," + (input ? 1 : 0).ToString(); SocketSend(sendStr);
    //}
    //private void f_btn_Calib()
    //{
    //    sendStr = "Calibration,"; SocketSend(sendStr);
    //}

    private void FixedUpdate()
    {
        //Debug.Log(LShoulder.transform.position.ToString("F3") + " / " + Vector3.Distance(LShoulder.transform.position, LWrist.transform.position) + " / " + Vector3.Distance(LShoulder.transform.position, RShoulder.transform.position));
        if (!isClient)
            return;
        sendStr = string.Empty;

        //dataStruct[0] = Holo.transform.forward;
        //dataStruct[1] = Holo.transform.position;
        //dataStruct[2] = LWrist.transform.position;
        //dataStruct[3] = LElbow.transform.position;
        //dataStruct[4] = LShoulder.transform.position;
        //dataStruct[5] = RShoulder.transform.position;
        for (int i = 0; i < 8; i++)
        {
            sendStr = sendStr + dataStruct[i].x.ToString("F3") + ',';
            sendStr = sendStr + dataStruct[i].y.ToString("F3") + ',';
            sendStr = sendStr + dataStruct[i].z.ToString("F3") + ';';
        }

        //byte[] data = Encoding.Unicode.GetBytes(sendStr);// encode data using UTF8 encoding in binary format.

        SocketSend(sendStr);
        //client.Send(data, data.Length, remoteEndPoint); // Send the text to the remote client
    }
}