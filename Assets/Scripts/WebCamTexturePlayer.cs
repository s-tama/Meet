//
// WebCameraTexturePlayer.cs
//
// Author : 
//

using UnityEngine;

public class WebCamTexturePlayer : MonoBehaviour
{

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        WebCamTexture webcamTexture = new WebCamTexture();
        webcamTexture.deviceName = devices[1].name;
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        Debug.Log(devices.Length);
    }
}
