using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Компонент основного меню
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameController _gameController;
    [SerializeField]
    private LockedMouse _lockedMouse;
    [SerializeField]
    private InputField _inputFieldX;
    [SerializeField]
    private InputField _inputFieldY;
    [SerializeField]
    private InputField _inputFieldZ;

    /// <summary>
    /// Запустить игру с пользовательскими параметрами
    /// </summary>
    public void StartGenerateMaze()
    {
        if (_inputFieldX.text != "" && _inputFieldY.text != "" && _inputFieldZ.text != "")
        {
            _gameController.StartGame(new Vector3Int(int.Parse(_inputFieldX.text), int.Parse(_inputFieldY.text), int.Parse(_inputFieldZ.text)), true);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    private void OnEnable()
    {
        _gameController.StartGame(new Vector3Int(3, 3, 3), false);
        _lockedMouse.LockMouse(false);
    }

    private void OnDisable()
    {
        _lockedMouse.LockMouse(true);
    }
}
