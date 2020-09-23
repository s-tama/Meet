using System;
using UnityEngine;
using UnityEngine.UI;

public class WindowBase : MonoBehaviour
{
    WindowType _type;

    [SerializeField] Button _buttonClose = null;
    [SerializeField] Button _buttonScale = null;

    public void Init(WindowType type)
    {
        _type = type;

        InitButton();
    }

    public void UpdateWindow()
    {
        if (_buttonClose == null) return;
        if (_buttonScale == null) return;
    }

    void InitButton()
    {
        _buttonClose.onClick.AddListener(() =>
        {
            WindowManager.Instance.CloseWindow(this);
        });
    }
}
