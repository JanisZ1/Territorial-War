using System.Collections;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.CodeBase.Tests.PlayMode
{
    public class RedCommandMeleeUnitsAttackTests
    {
        private Vector3 _greenCommandUnitMoveOffset = -Vector3.right * 2;
        private const float AttackAnimationTime = 2;

        [UnityTest]
        public IEnumerator WhenWaitedFor0point1Seconds_AndSettedEnemyPositionToRedCommandUnit_ThenAttackShouldBeEnabled()
        {
            // Arrange.
            GreenCommandUnit greenCommandunit = Setup.GreenCommandMeleeUnit();
            RedCommandUnit redCommandUnit = Setup.RedCommandMeleeUnit(greenCommandunit);
            RedComandMeleeUnitAttack meleeUnitAttack = redCommandUnit.GetComponent<RedComandMeleeUnitAttack>();

            // Act.
            greenCommandunit.transform.position = redCommandUnit.transform.position;

            yield return new WaitForSeconds(0.1f);

            // Assert.
            Assert.IsTrue(meleeUnitAttack.AttackEnabled);

            Object.Destroy(greenCommandunit.gameObject);
            Object.Destroy(redCommandUnit.gameObject);
        }

        [UnityTest]
        public IEnumerator WhenWaitedForAttack_AndSettedEnemyPositionToInFrontOfGreenUnit_ThenAttackėdUnitShouldBeDamaged()
        {
            // Arrange.
            GreenCommandUnit greenCommandUnit = Setup.GreenCommandMeleeUnit();
            GreenCommandUnitHealth greenCommandUnitHealth = greenCommandUnit.GetComponentInChildren<GreenCommandUnitHealth>();
            int initialHealth = greenCommandUnitHealth.UnitHealth;

            RedCommandMeleeUnitMove greenCommandunit = Setup.RedCommandMeleeUnit(greenCommandUnit);
            RedComandMeleeUnitAttack meleeUnitAttack = greenCommandunit.GetComponent<RedComandMeleeUnitAttack>();
            // Act.
            greenCommandUnit.transform.position = greenCommandunit.transform.position + _greenCommandUnitMoveOffset;

            yield return new WaitForSeconds(meleeUnitAttack.AttackCooldown + AttackAnimationTime);

            // Assert.
            Assert.Less(greenCommandUnitHealth.UnitHealth, initialHealth);

            Object.Destroy(greenCommandunit.gameObject);
            Object.Destroy(greenCommandUnit.gameObject);
        }
    }
}