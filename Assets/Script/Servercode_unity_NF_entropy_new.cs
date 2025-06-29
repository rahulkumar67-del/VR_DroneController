using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TMPro;
using System.Collections;
using UnityEngine.Rendering;

public class NeuropypeServer : MonoBehaviour
{
    public int unityServerPort = 8888;
    public static NeuropypeServer instance;
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private byte[] buffer = new byte[1024];
    private string receivedData;
    private float sampleEntropy;
    private float Focusindex;
    private bool newDataReceived = false;
    private string filePath;

    public float minVignetteIntensity = 0f;
    public float maxVignetteIntensity = 0.6f;
    public Transform FloatingTexttransform;
    public GameObject FloatingtextPrefab;
    public float minBlur = 10f; // Minimum blur (Focus Distance)
    public float maxBlur = 1f;

    private UnityEngine.Rendering.Universal.Vignette _vignette;// Vignette effect
    private UnityEngine.Rendering.Universal.DepthOfField _depthOfField;
    public GameObject Fps_cam; // Camera with the Volume component
    private Volume _volume; // URP Volume component

    void Awake()
    {
        instance = this;
        Focusindex = 10;
        filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FocusData.csv");
        InitializeCSV();
        StartServer();
        _volume = Fps_cam.GetComponent<Volume>();
        if (_volume == null)
        {
            Debug.LogError("Volume component not found on Fps_cam!");
            return;
        }

        // Try to get the Vignette effect from the Volume's profile
        if (!_volume.profile.TryGet(out _vignette))
        {
            Debug.LogError("Vignette effect not found in the Volume profile!");
        }

        // Try to get the Depth of Field effect from the Volume's profile
        if (!_volume.profile.TryGet(out _depthOfField))
        {
            Debug.LogError("Depth of Field effect not found in the Volume profile!");
        }

    }

    void InitializeCSV()
    {
        if (!File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("Timestamp,SampleEntropy,FocusIndex");
            }
        }
    }

    void StartServer()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, unityServerPort);
            server.Start();
            Debug.Log("Unity server started, listening on port " + unityServerPort);
            server.BeginAcceptTcpClient(HandleIncomingConnection, null);
        }
        catch (Exception e)
        {
            Debug.Log("Error starting Unity server: " + e.Message);
        }
    }

    void HandleIncomingConnection(IAsyncResult result)
    {
        client = server.EndAcceptTcpClient(result);
        stream = client.GetStream();
        Debug.Log("Client connected from: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
        StartReadingData();
    }

    void StartReadingData()
    {
        stream.BeginRead(buffer, 0, buffer.Length, ReadData, null);
    }

    void ReadData(IAsyncResult result)
    {
        try
        {
            int bytesRead = stream.EndRead(result);
            if (bytesRead > 0)
            {
                receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Debug.Log("Received data from Neuropype: " + receivedData);
                newDataReceived = true;
                StartReadingData();
            }
            else
            {
                Debug.Log("Connection closed by client");
                stream.Close();
                client.Close();
                server.BeginAcceptTcpClient(HandleIncomingConnection, null);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error reading data: " + e.Message);
        }
    }

    void Update()
    {
        if (newDataReceived)
        {
            ProcessReceivedData();
            SaveToCSV();
            newDataReceived = false;
        }
        EnviromentEffect(GetSampleEntropy());
    }

    void ProcessReceivedData()
    {
        try
        {
            sampleEntropy = float.Parse(receivedData.Trim());
            Debug.Log("Sample Entropy: " + sampleEntropy);
            CalculateFocusIndex();
        }
        catch (FormatException)
        {
            Debug.Log("Received data is not a valid float value.");
            sampleEntropy = 0f;
        }
    }

    void CalculateFocusIndex()
    {
        if (sampleEntropy < 1f)
        {
            Debug.Log("Focus is decreasing");
            
            Focusindex -= 1;
           
            showFloatingText("-1");
        }
        else if (sampleEntropy > 1f && sampleEntropy <= 2f)
        {
            Debug.Log("Middle focus level");
            showFloatingText("+1");
           
            Focusindex += 1;
        }
        else if (sampleEntropy > 2f && sampleEntropy <= 10f)
        {
            

            Debug.Log("High focus level");
            showFloatingText("+2");
            Focusindex += 2f;
        }
    }

   
    private void EnviromentEffect(float fi)
    {
        if (fi >2)
        {
            float vignetteIntensity = 0f;
            _vignette.intensity.Override(vignetteIntensity);
            float blur = 10f;
            _depthOfField.focusDistance.Override(blur);
        }
        if(fi < 2)
        {
            float vignetteIntensity = 0.5f;
            _vignette.intensity.Override(vignetteIntensity);
            float blur = 0.5f;
            _depthOfField.focusDistance.Override(blur);
        }
    }
    void showFloatingText(string FI)
    {
        var text = Instantiate(FloatingtextPrefab, FloatingTexttransform.transform.position, Quaternion.identity, FloatingTexttransform.transform);
        text.GetComponent<TextMeshPro>().text = FI.ToString();
    }
    void SaveToCSV()
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            writer.WriteLine($"{timestamp},{sampleEntropy},{Focusindex}");
        }
        Debug.Log("Data saved to CSV");
    }

    public float GetSampleEntropy()
    {
        return sampleEntropy;
    }

    public float GetFocusIndex()
    {
        return Focusindex;
    }

    void OnDestroy()
    {
        if (server != null)
        {
            server.Stop();
            Debug.Log("Unity server stopped");
        }
    }
}

