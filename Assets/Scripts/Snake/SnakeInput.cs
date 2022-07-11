using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeInput : MonoBehaviour
{
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
    }
    public Vector2 GetDirectionToClick(Vector2 headPositon)
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition = _camera.ScreenToViewportPoint(mousePosition);
        mousePosition.y = 0.5f;
        mousePosition = _camera.ViewportToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - headPositon.x, mousePosition.y - headPositon.y);
        return direction;
    }
}
