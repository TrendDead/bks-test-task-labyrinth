using UnityEngine;

/// <summary>
/// Вращение лабиринта
/// </summary>
public class MazeRotator : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private CameraRotateController _cameraRotateController;

    /// <summary>
    /// Вращение лабиринта с учетом поворота камеры
    /// </summary>
    /// <param name="vectorRotate"></param>
    /// <param name="corficent"></param>
    public void Rotating(Vector3 vectorRotate, int corficent)
    {
        transform.RotateAround(Vector3.zero, AddCameraAngle(transform.position, vectorRotate) * corficent, 1 * _speed);
    }

    private Vector3 AddCameraAngle(Vector2 direction, Vector3 axis)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + _cameraRotateController.transform.eulerAngles.y;
        return (Quaternion.Euler(0f, targetAngle, 0f) * axis).normalized;
    }    

}
