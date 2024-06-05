using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;

public class VideoReceiver : MonoBehaviour
{
    public string ipAddress = "localhost"; // Replace with the IP address of the Python sender
    public int port = 9999;

    private TcpClient client;
    private NetworkStream stream;
    private Texture2D receivedTexture;

    private void Start()
    {
        client = new TcpClient(ipAddress, port);
        stream = client.GetStream();
        receivedTexture = new Texture2D(2, 2);
    }

    private void Update()
    {
        try
        {
            if (stream.CanRead)
            {
                // Read the message size
                byte[] sizeBytes = new byte[8];
                stream.Read(sizeBytes, 0, 8);
                long size = BitConverter.ToInt64(sizeBytes, 0);

                // Read the image data
                byte[] imageData = new byte[size];
                stream.Read(imageData, 0, (int)size);

                // Convert image data to texture
                receivedTexture.LoadImage(imageData);
                receivedTexture.Apply();

                // Display the texture on a GameObject's material
                GetComponent<Renderer>().material.mainTexture = receivedTexture;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    private void OnDestroy()
    {
        stream.Close();
        client.Close();
    }
}
