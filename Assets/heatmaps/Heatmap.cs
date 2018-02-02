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

    public Material[] material;

    public int count;

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
                temp.Add(new Vector4(data[i+j].X, data[i+j].Y - 1f, parameters[0], parameters[1]));
            }
            positions.Add(temp);
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