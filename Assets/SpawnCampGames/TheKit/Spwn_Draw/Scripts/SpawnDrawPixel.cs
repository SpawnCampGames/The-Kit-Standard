using UnityEngine;
using UnityEngine.UI;

public class SpawnDrawPixel : MonoBehaviour
{
    public Texture2D drawingTexture; // Main drawing texture
    public RawImage displayImage; // The RawImage component to display the texture
    public Color[] colors; // Array of colors for drawing
    public float brushSize = 5f; // Brush size

    private RectTransform canvasRectTransform;
    private Vector2 lastPosition;
    private Color currentColor;

    void Start()
    {
        InitializeCanvas();
        InitializeTexture();
        InitializeColors();
        Clear();
    }

    void Update()
    {
        HandleInput();
        if (Input.GetMouseButton(0)) Draw();
    }

    void InitializeCanvas()
    {
        canvasRectTransform = GetComponent<RectTransform>();
    }

    void InitializeTexture()
    {
        int width = (int)canvasRectTransform.rect.width;
        int height = (int)canvasRectTransform.rect.height;
        drawingTexture = new Texture2D(width, height);
        displayImage.texture = drawingTexture;
        drawingTexture.filterMode = FilterMode.Bilinear;
        drawingTexture.wrapMode = TextureWrapMode.Repeat;
    }

    void InitializeColors()
    {
        if (colors.Length > 0) currentColor = colors[0];
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentColor = colors[0];
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentColor = colors[1];
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentColor = colors[2];
        if (Input.GetKeyDown(KeyCode.Alpha4)) currentColor = colors[3];
        if (Input.GetKeyDown(KeyCode.Alpha5)) currentColor = colors[4];

        if (Input.GetMouseButtonDown(0))
        {
            lastPosition = GetTextureCoordinates(Input.mousePosition);
        }

        if (Input.GetKeyDown(KeyCode.X)) // Press 'X' to clear the drawing texture
        {
            Clear();
        }
    }

    void Draw()
    {
        Vector2 currentPosition = GetTextureCoordinates(Input.mousePosition);
        DrawLine(lastPosition, currentPosition, brushSize, currentColor);
        lastPosition = currentPosition;
    }

    Vector2 GetTextureCoordinates(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, null, out Vector2 localPoint);
        return new Vector2(
            (localPoint.x - canvasRectTransform.rect.x) / canvasRectTransform.rect.width,
            (localPoint.y - canvasRectTransform.rect.y) / canvasRectTransform.rect.height
        );
    }

    void DrawLine(Vector2 start, Vector2 end, float radius, Color color)
    {
        Vector2Int startPixel = Vector2Int.FloorToInt(start * new Vector2(drawingTexture.width, drawingTexture.height));
        Vector2Int endPixel = Vector2Int.FloorToInt(end * new Vector2(drawingTexture.width, drawingTexture.height));

        int dx = Mathf.Abs(endPixel.x - startPixel.x);
        int dy = Mathf.Abs(endPixel.y - startPixel.y);
        int sx = startPixel.x < endPixel.x ? 1 : -1;
        int sy = startPixel.y < endPixel.y ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            DrawCircle(startPixel, radius, color);

            if (startPixel == endPixel) break;
            int e2 = err * 2;
            if (e2 > -dy) { err -= dy; startPixel.x += sx; }
            if (e2 < dx) { err += dx; startPixel.y += sy; }
        }

        drawingTexture.Apply();
    }

    void DrawCircle(Vector2Int center, float radius, Color color)
    {
        int xMin = Mathf.Max(0, center.x - (int)radius);
        int xMax = Mathf.Min(drawingTexture.width, center.x + (int)radius);
        int yMin = Mathf.Max(0, center.y - (int)radius);
        int yMax = Mathf.Min(drawingTexture.height, center.y + (int)radius);

        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                Vector2 pixelPosition = new Vector2(x / (float)drawingTexture.width, y / (float)drawingTexture.height);
                float distance = Vector2.Distance(center / new Vector2(drawingTexture.width, drawingTexture.height), pixelPosition);

                if (distance <= radius)
                {
                    drawingTexture.SetPixel(x, y, color);
                }
            }
        }
    }

    public void Clear()
    {
        Color[] clearPixels = new Color[drawingTexture.width * drawingTexture.height];
        for (int i = 0; i < clearPixels.Length; i++) clearPixels[i] = Color.clear;
        drawingTexture.SetPixels(clearPixels);
        drawingTexture.Apply();
    }
}
