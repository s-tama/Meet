using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum WindowType
{
    None,
    Whiteboard
}

public class WindowManager : SingletonMonoBehaviour<WindowManager>
{
    [SerializeField] Transform _parent = null;

    List<WindowBase> _windowList;
    List<WindowBase> _closeWindow;

    Vector2 _prevMousePos;

    void Update()
    {
        if (_windowList == null) return;

        for (int i = 0; i < _windowList.Count; i++)
        {
            if(_windowList[i] != null)
            {
                _windowList[i].UpdateWindow();
            }
            else
            {
                _windowList.RemoveAt(i);
            }
        }

        MoveWindow();
    }

    public WindowBase CreateWindow(WindowType type)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Window/" + type.ToString());
        GameObject go = Instantiate(prefab, _parent);
        WindowBase window = go.GetComponent<WindowBase>();
        Debug.Assert(window != null, "WindowBaseの取得に失敗しました");
        window.Init(type);
        AddWindow(window);
        return window;
    }

    public void CloseWindow(WindowBase window)
    {
        Destroy(window.gameObject);
    }

    void AddWindow(WindowBase window)
    {
        if(_windowList == null)
        {
            _windowList = new List<WindowBase>();
        }

        _windowList.Add(window);
    }

    void MoveWindow()
    {
        Vector2 currentMousePos = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            _prevMousePos = Input.mousePosition;
            _prevMousePos.x -= Screen.width / 2;
            _prevMousePos.y -= Screen.height / 2;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (!hit) return;
            if (!hit.transform.CompareTag("Window")) return;

            currentMousePos = Input.mousePosition;
            currentMousePos.x -= Screen.width / 2;
            currentMousePos.y -= Screen.height / 2;
            Vector2 move = currentMousePos - _prevMousePos;
            hit.transform.localPosition += new Vector3(move.x, move.y, 0);
            _prevMousePos = currentMousePos;
        }
    }
}
