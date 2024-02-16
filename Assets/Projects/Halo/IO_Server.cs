using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using System.Threading;
using System;

public class IO_Server : MonoBehaviour
{
    Thread thread;
    public int connectionPort = 25001;
    TcpListener server;
    TcpClient client;
    bool running;
    
    public GameObject FaceMesh;
    public int notification;
    public ReloadTexture reloadTexture;


    void Start()
    {
        Debug.Log("!!! IO_Server Started");
        // Receive on a separate thread so Unity doesn't freeze waiting for data
        ThreadStart ts = new ThreadStart(GetData);
        thread = new Thread(ts);
        thread.Start();
    }

    void GetData()
    {
        // Create the server
        Debug.Log("!!! Thread Started");
        server = new TcpListener(IPAddress.Any, connectionPort);
        server.Start();
        Debug.Log("!!! Server Started");

        // Create a client to get the data stream
        client = server.AcceptTcpClient();

        // Start listening
        running = true;
        while (running)
        {
            Connection();
        }
        server.Stop();
        Debug.Log("!!! Server Stopped");
    }

    void Connection()
    {
        // Read data from the network stream
        NetworkStream nwStream = client.GetStream();
        byte[] buffer = new byte[client.ReceiveBufferSize];
        int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

        // Decode the bytes into an integer
        notification = BitConverter.ToInt32(buffer, 0);

        // Process the received notification
        Interpreter();
    }

    void Interpreter()
    {
        if (notification == 1) //good to go
        {
            Debug.Log("!!! Notif = 1");
            reloadTexture.TextureLoader();
            Debug.Log(reloadTexture.canLoadTexture);
            notification = 0;

            SendResponse(0);
        }

        if (notification > 1)
        {
            SendResponse(2);
            Debug.Log("!!! Notif != 1");
        }
    }

    void SendResponse(int responseCode)
    {
        NetworkStream nwStream = client.GetStream();
        byte[] responseBytes = BitConverter.GetBytes(responseCode);
        nwStream.Write(responseBytes, 0, responseBytes.Length);
    }
}