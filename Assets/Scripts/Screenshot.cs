using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveScreenshotWebGL(string filename, string data);
    public void TakeScreenshot()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
        var filename = $"{timestamp}-{QueryParameters.values["modelId"]}.png";
        var data = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG().ToString();
        SaveScreenshotWebGL(filename, data);
    }
}

