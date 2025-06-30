using UnityEngine;

public class DrawCell2D
{
    public Vector2Int cellSize;
    public float borderWidth;
    public GameObject cellObject;

    public DrawCell2D(Vector2Int cellSize, float borderWidth)
    {
        this.cellSize = cellSize;
        this.borderWidth = borderWidth;
        this.cellObject = new GameObject("DrawCell2D");
    }

    public void SetPos(Vector2 pos)
    {
        cellObject.transform.position = pos - (Vector2)cellSize / 2f;
    }

    public void Activate() => cellObject.SetActive(true);
    public void Deactivate() => cellObject.SetActive(false);

    public void Draw(Material lineMaterial)
    {
        LineRenderer line = cellObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.positionCount = 5;

        Vector3[] corners = new Vector3[]
        {
            new Vector3(0, 0, 0), // bottom left
            new Vector3(cellSize.x, 0, 0), // bottom right
            new Vector3(cellSize.x, cellSize.y, 0), // top right
            new Vector3(0, cellSize.y, 0), // top left
            new Vector3(0, 0, 0) // back to bottom left to close
        };

        line.SetPositions(corners);
        line.startWidth = borderWidth;
        line.endWidth = borderWidth;
        line.material = lineMaterial;
        line.sortingOrder = 10;
    }
}
