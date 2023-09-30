using Assets.CodeBase.Logic.RedCommand;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandBullet : MonoBehaviour
    {
        [SerializeField] private Transform p0;
        [SerializeField] private Transform p1;
        [SerializeField] private Transform p2;
        [Range(0, 1)]
        [SerializeField] private float t;
        private RedCommandUnitMove _redCommandUnit;
        private List<Transform> _bezierPointsToDelete = new List<Transform>();
        private RedCommandUnitHealth _redCommandUnitHealth;

        private void Awake()
        {
            transform.DetachChildren();
            _redCommandUnitHealth = FindObjectOfType<RedCommandUnitHealth>();
            _redCommandUnit = FindObjectOfType<RedCommandUnitMove>();
            _bezierPointsToDelete.Add(transform);
            _bezierPointsToDelete.Add(p0);
            _bezierPointsToDelete.Add(p1);
            _bezierPointsToDelete.Add(p2);
            p0 = Instantiate(p0, transform.position, Quaternion.identity);
            _bezierPointsToDelete.Add(p0);
            p1 = Instantiate(p1, transform.position + new Vector3(0.75f, 1f, 0), Quaternion.identity);
            _bezierPointsToDelete.Add(p1);
            p2 = Instantiate(p2, _redCommandUnit.transform.position, Quaternion.identity);
            _bezierPointsToDelete.Add(p2);
            p2.position = _redCommandUnit.transform.position;
        }

        private void Update()
        {
            if (_redCommandUnit)
            {
                p2.position = _redCommandUnit.transform.position;
                t += Time.deltaTime;
            }
            else
            {
                //���� ���������� ����� ����� ���� ����� ����� � ���� ��� �����
            }
            transform.position = GetPoint(p0.position, p1.position, p2.position, t);
        }

        public Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            Vector3 p01 = Vector3.Lerp(p0, p1, t);
            Vector3 p12 = Vector3.Lerp(p1, p2, t);
            Vector3 p0112 = Vector3.Lerp(p01, p12, t);
            return p0112;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<RedCommandUnitHealth>())
            {
                DestroyBezierPoints();
                _redCommandUnitHealth.TakeDamage(1);
            }
        }

        private void DestroyBezierPoints()
        {
            for (int i = 0; i < _bezierPointsToDelete.Count; i++)
            {
                Destroy(_bezierPointsToDelete[i].gameObject);
            }
            _bezierPointsToDelete.Clear();
        }
    }
}