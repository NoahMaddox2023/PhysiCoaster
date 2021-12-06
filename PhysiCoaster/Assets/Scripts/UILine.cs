using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UILine : Graphic
{
    public Vector2Int gridSize;
    public List<Vector2> points;
    public UIGrid grid;

    private string path1, path2;

    float width;
    float height;
    float unitWidth;
    float unitHeight;

    public float thickness = 10.0f;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridSize.x;
        unitHeight = height / (float)gridSize.y;

        if (points.Count < 2)
        {
            return;
        }

        float angle = 0;

        for (int i = 0; i < points.Count; i++)
        {
            Vector2 point = points[i];

            if (i < points.Count - 1)
            {
                angle = GetAngle(points[i], points[i + 1]) + 45.0f;
            }

            DrawVertices(point, vh, angle);
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 2);
            vh.AddTriangle(index + 3, index + 2, index + 0);
        }
    }

    public float GetAngle(Vector2 point, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - point.y, target.x - point.x) * (180 / Mathf.PI));
    }

    void DrawVertices(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }

    private void Start()
    {
        points.Clear();
        path1 = Application.dataPath + "/PotentialPoints.txt";
        path2 = Application.dataPath + "/KineticPoints.txt";
        ReadFile();
    }

    private void Update()
    {
        if (grid != null)
        {
            if (gridSize != grid.gridSize)
            {
                gridSize = grid.gridSize;
                SetVerticesDirty();
            }
        }
    }

    private void ReadFile()
    {
        switch (this.gameObject.tag)
        {
            case "KineticEnergy":
                using (StreamReader sr = File.OpenText(path1))
                {
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] halves = s.Split(',');
                        points.Add(new Vector2(float.Parse(halves[0]) / 2.0f, float.Parse(halves[1]) / 363.75f * 5));
                    }
                }
                    break;
            case "PotentialEnergy":
                using (StreamReader sr = File.OpenText(path2))
                {
                    string s;

                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] halves = s.Split(',');
                        points.Add(new Vector2(float.Parse(halves[0]) / 2.0f, float.Parse(halves[1]) / 363.75f * 5));
                    }
                }
                break;
        }
    }
}
