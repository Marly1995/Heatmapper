using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;

public class DataHandler : MonoBehaviour
{
    /// <summary>
    /// Frame of which transform to display.
    /// </summary>
    public int indexPositionStart;
    public int indexPositionEnd;

    // Csv Data
    public int participant;
    public string file;
    TextReader textReader;
    CsvReader csv;

    // participant data set
    List<TransformData> data;

    // heatmap data
    List<Vector3> heatmapPoints;
    public int pointRadius;

    // line chart data
    List<float> linePoints;
    public LineRenderer line;

    // positions for 3d representation
    public Transform head;
    public Transform rightHand;
    public Transform leftHand;

    Vector3[] headPositions;
    Vector3[] rightHandPositions;
    Vector3[] leftHandPositions;
    Quaternion[] headRotations;
    Quaternion[] rightHandRotations;
    Quaternion[] leftHandRotations;

    bool doneLoading = false;

    private void Start()
    {
        heatmapPoints = new List<Vector3>();
        linePoints = new List<float>();
        StartCoroutine(LoadDatasets());
    }

    private void Update()
    {
        UpdateVisualRepresentation();
        if (Input.GetKey(KeyCode.Keypad1))
        { BuildHeatmap(); }
        if (Input.GetKey(KeyCode.Keypad2))
        { BuildLineChart(); }
    }

    private void BuildHeatmap()
    {
        heatmapPoints.Clear();
        if (indexPositionStart < indexPositionEnd)
        {
            for (int i = indexPositionStart; i < indexPositionEnd; i++)
            {
                heatmapPoints.Add(rightHandPositions[i]);
            }
            Texture2D heatmapImage = Heatmap.CreateHeatmap(heatmapPoints.ToArray(), GetComponent<Camera>(), pointRadius);
            Heatmap.CreateRenderPlane(heatmapImage);
            Heatmap.projectionPlane.transform.position = new Vector3(-0.7f, 0.7f, 1f);
            Debug.Log("Map Built");
        }
    }

    private void BuildLineChart()
    {
        linePoints.Clear();
        if (indexPositionStart < indexPositionEnd)
        {
            for (int i = indexPositionStart; i < indexPositionEnd; i++)
            {
                linePoints.Add(rightHandPositions[i].magnitude);
            }
            float increment = 2f / (indexPositionEnd - indexPositionStart);
            line.positionCount = indexPositionEnd - indexPositionStart;
            for (int i = 0; i < line.positionCount; i++)
            {
                line.SetPosition(i, new Vector3((i * increment)-1f, linePoints[i]-1f, 0f));
            }
            Debug.Log("Line Built");
        }
    }

    private void UpdateVisualRepresentation()
    {
        if (doneLoading)
        {
            head.localPosition = headPositions[indexPositionStart];
            head.localRotation = headRotations[indexPositionStart];
            rightHand.localPosition = rightHandPositions[indexPositionStart];
            rightHand.localRotation = rightHandRotations[indexPositionStart];
            leftHand.localPosition = leftHandPositions[indexPositionStart];
            leftHand.localRotation = leftHandRotations[indexPositionStart];
        }
    }

    public IEnumerator LoadDatasets()
    {
        data = new List<TransformData>();
        string path = file + "\\" + participant.ToString() + "\\" + "localData";
        textReader = File.OpenText(path);
        csv = new CsvReader(textReader);
        csv.Configuration.RegisterClassMap<TransformDataMap>();
        IEnumerable<TransformData> enumerable = csv.GetRecords<TransformData>();
        data = enumerable.ToList();

        headPositions = new Vector3[data.Count];
        rightHandPositions = new Vector3[data.Count];
        leftHandPositions = new Vector3[data.Count];

        headRotations = new Quaternion[data.Count];
        rightHandRotations = new Quaternion[data.Count];
        leftHandRotations = new Quaternion[data.Count];

        Vector3 vec3 = new Vector3();
        Quaternion quat = new Quaternion();
        for (int i = 0; i < data.Count; i++)
        {
            // parse postiional data for 3d representation
            vec3.x = data[i].HeadX; vec3.y = data[i].HeadY; vec3.z = data[i].HeadZ;
            quat.x = data[i].HeadRotX; quat.y = data[i].HeadRotY; quat.z = data[i].HeadRotZ; quat.w = data[i].HeadRotW;
            headPositions[i] = vec3;
            headRotations[i] = quat;

            vec3.x = data[i].RightX; vec3.y = data[i].RightY; vec3.z = data[i].RightZ;
            quat.x = data[i].RightRotX; quat.y = data[i].RightRotY; quat.z = data[i].RightRotZ; quat.w = data[i].RightRotW;
            rightHandPositions[i] = vec3;
            rightHandRotations[i] = quat;

            vec3.x = data[i].LeftX; vec3.y = data[i].LeftY; vec3.z = data[i].LeftZ;
            quat.x = data[i].LeftRotX; quat.y = data[i].LeftRotY; quat.z = data[i].LeftRotZ; quat.w = data[i].LeftRotW;
            leftHandPositions[i] = vec3;
            leftHandRotations[i] = quat;
        }
        Debug.Log("Done Loading");
        doneLoading = true;
        yield return 0;
    }
}