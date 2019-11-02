using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsLibrary", menuName = "CustomSO/Test/Test Prefabs Library", order = 100)]
public class PrefabsLibrary : ScriptableObject
{
    public List<BaseType> prefabs;


    public void GetConfigLibrary(Dictionary<Type, object> objectLibrary)
    {
        for(int i = 0; i < prefabs.Count; ++i)
        {
            Debug.Log($"type - {prefabs[i].GetType()}");
            objectLibrary.Add(prefabs[i].GetType(), prefabs[i].gameObject);
        }
    }
}
