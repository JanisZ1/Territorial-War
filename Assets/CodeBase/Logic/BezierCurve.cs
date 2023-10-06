using UnityEngine;

public class BezierCurve : MonoBehaviour {
    private Vector3[] _points;

    public void Reset() {
        _points = new Vector3[] {
            new Vector3(1f, 0f, 0f),
            new Vector3(2f, 0f, 0f),
            new Vector3(3f, 0f, 0f),
            new Vector3(4f, 0f, 0f)
        };
    }

    public Vector3 GetPoint(float t) =>
        transform.TransformPoint(Bezier.GetPoint(_points[0], _points[1], _points[2], _points[3], t));

    public Vector3 GetVelocity(float t) =>
        transform.TransformPoint(Bezier.GetFirstDerivative(_points[0], _points[1], _points[2], _points[3], t)) -
            transform.position;

    public Vector3 GetDirection(float t) =>
        GetVelocity(t).normalized;
}
