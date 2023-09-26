using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private Transform p0;
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;
    [Range(0, 1)]
    [SerializeField] private float t;
    private PlayerUnit _playerUnit;
    private List<Transform> _objectsToDelete = new List<Transform>();

    private void Awake()
    {
        transform.DetachChildren();
        _playerUnit = FindObjectOfType<PlayerUnit>();
        _objectsToDelete.Add(transform);
        _objectsToDelete.Add(p0);
        _objectsToDelete.Add(p1);
        _objectsToDelete.Add(p2);
        _objectsToDelete.Add(Instantiate(p0, transform.position, Quaternion.identity));
        _objectsToDelete.Add(Instantiate(p1, transform.position + new Vector3(-0.75f, 1f, 0), Quaternion.identity));
        _objectsToDelete.Add(Instantiate(p2, _playerUnit.transform.position, Quaternion.identity));
        p2.position = _playerUnit.transform.position;
    }

    private void Update()
    {
        if (_playerUnit)
        {
            p2.position = _playerUnit.transform.position;
            t += Time.deltaTime;
        }
        transform.position = GetPoint(p0.position, p1.position, p2.position, t);
    }

    public void Draw(Vector3 point, float length)
    {

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
        if (other.gameObject.GetComponent<PlayerUnit>())
        {
            for (int i = 0; i < _objectsToDelete.Count; i++)
            {
                Destroy(_objectsToDelete[i].gameObject);
            }
            _objectsToDelete.Clear();
        }
    }
}
