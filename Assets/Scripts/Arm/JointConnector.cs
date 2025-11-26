using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class JointConnector : MonoBehaviour
{
    [SerializeField] Transform[] spline; 
    [SerializeField] int index;
    LineRenderer lineRenderer;

    SplineContainer container;

    private void Start()
    {
        container = GetComponentInParent<SplineContainer>();
        lineRenderer = GetComponent<LineRenderer>();
        InitializeSpline(spline);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpline(0, spline);

        UpdateLineRenderer(0, lineRenderer);
    }

    void InitializeSpline(Transform[] points)
    {
        Spline newSpline = new();

        foreach (Transform point in points)
        {
            BezierKnot knot = new()
            {
                Position = point.position
            };
            newSpline.Add(knot);
        }

        container.AddSpline(newSpline);
    }

    void UpdateSpline(int index, Transform[] points)
    {
        int knotIndex = 0;

        foreach (Transform point in points)
        {
            BezierKnot knot = new()
            {
                Position = point.position
            };
            container.Splines[index].SetKnot(knotIndex, knot);

            knotIndex++;
        }
    }

    void UpdateLineRenderer(int index, LineRenderer lineRenderer)
    {
        int degree = 50;
        Spline spline = container.Splines[index];
        lineRenderer.positionCount = 0;

        int posIndex = 0;
        int curve = 0;
        float increment = Mathf.Max(spline.GetCurveLength(curve) / degree, 0.000001f);
        for (float x = 0; x <= spline.GetCurveLength(curve); x += increment)
        {
            lineRenderer.positionCount++;
            float t = spline.GetCurveInterpolation(curve, x);
            Vector3 pos = spline.GetPointAtLinearDistance(t, x, out float time);

            // add result point to line renderer
            lineRenderer.SetPosition(posIndex, pos);
            posIndex++;
        }
    }
}
