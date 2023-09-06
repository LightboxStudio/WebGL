using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace WebGLScreenshotTool
{
    [AddComponentMenu("WebGLScreenshot/WebGLScreenshotTool")]
    public class WebGLScreenshotTool : MonoBehaviour
    {
        private string _fileName = "Screenshot";
        private bool _withDateInFileName = false;

        [Space]
        [Tooltip("The Canvases you want to ignore for screenshotting. This list of canvases will be used for the 'TakeScreenshotIgnoringSpecificCanvas' method.")]
        [SerializeField] private List<Canvas> _canvasesToIgnore;

        /// <summary>
        /// This constant variable is used to replace the value of _fileName when you leave it empty.
        /// </summary>
        private const string DEFAULT_FILE_NAME = "Screenshot";

        /// <summary>
        /// WebGLScreenshotTool instances is used to access the screenshot methods:
        /// TakeScreenshot() and TakeScreenshotIgnoringSpecificCanvas().
        /// </summary>
        public static WebGLScreenshotTool instance;

        private void Awake()
        {
            instance = this;
        }

        /// <summary>
        /// Method that takes the screenshot of all the UIs and objects of the scene that is displayed in the main camera.
        /// </summary>
        public void TakeScreenshot()
        {
#if UNITY_EDITOR
            Debug.LogWarning("Screenshot is only possible in WebGL build.");
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
                StartCoroutine(TakeScreenshotCoroutine());
#endif
        }

        /// <summary>
        /// Method that takes the screenshot of all the UIs and objects of the scene that is displayed in the main camera,
        /// except the list of canvases _canvasesToIgnore.
        /// <summary>
        public void TakeScreenshotIgnoringSpecificCanvas()
        {
#if UNITY_EDITOR
            Debug.LogWarning("Screenshot is only possible in WebGL build.");
#endif

#if UNITY_WEBGL && !UNITY_EDITOR
                StartCoroutine(TakeScreenshotCoroutine(_canvasesToIgnore));
#endif
        }

        /// <summary>
        /// Method to add a new canvas to the list of canvases _canvasesToIgnore.
        /// <summary>
        /// <param name="newCanvas">The canvas to add to the list _canvasesToIgnore.</param>
        public void AddNewCanvasToIgnore(Canvas newCanvas)
        {
            _canvasesToIgnore.Add(newCanvas);
        }

        /// <summary>
        /// Coroutine to disable all canvases to take screenshot.
        /// </summary>
        /// <param name="canvasToIgnore">Canvases to disable in the method.</param>
        private IEnumerator DisableCanvasCoroutine(List<Canvas> canvasToIgnore)
        {
            foreach (var canvas in canvasToIgnore)
            {
                canvas.enabled = false;
            }
            yield return null;
        }

        /// <summary>
        /// Coroutine to enable all canvases disabled after finished taking screenshot.
        /// </summary>
        /// <param name="canvasToIgnore">Canvases to enable in the method.</param>
        private IEnumerator EnableCanvasCoroutine(List<Canvas> canvasToIgnore)
        {
            foreach (var canvas in canvasToIgnore)
            {
                canvas.enabled = true;
            }
            yield return null;
        }

        [DllImport("__Internal")]
        private static extern string DownloadScreenshot(byte[] array, int byteLength, string fileName);

        /// <summary>
        /// Coroutine to take screenshot.
        /// </summary>
        private IEnumerator TakeScreenshotCoroutine()
        {
            yield return new WaitForEndOfFrame();

            int width = Screen.width;
            int height = Screen.height;
            Texture2D texture2D = new Texture2D(width, height, TextureFormat.RGBAFloat, false);

            texture2D.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            texture2D.Apply();

            byte[] bytes = ImageConversion.EncodeArrayToPNG(texture2D.GetRawTextureData(), texture2D.graphicsFormat, (uint)width, (uint)height);
            UnityEngine.Object.Destroy(texture2D);

#if UNITY_WEBGL && !UNITY_EDITOR
            var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss", System.Globalization.CultureInfo.InvariantCulture);
            var filename = $"{timestamp}-{QueryParameters.values["modelId"]}.png";
            DownloadScreenshot(bytes, bytes.Length, filename);
#endif

            yield return null;
        }

        /// <summary>
        /// Coroutine to take screenshot. Ignoring the specific canvas.
        /// </summary>
        /// <param name="canvasToIgnore">Canvases to enable and disable to take screenshot.</param>
        private IEnumerator TakeScreenshotCoroutine(List<Canvas> canvasToIgnore)
        {
            yield return DisableCanvasCoroutine(canvasToIgnore);
            yield return TakeScreenshotCoroutine();
            yield return EnableCanvasCoroutine(canvasToIgnore);
        }
    }
}
