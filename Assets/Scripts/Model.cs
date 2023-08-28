using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PrefabLightmapData))]
public class Model : MonoBehaviour
{
    [Header("References")]
    private List<Material> materials = new List<Material>();

    [Header("Data")]
    public string id;

    void Awake()
    {
        FindMaterialReferences(this.transform);
    }

    void FindMaterialReferences(Transform current)
    {
        var renderers = current.GetComponents<MeshRenderer>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(renderer.materials);
        }
        foreach (Transform child in current)
        {
            FindMaterialReferences(child);
        }
    }

    async void Start()
    {
        foreach (var material in materials)
        {
            try
            {
                var materialName = material.name.Replace(" (Instance)", "");
                var textureId = QueryParameters.values[materialName];
                var texture = await TextureDownloader.GetTexture(textureId);
                material.mainTexture = texture;
            }
            catch { }
        }
    }
}
