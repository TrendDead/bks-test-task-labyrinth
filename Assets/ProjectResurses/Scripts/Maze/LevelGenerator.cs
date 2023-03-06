using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генератор уровня куба
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private SectionController _sectionPrefab;

    private SectionController[,,] _sections;
    private Vector3 _positionadjustment;
    private Quaternion _zeroQuaternion = Quaternion.Euler(0f, 0f, 0f);
    /// <summary>
    /// Создание уровня
    /// </summary>
    /// <param name="sizeLevel">Размер уровня</param>
    public void GenerateLevel( Vector3Int sizeLevel, ref Vector3 positionadjustment)
    {
        transform.position = Vector3.zero;
        transform.rotation = _zeroQuaternion;

        sizeLevel.x = sizeLevel.x < 2 ? 2 : sizeLevel.x;
        sizeLevel.y = sizeLevel.y < 2 ? 2 : sizeLevel.y;
        sizeLevel.z = sizeLevel.z < 1 ? 1 : sizeLevel.z;

        positionadjustment = new Vector3(-((float)sizeLevel.x / 2), (float)sizeLevel.z / 2, (float)sizeLevel.y / 2);

        _sections = new SectionController[sizeLevel.x, sizeLevel.y, sizeLevel.z];

        for (int z = 0; z < sizeLevel.z; z++)
        {
            for (int i = 0; i < sizeLevel.x; i++)
            {
                for (int j = 0; j < sizeLevel.y; j++)
                {
                    _sections[i, j, z] = Instantiate(_sectionPrefab, transform);
                    _sections[i, j, z].transform.position = new Vector3(i, -z, -j) + positionadjustment;
                    _sections[i, j, z].Position = new Vector3Int(i, j, z);
                    if (i == sizeLevel.x - 1)
                    {
                        _sections[i, j, z].ActiveWall(Walls.RIGHT, true);
                    }
                    if(j != 0)
                    {
                        _sections[i, j, z].ActiveWall(Walls.UP, true, true);
                    }
                    if (j == sizeLevel.y - 1)
                    {
                        _sections[i, j, z].ActiveWall(Walls.DOWN, true);
                    }
                    if (i != 0)
                    {
                        _sections[i, j, z].ActiveWall(Walls.LEFT, true, true);
                    }
                    if(z == 0)
                    {
                        _sections[i, j, z].ActiveWall(Walls.ROOF, true);
                    }
                }
            }

            _sections[UnityEngine.Random.Range(0, sizeLevel.x - 1), UnityEngine.Random.Range(0, sizeLevel.y - 1), 
                z].ActiveWall(z == sizeLevel.z -1 ? Walls.FINISH : Walls.FLOOR, z == sizeLevel.z - 1 ? true : false);

            GenetatingPath(z, sizeLevel);
        }
    }

    /// <summary>
    /// Создания путей на определенном слое лабиринта
    /// </summary>
    /// <param name="row">Слой лабиринта</param>
    private void GenetatingPath(int row, Vector3Int sizeLevel)
    {
        SectionController current = _sections[UnityEngine.Random.Range(0, sizeLevel.x - 1), UnityEngine.Random.Range(0, sizeLevel.y - 1), row];
        current.IsVisited = true;

        Stack<SectionController> stack = new Stack<SectionController>();
        do
        {
            List<SectionController> unvisitedNeighbors = new List<SectionController>();
            unvisitedNeighbors = GetNeighbors(current, row, sizeLevel);

            if (unvisitedNeighbors.Count > 0)
            {
                SectionController newCurrent = unvisitedNeighbors[UnityEngine.Random.Range(0, unvisitedNeighbors.Count)];
                newCurrent.IsVisited = true;
                RemoveWall(current, newCurrent);
                current = newCurrent;
                stack.Push(newCurrent);
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    /// <summary>
    /// Получение соседей выбранной ячейки
    /// </summary>
    private List<SectionController> GetNeighbors(SectionController current, int row, Vector3Int sizeLevel)
    {
        List<SectionController> unvisitedNeighbors = new List<SectionController>();

        if (current.Position.x > 0 && !_sections[current.Position.x - 1, current.Position.y, row].IsVisited)
        {
            unvisitedNeighbors.Add(_sections[current.Position.x - 1, current.Position.y, row]);
        }
        if (current.Position.y > 0 && !_sections[current.Position.x, current.Position.y - 1, row].IsVisited)
        {
            unvisitedNeighbors.Add(_sections[current.Position.x, current.Position.y - 1, row]);
        }
        if (current.Position.x < sizeLevel.x - 1 && !_sections[current.Position.x + 1, current.Position.y, row].IsVisited)
        {
            unvisitedNeighbors.Add(_sections[current.Position.x + 1, current.Position.y, row]);
        }
        if (current.Position.y < sizeLevel.y - 1 && !_sections[current.Position.x, current.Position.y + 1, row].IsVisited)
        {
            unvisitedNeighbors.Add(_sections[current.Position.x, current.Position.y + 1, row]);
        }

        return unvisitedNeighbors;
    }

    /// <summary>
    /// Удаление стен между ячейками
    /// </summary>
    private void RemoveWall(SectionController current, SectionController newCurrent)
    {
        if (current.Position.x == newCurrent.Position.x)
        {
            if(current.Position.y > newCurrent.Position.y)
            {
                current.ActiveWall(Walls.UP, false);
                newCurrent.ActiveWall(Walls.DOWN, false);
            }
            else
            {
                newCurrent.ActiveWall(Walls.UP, false);
                current.ActiveWall(Walls.DOWN, false);
            }
        }
        else
        {
            if (current.Position.x > newCurrent.Position.x)
            {
                current.ActiveWall(Walls.LEFT, false);
                newCurrent.ActiveWall(Walls.RIGHT, false);
            }
            else
            {
                newCurrent.ActiveWall(Walls.LEFT, false);
                current.ActiveWall(Walls.RIGHT, false);
            }
        }
    }
}
