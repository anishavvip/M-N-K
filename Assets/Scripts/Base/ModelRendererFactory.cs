using UnityEngine;

public class ModelRendererFactory : IBaseGridFactory
{
    public int size { get; private set; }
    Vector3 StartOffset => new Vector3(0.5f, 0.5f, 0);

    public Color color => Color.black;
    public float lineWidth = 0.1f;

    public void CreateGrid(int size)
    {
        if (size < 3) return;

        SetCamBounds(size);
        PreFillGrid(size);
    }

    public void CreateModelRenderer(GameObject gameObject)
    {
        LineRenderer l = gameObject.AddComponent<LineRenderer>();
        l.material = new Material(Shader.Find("Mobile/Particles/Alpha Blended"));
        Vector3[] positions = new Vector3[4] { new Vector3(0, 0), new Vector3(1, 0), new Vector3(1, 1), new Vector3(0, 1) };

        for (int i = 0; i < positions.Length; i++)
            positions[i] -= StartOffset - gameObject.transform.position;

        l.startColor = color;
        l.endColor = color;
        l.startWidth = lineWidth;
        l.endWidth = lineWidth;
        l.loop = true;
        l.positionCount = 4;
        l.SetPositions(positions);
        l.useWorldSpace = true;
    }
    private static void SetCamBounds(int size)
    {
        Camera.main.transform.localPosition = new Vector3(((float)size / 2) - 0.5f, ((float)size / 2) - 0.5f, -10);
        Camera.main.orthographicSize = size * 0.6f;
    }
    private void PreFillGrid(int size)
    {
        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                FillGrid(i, j);
    }

    void FillGrid(int i, int j)
    {
        GameObject obj = new GameObject();
        obj.AddComponent<BoxCollider2D>();
        obj.AddComponent<SpriteRenderer>();
        Data data= obj.AddComponent<Data>();

        data.pos = new int[2] { i, j };
        obj.transform.position = new Vector2(i, j);
        obj.name = $"{i} | {j}";
        CreateModelRenderer(obj);
    }
}
