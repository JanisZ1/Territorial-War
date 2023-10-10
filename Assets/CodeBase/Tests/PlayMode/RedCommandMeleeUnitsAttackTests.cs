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
    }
}