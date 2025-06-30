using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpatialHashing<T> where T : Component
{
    public float cellsize;
    public Dictionary<Vector2Int, HashSet<T>> cells;
    public Dictionary<T, Vector2Int> unitCells;
    public SpatialHashing(float cellsize)
    {
        this.cellsize = cellsize;
        cells = new Dictionary<Vector2Int, HashSet<T>>();
        unitCells = new Dictionary<T, Vector2Int>();
    }

    public Vector2Int GetCell(Vector2 pos)
    {
        return new Vector2Int(
            Mathf.FloorToInt(pos.x / cellsize),
            Mathf.FloorToInt(pos.y / cellsize)
        );
    }

    public void Add(T unit)
    {
        Vector2 pos = unit.transform.position;
        Vector2Int cell = GetCell(pos);

        if (!cells.TryGetValue(cell, out var list))
        {
            list = new HashSet<T>();
            cells.Add(cell, list);
        }
        list.Add(unit);
        unitCells.Add(unit, cell);

    }
    public void Remove(T unit)
    {
        if (unitCells.TryGetValue(unit, out var cell) && cells.TryGetValue(cell, out var list))
        {
            list.Remove(unit);
            if (list.Count == 0) cells.Remove(cell);

        }
        unitCells.Remove(unit);
    }
    public void UpdateUnit(T unit)
    {
        Vector2 pos = unit.transform.position;
        Vector2Int newcell = GetCell(pos);

        if (!unitCells.TryGetValue(unit, out var oldcell) || oldcell != newcell)
        {
            Remove(unit);

            Add(unit);
        }
    }
    public List<T> QueryNearby(Vector2 pos, int radiusInCells = 1)
    {
        var center = GetCell(pos);
        List<T> result = new List<T>();

        for (int dx = -radiusInCells; dx <= radiusInCells; dx++)
            for (int dy = -radiusInCells; dy <= radiusInCells; dy++)
            {
                var cell = new Vector2Int(center.x + dx, center.y + dy);
                if (cells.TryGetValue(cell, out var bucket))
                    result.AddRange(bucket);
            }

        return result;
    }
    public int Count()
    {
        return unitCells.Count;
    }
}
