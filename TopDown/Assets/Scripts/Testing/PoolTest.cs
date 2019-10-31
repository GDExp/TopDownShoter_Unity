using System;
using System.Collections.Generic;
using UnityEngine;
using GameCore;

public enum CreateType
{
    Cube,
    Sphere,
    Cylinder,
}

public class PoolTest : MonoBehaviour
{
    public List<GameObject> testObjects;
    public CreateType createType;

    private void Start()
    {
        testObjects = new List<GameObject>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Type objectType = null;

            switch (createType)
            {
                case (CreateType.Cube):
                    objectType = typeof(CubeType);
                    break;
                case (CreateType.Cylinder):
                    objectType = typeof(CylinderType);
                    break;
                case (CreateType.Sphere):
                    objectType = typeof(SphereType);
                    break;
                default:
                    break;
            }

            var obj = GameController.Instance.objectPool.ProvideObject(objectType) as GameObject;
            testObjects.Add(obj);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            for(int i = 0; i < testObjects.Count; ++i)
            {
                GameController.Instance.objectPool.ReturnObject(testObjects[i]);
            }
            testObjects.Clear();
        }
    }
}
