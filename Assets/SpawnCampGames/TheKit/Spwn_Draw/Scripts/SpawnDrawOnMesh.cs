using UnityEngine;

public class SpawnDrawOnMesh : MonoBehaviour
{
    public Renderer targetRenderer;
    public int textureSize = 512;
    public float brushSize = 5f;

    private Texture2D drawingTexture;
    [SerializeField]private Color drawingColor;
    private Vector2 lastUV;

    void Start()
    {
        InitializeTexture();
        InitializeColors();
        Clear();
    }

    void Update()
    {
        HandleInput();
        if(Input.GetMouseButton(0)) Draw();
    }

    void InitializeTexture()
    {
        drawingTexture = new Texture2D(textureSize,textureSize);
        drawingTexture.filterMode = FilterMode.Bilinear;
        drawingTexture.wrapMode = TextureWrapMode.Clamp;
        Clear();

        // Assign texture to material
        targetRenderer.material.mainTexture = drawingTexture;
    }

    void InitializeColors()
    {
        
    }

    void HandleInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(GetUVCoord(out Vector2 uv)) lastUV = uv;
        }

        if(Input.GetKeyDown(KeyCode.X)) Clear();
    }

    void Draw()
    {
        if(GetUVCoord(out Vector2 uv))
        {
            DrawLine(lastUV,uv,brushSize,drawingColor);
            lastUV = uv;
        }
    }

    bool GetUVCoord(out Vector2 uv)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit) && hit.collider == targetRenderer.GetComponent<Collider>())
        {
            uv = hit.textureCoord;
            uv.x *= textureSize;
            uv.y *= textureSize;
            return true;
        }
        uv = Vector2.zero;
        return false;
    }

    void DrawLine(Vector2 start,Vector2 end,float radius,Color color)
    {
        Vector2Int startPixel = Vector2Int.FloorToInt(start);
        Vector2Int endPixel = Vector2Int.FloorToInt(end);

        int dx = Mathf.Abs(endPixel.x - startPixel.x);
        int dy = Mathf.Abs(endPixel.y - startPixel.y);
        int sx = startPixel.x < endPixel.x ? 1 : -1;
        int sy = startPixel.y < endPixel.y ? 1 : -1;
        int err = dx - dy;

        while(true)
        {
            DrawCircle(startPixel,radius,color);

            if(startPixel == endPixel) break;
            int e2 = err * 2;
            if(e2 > -dy) { err -= dy; startPixel.x += sx; }
            if(e2 < dx) { err += dx; startPixel.y += sy; }
        }

        drawingTexture.Apply();
    }

    void DrawCircle(Vector2Int center,float radius,Color color)
    {
        int xMin = Mathf.Max(0,center.x - (int)radius);
        int xMax = Mathf.Min(textureSize,center.x + (int)radius);
        int yMin = Mathf.Max(0,center.y - (int)radius);
        int yMax = Mathf.Min(textureSize,center.y + (int)radius);

        for(int x = xMin; x < xMax; x++)
        {
            for(int y = yMin; y < yMax; y++)
            {
                Vector2 pixelPosition = new Vector2(x,y);
                float distance = Vector2.Distance(center,pixelPosition);

                if(distance <= radius) drawingTexture.SetPixel(x,y,color);
            }
        }
    }

    [SerializeField] Color startingColor = Color.white;

    public void Clear()
    {
        Color[] clearPixels = new Color[textureSize * textureSize];
        for(int i = 0; i < clearPixels.Length; i++) clearPixels[i] = startingColor;
        drawingTexture.SetPixels(clearPixels);
        drawingTexture.Apply();
    }
}