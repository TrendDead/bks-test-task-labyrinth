using UnityEngine;

/// <summary>
/// Конроллер игры
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _playerPrefab;
    [SerializeField]
    private LevelGenerator _levelGenerator;
    [SerializeField]
    private MazeRotator _mazeRotator;
    [SerializeField]
    private LockedMouse _locked;
    [SerializeField]
    private InputController _inputController;
    [SerializeField]
    private CameraRotateController _cameraRotateController;
    [SerializeField]
    private MainMenu _mainMenu;

    private PlayerController _player;
    private Vector3 spawnPosition;
    public void StartGame(Vector3Int sizeLevel, bool isStartGame)
    {
        DeleteOldMaze();
        _levelGenerator.GenerateLevel(sizeLevel, ref spawnPosition);
        if (isStartGame)
        {
            _mazeRotator.IsConstantRotation = false;
            _player = Instantiate(_playerPrefab, _levelGenerator.transform.position + spawnPosition + new Vector3(0.5f, -0.2f, -0.5f), Quaternion.identity);
            _player.EndLevel += FinishLevel;
            _mazeRotator.Rotating(Vector3.right, 10);
            _locked.LockMouse(true);
            _inputController.enabled = true;
            _cameraRotateController.enabled = true;
        }
        else
        {
            if (_player != null)
            {
                _player.EndLevel -= FinishLevel;
                Destroy(_player.gameObject);
            }
            _mazeRotator.IsConstantRotation = true;
            _inputController.enabled = false;
            _cameraRotateController.enabled = false;
        }
    }

    public void FinishLevel()
    {
        _mainMenu.gameObject.SetActive(true);
    }

    private void DeleteOldMaze()
    {
        Transform[] objects = _levelGenerator.GetComponentsInChildren<Transform>();

        for (int i = 1; i < objects.Length; i++)
        {
            Destroy(objects[i].gameObject);
        }
    }
}
