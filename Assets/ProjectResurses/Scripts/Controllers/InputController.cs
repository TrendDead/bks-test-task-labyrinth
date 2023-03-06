using UnityEngine;

/// <summary>
/// Контроллер данных, вводимых пользователем, с клавиатуры
/// </summary>
public class InputController : MonoBehaviour
{
    [SerializeField]
    private MazeRotator _mazeRotator;
    [SerializeField]
    private CameraZoom _cameraZoomController;
    [SerializeField]
    private CameraRotateController _cameraRotateController;
    [SerializeField]
    private GameController _gameController;

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            _mazeRotator.Rotating(Vector3.forward, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _mazeRotator.Rotating(Vector3.forward, 1);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _mazeRotator.Rotating(Vector3.right, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _mazeRotator.Rotating(Vector3.right, -1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            _cameraZoomController.Zoom(-1);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            _cameraZoomController.Zoom(1);
        }
        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            _cameraRotateController.RotationCamera(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            _gameController.FinishLevel();
        }
    }
}
