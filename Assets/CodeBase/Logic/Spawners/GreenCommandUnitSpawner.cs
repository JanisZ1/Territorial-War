using Assets.CodeBase.Infrastructure.Services.Factory;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public class GreenCommandUnitSpawner : MonoBehaviour
    {
        private GreenCommandUnit _previousUnit;
        private IUnitFactory _greenCommandUnitFactory;
        private IRedCommandUnitsHandler _redCommandUnitsHandler;

        public void Construct(IUnitFactory warriorFactory, IRedCommandUnitsHandler redCommandUnitsHandler)
        {
            _greenCommandUnitFactory = warriorFactory;
            _redCommandUnitsHandler = redCommandUnitsHandler;
        }

        public void Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = _greenCommandUnitFactory.CreateUnit(unitType, position, rotation);
            GreenCommandUnit unit = gameObject.GetComponent<GreenCommandUnit>();

            unit.PreviousUnit = _previousUnit;
            _previousUnit = unit;
            unit.GetComponent<GreenCommandEnemyUnitInFrontCalculator>().Construct(_redCommandUnitsHandler);
        }
    }
}