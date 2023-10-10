using System.Collections;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.CodeBase.Tests.PlayMode
{
    public class GreenCommandMeleeUnitsMoveTests
    {
        [UnityTest]
        public IEnumerator WhenWaitedFor0point1Seconds_AndPreviousUnitIsNull_ThenUnitShouldMoveRight()
        {
            // Arrange.
            Transform greenCommandUnit = Setup.GreenCommandUnit();

            float initialPosition = Setup.InitialPosition(greenCommandUnit);

            // Act.
            yield return new WaitForSeconds(0.1f);

            // Assert.
            Assert.Greater(greenCommandUnit.position.x, initialPosition);

            Object.Destroy(greenCommandUnit.gameObject);
        }

        [UnityTest]
        public IEnumerator WhenSettedEnemyPositionToGreenCommandUnit_AndEnemyInFront_ThenUnitShouldStopMoving()
        {
            // Arrange.
            RedCommandUnit redCommandUnit = Create.RedCommandMeleeUnitMove();
            Transform greenCommandUnit = Setup.GreenCommandUnit(redCommandUnit);
            GreenCommandMeleeUnitMove greenCommandMeleeUnitMove = greenCommandUnit.GetComponent<GreenCommandMeleeUnitMove>();

            // Act.
            redCommandUnit.transform.position = greenCommandUnit.position;
            yield return null;

            // Assert.
            Assert.IsFalse(greenCommandMeleeUnitMove.MovingEnabled);

            Object.Destroy(greenCommandUnit.gameObject);
            Object.Destroy(redCommandUnit.gameObject);
        }
    }
}