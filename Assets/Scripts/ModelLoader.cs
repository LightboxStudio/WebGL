using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    [Header("References")]
    public static ModelLoader instance;
    public List<Model> modelPrefabs = new List<Model>();
    public Transform placementPoint;

    [Header("Data")]
    private Dictionary<string, Model> models = new Dictionary<string, Model>();

    void Awake()
    {
        instance = this;
        AssignToDictionary();
        LoadModel();
    }

    private void AssignToDictionary()
    {
        foreach (var model in modelPrefabs)
        {
            models.Add(model.id, model);
        }
    }

    public static void LoadModel()
    {
        try
        {
            QueryParameters.CollectQueryParameters();
            string id = QueryParameters.values["modelId"];
            Model model = null;
            if (instance.models.TryGetValue(id, out model))
            {
                var clone = Instantiate(model.gameObject, instance.placementPoint.position, Quaternion.identity);
                clone.transform.SetParent(instance.placementPoint);
            }
            else
            {
                throw new Exception($"No model found with ID: {id}");
            }
        }
        catch (Exception e)
        {
            Application.ExternalCall("alert", e.ToString());
        }
    }
}
