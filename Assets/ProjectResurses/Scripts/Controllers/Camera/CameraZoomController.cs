using UnityEngine;

/// <summary>
/// Контроллер камеры
/// </summary>
[RequireComponent(typeof(Camera))]
public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    private float _speedZoom;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    public void CameraZoom(int zoom)
    {
        _camera.fieldOfView += (zoom * _speedZoom);
    }
}
