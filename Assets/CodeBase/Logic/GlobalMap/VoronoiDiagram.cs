using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class VoronoiDiagram : MonoBehaviour
    {
        [SerializeField] private Texture2D _texture;
        [SerializeField] private int _resolution = 128;
        [SerializeField] private Vector3[] _points;
        [SerializeField] private Color[] _colors;
        [SerializeField] private Vector3 _maximumPoint;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private int _regionAmount;

        private void Start()
        {
            _regionAmount = _points.Length;
            _renderer.material.mainTexture = GetDiagram();
        }

        private Texture2D GetDiagram()
        {
            Vector2Int[] points = new Vector2Int[_regionAmount];
            Color[] colors = new Color[_regionAmount];
            List<Vector2Int> pointsInPixel = ConvertToPixelCoordinates();

            for (int i = 0; i < _regionAmount; i++)
            {
                points[i] = new Vector2Int(pointsInPixel[i].x, pointsInPixel[i].y);
                colors[i] = new Color(_colors[i].r, _colors[i].g, _colors[i].b);
            }

            Color[] pixelColors = new Color[_resolution * _resolution];
            for (int x = 0; x < _resolution; x++)
            {
                for (int y = 0; y < _resolution; y++)
                {
                    int index = x * _resolution + y;
                    pixelColors[index] = colors[GetClosestPointIndex(new Vector2Int(x, y), points)];
                }
            }
            return GetImageFromColorArray(pixelColors);
        }

        private Texture2D GetImageFromColorArray(Color[] pixelColors)
        {
            Texture2D texture = new Texture2D(_resolution, _resolution);
            texture.wrapMode = TextureWrapMode.Clamp;
            texture.filterMode = FilterMode.Point;
            texture.SetPixels(pixelColors);
            texture.Apply();
            return texture;
        }

        private int GetClosestPointIndex(Vector2Int point, Vector2Int[] points)
        {
            float minDistance = float.MaxValue;
            int index = 0;

            for (int i = 0; i < points.Length; i++)
            {
                float distance = Vector2Int.Distance(point, points[i]);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    index = i;
                }
            }
            return index;
        }
        private List<Vector2Int> ConvertToPixelCoordinates()
        {
            List<Vector2Int> convertedPoints = new List<Vector2Int>();

            for (int i = 0; i < _points.Length; i++)
            {
                int x = Mathf.RoundToInt(_points[i].x * _resolution / _maximumPoint.x);
                int y = Mathf.RoundToInt(_points[i].z * _resolution / _maximumPoint.z);
                convertedPoints.Add(new Vector2Int(x, y));
            }

            return convertedPoints;
        }
    }
}

