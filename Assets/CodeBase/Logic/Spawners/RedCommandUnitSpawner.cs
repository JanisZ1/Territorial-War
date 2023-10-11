﻿using Assets.CodeBase.Infrastructure.Services.Factory.Unit;
using Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler;
using Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler;
using Assets.CodeBase.Logic.RedCommand;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Logic.Spawners
{
    public class RedCommandUnitSpawner : MonoBehaviour
    {
        private RedCommandUnit _previousUnit;
        private IUnitFactory _greenCommandUnitFactory;
        private IRedCommandUnitsHandler _redCommandUnitsHandler;
        private IGreenCommandUnitsHandler _greenCommandUnitsHandler;

        public void Construct(IUnitFactory warriorFactory, IRedCommandUnitsHandler redCommandUnitsHandler, IGreenCommandUnitsHandler greenCommandUnitsHandler)
        {
            _greenCommandUnitFactory = warriorFactory;
            _redCommandUnitsHandler = redCommandUnitsHandler;
            _greenCommandUnitsHandler = greenCommandUnitsHandler;
        }

        public void Spawn(UnitType unitType, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = _greenCommandUnitFactory.CreateUnit(unitType, position, rotation);
            RedCommandUnit unit = gameObject.GetComponent<RedCommandUnit>();

            unit.PreviousUnit = _previousUnit;
            _previousUnit = unit;
            unit.GetComponent<ClosestEnemyUnitCalculatorForRedCommand>().Construct(_greenCommandUnitsHandler);
            unit.GetComponentInChildren<RedCommandUnitDeath>().OnUnitDied += RemoveFromRedCommandHandler;
            _redCommandUnitsHandler.RedCommandUnits.Add(unit);
        }

        private void RemoveFromRedCommandHandler(RedCommandUnitDeath redCommandUnitDeath) =>
            _redCommandUnitsHandler.RedCommandUnits.Remove(redCommandUnitDeath.GetComponent<RedCommandUnit>());
    }
}