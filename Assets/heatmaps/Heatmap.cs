// Alan Zucconi
// www.alanzucconi.com
using UnityEngine;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class Heatmap : MonoBehaviour
{
    public string file;

    public float maxX;
    public float minX;

    public float maxY;
    public float minY;

    public Material[] material;

    public int count;
    public int segments;

    TextReader textReader;
    CsvReader csv;

    List<TransformData> data;

    List<List<Vector4>> positions;
    public Vector2 parameters;

    private void Start()
    {
        data = new List<TransformData>();
        positions = new List<List<Vector4>>();

        textReader = File.OpenText(file);
        csv = new CsvReader(textReader);
        csv.Configuration.RegisterClassMap<TransformDataMap>();
        IEnumerable<TransformData> enumerable = csv.GetRecords<TransformData>();
        data = enumerable.ToList();
        for (int i = 0; i < data.Count; i+=count)
        {
            List<Vector4> temp = new List<Vector4>();
            for (int j = 0; j < count; j++)
            {
                if (i+j >= data.Count) { break; }
                CheckBoundaries(data[i + j].X, data[i + j].Y);
                temp.Add(new Vector4(data[i+j].X, data[i+j].Y - 1f, parameters[0], parameters[1]));
            }
            positions.Add(temp);
        }
        
        float ySize = maxY - minY;
        float xSize = maxX - minX;
        float yinc = ySize / segments;
        float xinc = xSize / segments;
        for (int i = 0; i < data.Count; i++)
        {
            for (int y = 0; y < segments; y++)
            {
                if (data[i].Y >= (y-1)*yinc && data[i].Y < y * yinc)
                {
                    for (int x = 0; x < segments; x++)
                    {
                        if (data[i].X >= (x - 1) * xinc && data[i].X < x * xinc)
                        {

                        }
                    }
                }
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < positions.Count; i++)
        {
            material[i].SetInt("_Points_Length", data.Count);
            material[i].SetVectorArray("_Points", positions[i]);
        }
    }

    void CheckBoundaries(float x, float y)
    {
        if (x >= maxX) { maxX = x; }
        if (x <= minX) { minX = x; }
        if (y >= maxY) { maxY = y; }
        if (y <= minY) { minY = y; }
    }

    public class TransformData
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public bool Gesturing { get; set; }

        public TransformData() { }

        public TransformData(float x, float y, float z, bool gesture)
        {
            X = x;
            Y = y;
            Z = z;
            Gesturing = gesture;
        }

    }

    public sealed class TransformDataMap : ClassMap<TransformData>
    {
        public TransformDataMap()
        {
            Map(m => m.X).Index(0);
            Map(m => m.Y).Index(1);
            Map(m => m.Z).Index(2);
            Map(m => m.Gesturing).Index(3);
        }
    }
}