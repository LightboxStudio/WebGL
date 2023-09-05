using System;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    public void TakeScreenshot()
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
        ScreenCapture.CaptureScreenshot($"{timestamp}-{QueryParameters.values["modelId"]}.png");
    }
}

