using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : SingletonMonoBehaviour<TaskManager>
{
    List<WindowBase> _windowList;

    protected override void Awake()
    {
        base.Awake();

        _windowList = new List<WindowBase>();
    }

    void Update()
    {
        _windowList.ForEach((window) =>
        {

        });
    }

    void AddWindow(WindowBase window)
    {
        _windowList.Add(window);
    }
}
