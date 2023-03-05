using UnityEngine;

public class SectionController : MonoBehaviour
{
    [HideInInspector]
    public Vector3Int Position;
    [HideInInspector]
    public bool IsVisited = false;

    [SerializeField]
    private GameObject _upWall;
    [SerializeField]
    private GameObject _leftWall;
    [SerializeField]
    private GameObject _rightWall;
    [SerializeField]
    private GameObject _downWall;
    [SerializeField]
    private GameObject _floor;
    [SerializeField]
    private GameObject _roof;
    [SerializeField]
    private GameObject _finish;

    public void ActiveWall(Walls selectedWall, bool isActive)
    {
        switch (selectedWall)
        {
            case Walls.LEFT:
                _leftWall.SetActive(isActive);
                break;
            case Walls.RIGHT:
                _rightWall.SetActive(isActive);
                break;
            case Walls.UP:
                _upWall.SetActive(isActive);
                break;
            case Walls.DOWN:
                _downWall.SetActive(isActive);
                break;
            case Walls.FLOOR:
                _floor.SetActive(isActive);
                break;
            case Walls.ROOF:
                _roof.SetActive(isActive);
                break;
            case Walls.FINISH:
                _floor.SetActive(!isActive);
                _finish.SetActive(isActive);
                break;
            default:
                break;
        }
    }
}

public enum Walls
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
    FLOOR,
    ROOF,
    FINISH
}
