using System.Collections;
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
    }
}