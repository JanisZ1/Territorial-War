using System.Collections.Generic;
using UnityEngine;

public class GreenCommandUnitMove : MonoBehaviour
{
    private float _xVector = 1f;
    public Vector3 _playerUnitVector;

    [SerializeField] private Animator _animator;
    [SerializeField] private TriggerObserver _triggerObserver;
    [SerializeField] private MeleeAttack _meleeAttack;

    private IGreenCommandSpawner _greenCommandSpawner;
    private GreenCommandUnitMove _otherUnit;
    private List<GreenCommandUnitMove> _greenCommandUnits = new List<GreenCommandUnitMove>();

    public int Id { get; set; }

    public void Construct(IGreenCommandSpawner greenCommandSpawner) =>
        _greenCommandSpawner = greenCommandSpawner;

    private void Start()
    {
        Debug.Log("Start");

        _triggerObserver.TriggerEnter += TriggerEnter;
        _triggerObserver.TriggerExit += TriggerExit;
    }

    private void OnDestroy()
    {
        _triggerObserver.TriggerEnter -= TriggerEnter;
        _triggerObserver.TriggerExit -= TriggerExit;
    }

    private void TriggerEnter(Collider obj)
    {
        _otherUnit = obj.GetComponentInParent<GreenCommandUnitMove>();
        //If instantiating right into trigger then other unit is null
        if (!_otherUnit)
        {
            GetComponentInChildren<SphereCollider>().enabled = false;
            GetComponentInChildren<SphereCollider>().enabled = true;
        }

        if (_otherUnit)
        {
            if (!_greenCommandUnits.Contains(_otherUnit))
            {
                _greenCommandUnits.Add(_otherUnit);
                _greenCommandUnits.Add(this);
            }
        }
    }

    private void TriggerExit(Collider obj)
    {
        _otherUnit = obj.GetComponentInParent<GreenCommandUnitMove>();

        if (_otherUnit)
        {
            if (_greenCommandUnits.Contains(_otherUnit))
            {
                //If Id is the lessest then dont remove from list crutch.
                if (FindLesserIdUnit() != Id)
                {
                    _greenCommandUnits.Remove(_otherUnit);
                    _greenCommandUnits.Remove(this);
                }
            }
        }
    }
    //This kinda works but with some bugs and crutches
    private void Update()
    {
        if (_otherUnit)
        {
            if (Vector3.Distance(transform.position, _otherUnit.transform.position) > 1.2f && !_meleeAttack.IsFighting)
            {
                Move();
            }
            else if (FindLesserIdUnit() == Id)
            {
                Move();
            }
        }
        else
        {
            Move();
        }
    }

    private int FindLesserIdUnit()
    {
        int minumumIdClipped = int.MaxValue;

        for (int i = 0; i < _greenCommandUnits.Count; i++)
        {
            if (_greenCommandUnits[i].Id < minumumIdClipped)
            {
                _otherUnit = _greenCommandUnits[i];
                minumumIdClipped = _greenCommandUnits[i].Id;
            }
        }

        return minumumIdClipped;
    }

    public void Move()
    {
        Vector3 movingVector = new Vector3(_xVector * Time.deltaTime, 0f, 0f);
        transform.Translate(movingVector);
    }
}
