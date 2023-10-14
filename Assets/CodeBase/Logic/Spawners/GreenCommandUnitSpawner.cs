using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public class GreenCommandUnitSpawner : UnitSpawner
    {
        private GreenCommandUnit _previousUnit;
        private IUnitFactory _unitFactory;
        private IRedCommandUnitsHandler _redCommandUnitsHandler;
        private IGreenCommandUnitsHandler _greenCommandUnitsHandler;

        public override void Construct(IUnitFactory warriorFactory, IRedCommandUnitsHandler redCommandUnitsHandler, IGreenCommandUnitsHandler greenCommandUnitsHandler)
        {
            _unitFactory = warriorFactory;
            _redCommandUnitsHandler = redCommandUnitsHandler;
            _greenCommandUnitsHandler = greenCommandUnitsHandler;
        }

        public override void Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = _unitFactory.CreateUnit(unitType, position, rotation);
            GreenCommandUnit unit = gameObject.GetComponent<GreenCommandUnit>();

            unit.PreviousUnit = _previousUnit;
            _previousUnit = unit;
            unit.GetComponentInChildren<GreenCommandUnitDeath>().OnUnitDied += RemoveFromGreenCommandHandler;
            unit.GetComponent<ClosestEnemyUnitCalculatorForGreenCommand>().Construct(_redCommandUnitsHandler);
            _greenCommandUnitsHandler.GreenCommandUnits.Add(unit);
        }

        private void RemoveFromGreenCommandHandler(GreenCommandUnitDeath greenCommandUnitDeath) =>
            _greenCommandUnitsHandler.GreenCommandUnits.Remove(greenCommandUnitDeath.GetComponentInParent<GreenCommandUnit>());
    }
}