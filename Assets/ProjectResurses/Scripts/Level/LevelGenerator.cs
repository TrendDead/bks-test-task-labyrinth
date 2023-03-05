using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Генератор уровня куба
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private SectionController _sectionPrefab;
    [SerializeField]
    private Vector3Int _sizeLevel;

    private SectionController[,,] _sections;
    private void Start()
    {
        GenetatingLevel();
    }

    private void GenetatingLevel()
    {
        _sizeLevel.x = _sizeLevel.x < 2 ? 2 : _sizeLevel.x;
        _sizeLevel.y = _sizeLevel.y < 2 ? 2 : _sizeLevel.y;
        _sizeLevel.z = _sizeLevel.z < 1 ? 1 : _sizeLevel.z;

        _sections = new SectionController[_sizeLevel.x, _sizeLevel.y, _sizeLevel.z];

        for (int z = 0; z < _sizeLevel.z; z++)
        {
            for (int i = 0; i < _sizeLevel.x; i++)
            {
                for (int j = 0; j < _sizeLevel.y; j++)
                {
                    _sections[i, j, z] = Instantiate(_sectionPrefab, transform);
                    _sections[i, j, z].transform.position = new Vector3(i, -z, -j);
                    _sections[i, j, z].Position = new Vector3Int(i, j, z);
                    if (i == _sizeLevel.x - 1)
                    {
                        _sections[i, j, z].ActiveWall(Walls.RIGHT, true);
                    }
                    if (j == _sizeLevel.y - 1)
                    {
                        _sections[i, j, z].ActiveWall(Walls.DOWN, true);
                    }
                    if(z == 0)
                    {
                        _sections[i, j, z].ActiveWall(Walls.ROOF, true);
                    }
                }
            }

            _sections[Random.Range(0, _sizeLevel.x - 1), Random.Range(0, _sizeLevel.y - 1), 
                z].ActiveWall(z == _sizeLevel.z -1 ? Walls.FINISH : Walls.FLOOR, z == _sizeLevel.z - 1 ? true : false);

            GenetatingPath(z);
        }
    }

    /// <summary>
    /// Создания путей на определенном слое лабиринта
    /// </summary>
    /// <param name="row">Слой лабиринта</param>
    private void GenetatingPath(int row)
    {
        SectionController current = _sections[Random.Range(0, _sizeLevel.x - 1), Random.Range(0, _sizeLevel.y - 1), row];
        current.IsVisited = true;

        Stack<SectionController> stack = new Stack<SectionController>();
        do
        {
            List<SectionController> unvisitedNeighbors = new List<SectionController>();
            unvisitedNeighbors = GetNeighbors(current, row);

            if (unvisitedNeighbors.Count > 0)
            {
                SectionController newCurrent = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
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
    private List<SectionController> GetNeighbors(SectionController current, int row)
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
        if (current.Position.x < _sizeLevel.x - 1 && !_sections[current.Position.x + 1, current.Position.y, row].IsVisited)
        {
            unvisitedNeighbors.Add(_sections[current.Position.x + 1, current.Position.y, row]);
        }
        if (current.Position.y < _sizeLevel.y - 1 && !_sections[current.Position.x, current.Position.y + 1, row].IsVisited)
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
