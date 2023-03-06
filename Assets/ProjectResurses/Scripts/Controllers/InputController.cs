using UnityEngine;

/// <summary>
/// ���������� ������, �������� �������������, � ����������
/// </summary>
public class InputController : MonoBehaviour
{
    [SerializeField]
    private MazeRotator _mazeRotator;
    [SerializeField]
    private CameraZoomController _cameraZoomController;
    [SerializeField]
    private CameraRotateController _cameraRotateController;

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.2f)
        {
            _mazeRotator.Rotating(Vector3.forward, -1);
        }
        if (Input.GetAxis("Horizontal") < -0.2f)
        {
            _mazeRotator.Rotating(Vector3.forward, 1);
        }
        if (Input.GetAxis("Vertical") > 0.2f)
        {
            _mazeRotator.Rotating(Vector3.right, 1);
        }
        if (Input.GetAxis("Vertical") < -0.2f)
        {
            _mazeRotator.Rotating(Vector3.right, -1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _cameraZoomController.CameraZoom(-1);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _cameraZoomController.CameraZoom(1);
        }
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _cameraRotateController.RotationCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
    }
}
