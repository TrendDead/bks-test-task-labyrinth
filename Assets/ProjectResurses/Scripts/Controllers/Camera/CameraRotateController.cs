using UnityEngine;

/// <summary>
/// Контроллер вращения камеры вокруг цели
/// </summary>
public class CameraRotateController : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private Vector2 _displacement;
    [SerializeField]
    private float _sensitivity = 3;
    [SerializeField]
    private float _limitYMax = 80;
    [SerializeField]
    private float _limitYMin = 4;

    private Vector3 offset;
    private float x, y;

    private void Start()
    {
        _limitYMax = Mathf.Abs(_limitYMax);
        if (_limitYMax > 90) _limitYMax = 90;
        offset = new Vector3(_displacement.x, _displacement.y, -Mathf.Abs(_radius));
        transform.position = _target.transform.position + offset;
    }

    /// <summary>
    /// Вращение камеры
    /// </summary>
    public void RotationCamera(Vector2 scroll)
    {
        x = transform.localEulerAngles.y + scroll.x * _sensitivity;
        y += scroll.y * _sensitivity;
        y = Mathf.Clamp(y, -_limitYMax, _limitYMin);
        transform.localEulerAngles = new Vector3(-y, x, 0);
        transform.position = transform.localRotation * offset + _target.transform.position;
    }

    private void OnDrawGizmos()
    {
        if (_target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_target.transform.position, _radius);
        }
    }
}
