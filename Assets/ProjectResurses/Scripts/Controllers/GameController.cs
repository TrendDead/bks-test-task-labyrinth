using UnityEngine;

/// <summary>
/// Конроллер игры
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerPrefab;
    [SerializeField]
    private LevelGenerator _levelGenerator;

    private void Awake()
    {
        _levelGenerator.EndLevelGenerate += StartGame;
    }

    private void OnDestroy()
    {
        _levelGenerator.EndLevelGenerate -= StartGame;
    }

    public void StartGame(Vector3 spawnPosition)
    {
        Instantiate(_playerPrefab, _levelGenerator.transform.position + spawnPosition + new Vector3(0.5f, -0.4f, -0.5f), Quaternion.identity);
    }
}
