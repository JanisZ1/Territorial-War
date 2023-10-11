using System.Collections;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.CodeBase.Tests.PlayMode
{
    public class RedCommandMeleeUnitsMoveTests
    {
        [UnityTest]
        public IEnumerator WhenWaitedFor0point1Seconds_AndPreviousUnitIsNull_ThenUnitShouldMoveLeft()
        {
            // Arrange.
            RedCommandMeleeUnitMove redCommandUnit = Setup.RedCommandMeleeUnit();

            float initialPosition = redCommandUnit.transform.position.x;

            // Act.
            yield return new WaitForSeconds(0.1f);

            // Assert.
            Assert.Less(redCommandUnit.transform.position.x, initialPosition);

            Object.Destroy(redCommandUnit.gameObject);
        }

        [UnityTest]
        public IEnumerator WhenSettedEnemyPositionToRedCommandUnit_AndEnemyInFront_ThenUnitShouldStopMoving()
        {
            // Arrange.
            GreenCommandUnit greenCommandUnit = Setup.GreenCommandMeleeUnit();
            RedCommandMeleeUnitMove redCommandUnit = Setup.RedCommandMeleeUnit(greenCommandUnit);

            // Act.
            greenCommandUnit.transform.position = redCommandUnit.transform.position;
            yield return null;

            // Assert.
            Assert.IsFalse(redCommandUnit.MovingEnabled);

            Object.Destroy(greenCommandUnit.gameObject);
            Object.Destroy(redCommandUnit.gameObject);
        }
    }
}