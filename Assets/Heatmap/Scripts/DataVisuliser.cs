using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

using CsvHelper;
using CsvHelper.Configuration;

public class DataVisuliser : MonoBehaviour
{
    List<Vector3> num1 = new List<Vector3>();
    List<Vector3> num2 = new List<Vector3>();
    List<Vector3> num3 = new List<Vector3>();
    List<Vector3> num4 = new List<Vector3>();
    List<Vector3> num5 = new List<Vector3>();
    List<Vector3> num6 = new List<Vector3>();
    List<Vector3> num7 = new List<Vector3>();
    List<Vector3> num8 = new List<Vector3>();
    List<Vector3> num9 = new List<Vector3>();
    List<Vector3> num10 = new List<Vector3>();

    public string file;

    public Material[] material;

    TextReader textReader;
    CsvReader csv;

    List<TransformData> data;

    // Heatmap Settings
    public int pointRadius = 40;                                

    public bool createPrimitive = false;                        

    public new Camera camera;

    Rect numberRect = new Rect(5, 5, 100, 30);

    [Space]
    public int displayNumber;

    public void OnGUI()
    {
        if (GUI.Button(numberRect, "Display"))
        {
            Texture2D heatmapImage;

            switch (displayNumber)
            {
                case 0:
                    heatmapImage = Heatmap.CreateHeatmap(num1.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 1:
                    heatmapImage = Heatmap.CreateHeatmap(num2.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 2:
                    heatmapImage = Heatmap.CreateHeatmap(num3.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 3:
                    heatmapImage = Heatmap.CreateHeatmap(num4.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 4:
                    heatmapImage = Heatmap.CreateHeatmap(num5.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 5:
                    heatmapImage = Heatmap.CreateHeatmap(num6.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 6:
                    heatmapImage = Heatmap.CreateHeatmap(num7.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 7:
                    heatmapImage = Heatmap.CreateHeatmap(num8.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 8:
                    heatmapImage = Heatmap.CreateHeatmap(num9.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
                case 9:
                    heatmapImage = Heatmap.CreateHeatmap(num10.ToArray(), camera, pointRadius);
                    Heatmap.CreateRenderPlane(heatmapImage);
                    break;
            }
        }
    }

    public Rect GUIToScreenRect(Rect guiRect)
    {
        return new Rect(guiRect.x, Screen.height - (guiRect.y + guiRect.height), guiRect.width, guiRect.height);
    }

    private void Start()
    {
        data = new List<TransformData>();

        textReader = File.OpenText(file);
        csv = new CsvReader(textReader);
        csv.Configuration.RegisterClassMap<TransformDataMap>();
        IEnumerable<TransformData> enumerable = csv.GetRecords<TransformData>();
        data = enumerable.ToList();

        for (int i = 0; i < data.Count; i++)
        {
            AddPoint(new Vector3(data[i].RightX, data[i].RightY, data[i].RightZ), data[i].GestureNumber);
        }
        Debug.Log("Loading Complete!!!");
    }

    void AddPoint(Vector3 pos, int num)
    {
        switch (num)
        {
            case 0:
                num1.Add(pos * 10f);
                break;
            case 1:
                num2.Add(pos * 10f);
                break;
            case 2:
                num3.Add(pos * 10f);
                break;
            case 3:
                num4.Add(pos * 10f);
                break;
            case 4:
                num5.Add(pos * 10f);
                break;
            case 5:
                num6.Add(pos * 10f);
                break;
            case 6:
                num7.Add(pos * 10f);
                break;
            case 7:
                num8.Add(pos * 10f);
                break;
            case 8:
                num9.Add(pos * 10f);
                break;
            case 9:
                num10.Add(pos * 10f);
                break;
        }
    }
}

public class TransformData
{
    public float HeadX { get; set; }
    public float HeadY { get; set; }
    public float HeadZ { get; set; }
    public float HeadRotX { get; set; }
    public float HeadRotY { get; set; }
    public float HeadRotZ { get; set; }
    public float HeadRotW { get; set; }
    public float RightX { get; set; }
    public float RightY { get; set; }
    public float RightZ { get; set; }
    public float RightRotX { get; set; }
    public float RightRotY { get; set; }
    public float RightRotZ { get; set; }
    public float RightRotW { get; set; }
    public float RightAngluarVelocityX { get; set; }
    public float RightAngluarVelocityY { get; set; }
    public float RightAngluarVelocityZ { get; set; }
    public float RightVelocityX { get; set; }
    public float RightVelocityY { get; set; }
    public float RightVelocityZ { get; set; }
    public float LeftX { get; set; }
    public float LeftY { get; set; }
    public float LeftZ { get; set; }
    public float LeftRotX { get; set; }
    public float LeftRotY { get; set; }
    public float LeftRotZ { get; set; }
    public float LeftRotW { get; set; }
    public float LeftAngluarVelocityX { get; set; }
    public float LeftAngluarVelocityY { get; set; }
    public float LeftAngluarVelocityZ { get; set; }
    public float LeftVelocityX { get; set; }
    public float LeftVelocityY { get; set; }
    public float LeftVelocityZ { get; set; }
    public bool Gesturing { get; set; }
    public bool GestureTime { get; set; }
    public int GestureNumber { get; set; }

    public TransformData() { }

    public TransformData(float hx, float hy, float hz, float hrx, float hry, float hrz, float hrw,
        float rx, float ry, float rz, float rrx, float rry, float rrz, float rrw, float ravx, float ravy, float ravz, float rvx, float rvy, float rvz,
        float lx, float ly, float lz, float llx, float lly, float llz, float llw, float lavx, float lavy, float lavz, float lvx, float lvy, float lvz,
                        bool gesture, bool gestureTime, int gestureNumber)
    {
        HeadX = hx;
        HeadY = hy;
        HeadZ = hz;
        HeadRotX = hrx;
        HeadRotY = hry;
        HeadRotZ = hrz;
        HeadRotW = hrw;
        RightX = rx;
        RightY = ry;
        RightZ = rz;
        RightRotX = rrx;
        RightRotY = rry;
        RightRotZ = rrz;
        RightRotW = rrw;
        RightAngluarVelocityX = ravx;
        RightAngluarVelocityY = ravy;
        RightAngluarVelocityZ = ravz;
        RightVelocityX = rvx;
        RightVelocityY = rvy;
        RightVelocityZ = rvz;
        LeftX = lx;
        LeftY = ly;
        LeftZ = lz;
        LeftRotX = llx;
        LeftRotY = lly;
        LeftRotZ = llz;
        LeftRotW = llw;
        LeftAngluarVelocityX = lavx;
        LeftAngluarVelocityY = lavy;
        LeftAngluarVelocityZ = lavz;
        LeftVelocityX = lvx;
        LeftVelocityY = lvy;
        LeftVelocityZ = lvz;
        Gesturing = gesture;
        GestureTime = gestureTime;
        GestureNumber = gestureNumber;
    }
}

public sealed class TransformDataMap : ClassMap<TransformData>
{
    public TransformDataMap()
    {
        Map(m => m.HeadX).Index(0);
        Map(m => m.HeadY).Index(1);
        Map(m => m.HeadZ).Index(2);
        Map(m => m.HeadRotX).Index(3);
        Map(m => m.HeadRotY).Index(4);
        Map(m => m.HeadRotZ).Index(5);
        Map(m => m.HeadRotW).Index(6);
        Map(m => m.RightX).Index(7);
        Map(m => m.RightY).Index(8);
        Map(m => m.RightZ).Index(9);
        Map(m => m.RightRotX).Index(10);
        Map(m => m.RightRotY).Index(11);
        Map(m => m.RightRotZ).Index(12);
        Map(m => m.RightRotW).Index(13);
        Map(m => m.RightAngluarVelocityX).Index(14);
        Map(m => m.RightAngluarVelocityY).Index(15);
        Map(m => m.RightAngluarVelocityZ).Index(16);
        Map(m => m.RightVelocityX).Index(17);
        Map(m => m.RightVelocityY).Index(18);
        Map(m => m.RightVelocityZ).Index(19);
        Map(m => m.LeftX).Index(20);
        Map(m => m.LeftY).Index(21);
        Map(m => m.LeftZ).Index(22);
        Map(m => m.LeftRotX).Index(23);
        Map(m => m.LeftRotY).Index(24);
        Map(m => m.LeftRotZ).Index(25);
        Map(m => m.LeftRotW).Index(26);
        Map(m => m.LeftAngluarVelocityX).Index(27);
        Map(m => m.LeftAngluarVelocityY).Index(28);
        Map(m => m.LeftAngluarVelocityZ).Index(29);
        Map(m => m.LeftVelocityX).Index(30);
        Map(m => m.LeftVelocityY).Index(31);
        Map(m => m.LeftVelocityZ).Index(32);
        Map(m => m.Gesturing).Index(33);
        Map(m => m.GestureTime).Index(34);
        Map(m => m.GestureNumber).Index(35);
    }
}