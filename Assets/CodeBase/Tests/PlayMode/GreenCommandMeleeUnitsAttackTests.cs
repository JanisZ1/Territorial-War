using System.Collections;
using Assets.CodeBase.Logic.GreenCommand;
using Assets.CodeBase.Logic.RedCommand;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.CodeBase.Tests.PlayMode
{
    public class GreenCommandMeleeUnitsAttackTests
    {
        private Vector3 _redCommandUnitMoveOffset = Vector3.right * 2;
        private const float AttackAnimationTime = 2;

        [UnityTest]
        public IEnumerator WhenWaitedFor0point1Seconds_AndSettedEnemyPositionToGreenCommandUnit_ThenAttackShouldBeEnabled()
        {
            // Arrange.
            RedCommandUnit redCommandUnit = Setup.RedCommandMeleeUnit();
            GreenCommandMeleeUnitMove greenCommandunit = Setup.GreenCommandMeleeUnit(redCommandUnit);
            GreenCommandMeleeUnitAttack meleeUnitAttack = greenCommandunit.GetComponent<GreenCommandMeleeUnitAttack>();

            // Act.
            redCommandUnit.transform.position = greenCommandunit.transform.position;

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
            RedCommandUnit redCommandUnit = Setup.RedCommandMeleeUnit();
            RedCommandUnitHealth redCommandUnitHealth = redCommandUnit.GetComponentInChildren<RedCommandUnitHealth>();
            int initialHealth = redCommandUnitHealth.UnitHealth;

            GreenCommandMeleeUnitMove greenCommandunit = Setup.GreenCommandMeleeUnit(redCommandUnit);
            GreenCommandMeleeUnitAttack meleeUnitAttack = greenCommandunit.GetComponent<GreenCommandMeleeUnitAttack>();
            // Act.
            redCommandUnit.transform.position = greenCommandunit.transform.position + _redCommandUnitMoveOffset;

            yield return new WaitForSeconds(meleeUnitAttack.AttackCooldown + AttackAnimationTime);

            // Assert.
            Assert.Less(redCommandUnitHealth.UnitHealth, initialHealth);

            Object.Destroy(greenCommandunit.gameObject);
            Object.Destroy(redCommandUnit.gameObject);
        }
    }
}