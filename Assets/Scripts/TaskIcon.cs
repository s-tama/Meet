using UnityEngine;
using UnityEngine.UI;

public class TaskIcon : MonoBehaviour
{
    [SerializeField] Button _button = null;
    [SerializeField] WindowType _type = WindowType.None;

    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            if (_type == WindowType.None)
            {
                Debug.LogError("WindowTypeが設定されていません");
                return;
            }

            WindowManager.Instance.CreateWindow(_type);
            Debug.Log("ウィンドウが生成されました : " + _type.ToString());
        });
    }
}
