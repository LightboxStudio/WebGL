using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TextureDownloader : MonoBehaviour
{
    [Header("Data")]
    public static string TEXTURES_URL = "https://new.lightboxstudio.es/webgi/Configurator/";

    public async static Task<Texture2D> GetTexture(string textureId)
    {
        var texture = await DownloadTexture(textureId);
        return texture;
    }

    private static async Task<Texture2D> DownloadTexture(string textureId)
    {
        var uri = TEXTURES_URL + textureId;
        var request = UnityWebRequestTexture.GetTexture(uri);
        request.SendWebRequest();
        while (!request.isDone)
        {
            await Task.Yield();
        }

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Error: " + request.error); // Optinal: Call Errors
            return null;
        }
        return DownloadHandlerTexture.GetContent(request);
    }
}
