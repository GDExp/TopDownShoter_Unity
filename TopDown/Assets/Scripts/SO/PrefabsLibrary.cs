using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabsLibrary", menuName = "CustomSO/Test/Test Prefabs Library", order = 100)]
public class PrefabsLibrary : ScriptableObject
{
    public List<BaseProjectile> projectiles;

    public void GetConfigLibrary(Dictionary<Type, GameObject> objectLibrary)
    {
        for(int i = 0; i < projectiles.Count; ++i) objectLibrary.Add(projectiles[i].GetType(), projectiles[i].gameObject);
    }
}
