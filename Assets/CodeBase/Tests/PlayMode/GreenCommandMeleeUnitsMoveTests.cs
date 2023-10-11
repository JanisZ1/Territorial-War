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
            GreenCommandMeleeUnitMove greenCommandUnit = Setup.GreenCommandMeleeUnit();

            float initialPosition = greenCommandUnit.transform.position.x;

            // Act.
            yield return new WaitForSeconds(0.1f);

            // Assert.
            Assert.Greater(greenCommandUnit.transform.position.x, initialPosition);

            Object.Destroy(greenCommandUnit.gameObject);
        }

        [UnityTest]
        public IEnumerator WhenSettedEnemyPositionToGreenCommandUnit_AndEnemyInFront_ThenUnitShouldStopMoving()
        {
            // Arrange.
            RedCommandUnit redCommandUnit = Setup.RedCommandMeleeUnit();
            GreenCommandMeleeUnitMove greenCommandUnit = Setup.GreenCommandMeleeUnit(redCommandUnit);

            // Act.
            redCommandUnit.transform.position = greenCommandUnit.transform.position;
            yield return null;

            // Assert.
            Assert.IsFalse(greenCommandUnit.MovingEnabled);

            Object.Destroy(greenCommandUnit.gameObject);
            Object.Destroy(redCommandUnit.gameObject);
        }
    }
}