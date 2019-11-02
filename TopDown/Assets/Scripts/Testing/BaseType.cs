using System;
using UnityEngine;


public class BaseType : MonoBehaviour, IPoolableObject
{
    public Type GetObjectType()
    {
        return GetType();
    }
}
