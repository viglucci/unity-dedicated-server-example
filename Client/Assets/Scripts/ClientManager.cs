using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

public class ClientManager : MonoBehaviour
{
    public static ClientManager Instance;

    [SerializeField] public string ip;
    [SerializeField] public int port;
    private int _myId = 0;
    private TcpTransport _tcpTransport;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning(
                $"Instance of {GetType().Name} already exists. Newly created instance will be destroyed.");
            Destroy(this);
        }
    }

    private void Start()
    {
        _tcpTransport = new TcpTransport(ip, port);
    }

    public void Connect()
    {
        _tcpTransport.Connect();
    }
}