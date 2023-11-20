using System.Collections;
using System.Collections.Generic;
//�����
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
//using StructClass;
using UnityEngine.UI;


public class server : MonoBehaviour
{
    //����Ĭ�϶���˽�еĳ�Ա
    Socket socket; //Ŀ��socket
    EndPoint clientEnd; //�ͻ���
    IPEndPoint ipEnd; //�����˿�
    string recvStr; //���յ��ַ���
    string sendStr; //���͵��ַ���
    byte[] recvData = new byte[1024]; //���յ����ݣ�����Ϊ�ֽ�
    byte[] sendData = new byte[1024]; //���͵����ݣ�����Ϊ�ֽ�
    int recvLen; //���յ����ݳ���
    Thread connectThread; //�����߳�

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
    //��ʼ��
    void InitSocket()
    {
        //���������˿�,�����κ�IP
        ipEnd = new IPEndPoint(IPAddress.Any, InPort);
        //�����׽�������,�����߳��ж���
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        //�������Ҫ��ip
        socket.Bind(ipEnd);
        //����ͻ���
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        clientEnd = (EndPoint)sender;
        print("waiting for UDP dgram");

        //����һ���߳����ӣ�����ģ��������߳̿���
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }

    void SocketSend(string sendStr)
    {
        //Debug.Log(sendStr);
        //��շ��ͻ���
        sendData = new byte[1024];
        //��������ת��
        sendData = Encoding.Unicode.GetBytes(sendStr);
        //���͸�ָ���ͻ���
        socket.SendTo(sendData, sendData.Length, SocketFlags.None, clientEnd);
    }

    //����������
    void SocketReceive()
    {
        //�������ѭ��
        while (true)
        {
            //��data����
            recvData = new byte[1024];
            //��ȡ�ͻ��ˣ���ȡ�ͻ������ݣ������ø��ͻ��˸�ֵ
            recvLen = socket.ReceiveFrom(recvData, ref clientEnd);
            print("message from: " + clientEnd.ToString()); //��ӡ�ͻ�����Ϣ
                                                            //������յ�������
            recvStr = Encoding.Unicode.GetString(recvData, 0, recvLen);
            print("���Ƿ����������յ��ͻ��˵�����" + recvStr);
            //�����յ������ݾ��������ٷ��ͳ�ȥ
            sendStr = "From Server: " + recvStr;
            if (recvStr == "Client Ready!")
                isClient = true;
            SocketSend(sendStr);
        }
    }

    //���ӹر�
    void SocketQuit()
    {
        //�ر��߳�
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //���ر�socket
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

        InitSocket(); //�������ʼ��server
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