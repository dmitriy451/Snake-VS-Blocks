using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Wall _wallTemplate;

    private Vector3 _leftWallPosition;
    private Vector3 _rightWallPosition;
    void Start()
    {
        _leftWallPosition = _mainCamera.ViewportToWorldPoint(new Vector3(0,0.5f,0));
        _leftWallPosition.z = _mainCamera.transform.position.z * -1;
        _rightWallPosition = _mainCamera.ViewportToWorldPoint(new Vector3(1,0.5f, 0));
        _rightWallPosition.z = _mainCamera.transform.position.z * -1;
        _wallTemplate.transform.localScale = new Vector3(_wallTemplate.transform.localScale.x,10, _wallTemplate.transform.localScale.z);
        Instantiate(_wallTemplate, _leftWallPosition, Quaternion.identity, _mainCamera.transform);
        Instantiate(_wallTemplate, _rightWallPosition, Quaternion.identity, _mainCamera.transform);
    }
}
