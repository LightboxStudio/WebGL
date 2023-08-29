using System;
using System.Collections.Generic;
using UnityEngine;

public class ModelLoader : MonoBehaviour
{
    [Header("References")]
    public static ModelLoader instance;
    public List<Model> modelPrefabs = new List<Model>();
    public Transform placementPoint;
    public Transform placementIndicator;

    [Header("Data")]
    private Dictionary<string, Model> models = new Dictionary<string, Model>();

    void Awake()
    {
        instance = this;
        try
        {
            CheckForDependencies();
        }
        catch (Exception e)
        {
            Application.ExternalCall("alert", e.ToString());
            return;
        }
        AssignToDictionary();
        LoadModel();
    }

    private void CheckForDependencies()
    {
        if (placementPoint == null)
        {
            throw new Exception("Missing reference to the placement point (ModelLoader component).");
        }

        if (placementIndicator == null)
        {
            throw new Exception("Missing reference to the placement indicator (ModelLoader component).");
        }

        if (modelPrefabs.Count == 0)
        {
            throw new Exception("No model prefabs set (ModeLoader component).");
        }
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
            if (instance.models.TryGetValue(id, out Model model))
            {
                var placementClone = Instantiate(model.gameObject, instance.placementIndicator.position, Quaternion.identity);
                placementClone.transform.SetParent(instance.placementIndicator);
                placementClone.transform.localScale = Vector3.one * 0.5f;

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
