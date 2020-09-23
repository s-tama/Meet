using UnityEngine;
using UnityEngine.EventSystems;

public class DebugUIDrag : MonoBehaviour
{
    Vector2 _prevPos;

    void Update()
    {
        Vector2 currentPos = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            _prevPos = Input.mousePosition;
            _prevPos.x -= Screen.width / 2;
            _prevPos.y -= Screen.height / 2;
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (!hit) return;
            if (!hit.transform.CompareTag("Window")) return;

            currentPos = Input.mousePosition;
            currentPos.x -= Screen.width / 2;
            currentPos.y -= Screen.height / 2;
            Vector2 move = currentPos - _prevPos;
            hit.transform.localPosition += new Vector3(move.x, move.y, 0);
            _prevPos = currentPos;
        }
    }
}
