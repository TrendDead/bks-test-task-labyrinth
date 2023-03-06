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
    [SerializeField]
    private Material _interiorWallMaterial;

    public void ActiveWall(Walls selectedWall, bool isActive, bool isInteriorWall = false)
    {
        switch (selectedWall)
        {
            case Walls.LEFT:
                _leftWall.SetActive(isActive);
                if (isInteriorWall)
                    _leftWall.GetComponent<MeshRenderer>().material = _interiorWallMaterial;
                break;
            case Walls.RIGHT:
                _rightWall.SetActive(isActive);
                if (isInteriorWall)
                    _rightWall.GetComponent<MeshRenderer>().material = _interiorWallMaterial;
                break;
            case Walls.UP:
                _upWall.SetActive(isActive);
                if (isInteriorWall)
                    _upWall.GetComponent<MeshRenderer>().material = _interiorWallMaterial;
                break;
            case Walls.DOWN:
                _downWall.SetActive(isActive);
                if (isInteriorWall)
                    _downWall.GetComponent<MeshRenderer>().material = _interiorWallMaterial;
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
