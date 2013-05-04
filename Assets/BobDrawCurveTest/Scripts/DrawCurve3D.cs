using UnityEngine;
using System.Collections;
using Vectrosity;

public class DrawCurve3D : MonoBehaviour {

    public Vector3 offset;
    int Size = 11;
    public float SegmentLength = 10;
    public int Segments = 100;
    public float Width = 14;
    public int iteration = 1;
    public bool isWidthSame = false;
    float _lastDrawTimer = 0;
    VectorLine line;
    Vector3[] _srcPoints;
    Vector3[] _dstPoints;
    Vector3[] _interpPoints;
    Vector3[] _bufferPoints;
    float[] _lineWidths;
    public bool isVertical;
    public float threadhold = 1.0f;
    public Material linematerial;
    // Use this for initialization

    public void Drawline(Vector3[] points)
    {
        line.MakeSpline(points, Segments - 1);
        line.Draw3D();
        resetwidth();
    }
    void Start()
    {
        _bufferPoints = new Vector3[Segments];
        _srcPoints = new Vector3[Size];
        _dstPoints = new Vector3[Size];
        _interpPoints = new Vector3[Size];
        GeneratePoints(transform, ref _srcPoints);
        for (int i = 0; i < Size; i++ )
        {
            _dstPoints[i] = _srcPoints[i];
            _interpPoints[i] = _srcPoints[i];
        }
        line = new VectorLine("curve", _bufferPoints, Color.grey, linematerial, 2.0f, LineType.Continuous);
        line.smoothWidth = true;
        resetwidth();
    }


    public void resetwidth() {
        generatelinewidth();
        line.SetWidths(_lineWidths);
    }
    public void Rebuild() {
        VectorLine.Destroy(ref line);
        line = new VectorLine("curve", _bufferPoints, Color.grey, linematerial, 2.0f, LineType.Continuous);
        line.smoothWidth = true;
        resetwidth();
    }
   
    private void GeneratePoints(Transform akeyPoint, ref Vector3[] outputPoints)
    {
        Vector3 akeypointToScreen = (akeyPoint.position+offset);
        for (int i = 0; i < Size; i++)
        {
            if (isVertical)
            {
                outputPoints[i].x = akeypointToScreen.x;
                outputPoints[i].y = akeypointToScreen.y+(i - Size / 2) * SegmentLength;
                outputPoints[i].z = akeypointToScreen.z;
            }
            else
            {
                outputPoints[i].x = akeypointToScreen.x+(i - Size / 2) * SegmentLength;
                outputPoints[i].y = akeypointToScreen.y;
                outputPoints[i].z = akeypointToScreen.z;
            }
        }
    }

    void OnEnable() {
        if(null != line)
            line.active = true;
    }
    void OnDisable() {
        line.active = false;
    }
	// Update is called once per frame
    void LateUpdate()
    {
        GeneratePoints(transform,ref _dstPoints);
        UpdatePoints();
        for (int i = 0; i < Size; i++)
        {
            _srcPoints[i] = _interpPoints[i];
        }
        _lastDrawTimer = Time.time;
        Drawline(_interpPoints);
	}

    private void generatelinewidth()
    {
        _lineWidths = new float[Segments - 1];
        if (isWidthSame)
        {
            for (int i = 0; i < (Segments - 1); i++)
            {
                _lineWidths[i] = Width;
            }
        }
        else
        {
            for (int i = 0; i < (Segments - 1) / 2; i++)
            {
                _lineWidths[i] = (float)i * Width * 2 / (float)Segments;
                _lineWidths[Segments - 2 - i] = _lineWidths[i];
            }
            _lineWidths[(Segments - 1) / 2] = Width;

        }
    }
    private void UpdatePoints()
    {
        if (Vector3.Distance(_dstPoints[Size / 2], _srcPoints[Size / 2]) < threadhold)
        {
            for (int i = 0; i < Size; i++)
            {
                _interpPoints[i] = _dstPoints[i];
            }
        }
        else
        {
            for (int i = 0; i < Size; i++)
            {
                float approachFactor = 0;
                float finalfactor = 1;
                approachFactor = /*Mathf.Cos*/(Mathf.Abs((1.0f / (1.0f - (2.0f * (float)i / (float)Size))))*Mathf.PI/2);
                for (int j = 0; j < iteration; j++) {
                    finalfactor *= approachFactor;
                }
                _interpPoints[i] = Vector3.Lerp(_srcPoints[i], _dstPoints[i], Time.deltaTime * approachFactor);
            }
        }

        if (isVertical)
        {
            _interpPoints[0].x = _interpPoints[0].x;
            _interpPoints[0].y = _interpPoints[0].y + Random.Range(-SegmentLength, SegmentLength) / 10;
            _interpPoints[Size - 1].x = _interpPoints[Size - 1].x;
            _interpPoints[Size - 1].y = _interpPoints[Size - 1].y + Random.Range(-SegmentLength, SegmentLength) / 10;
        }
        else
        {
            _interpPoints[0].x = _interpPoints[0].x + Random.Range(-SegmentLength, SegmentLength) / 10;
            _interpPoints[0].y = _interpPoints[0].y;
            _interpPoints[Size - 1].x = _interpPoints[Size - 1].x + Random.Range(-SegmentLength, SegmentLength) / 10;
            _interpPoints[Size - 1].y = _interpPoints[Size - 1].y;
        }
    }
}
