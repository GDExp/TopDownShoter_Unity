using System;
using UnityEngine;

public interface IPoolableObject
{
    GameObject GetObjectReference();
    Type GetObjectType();
    void EnablePoolObject();
    void DisablePoolObject(Transform storage);
}