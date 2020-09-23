using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
            }

            return _instance;
        }
    }

    virtual protected void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
    }
}
