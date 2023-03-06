using UnityEngine;

/// <summary>
/// Контроллер камеры
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    [SerializeField]
    private float _speedZoom;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    /// <summary>
    /// Зум камеры
    /// </summary>
    /// <param name="zoom"></param>
    public void Zoom(int zoom)
    {
        _camera.fieldOfView += (zoom * _speedZoom);
    }
}
