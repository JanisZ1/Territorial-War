using UnityEngine;

namespace Assets.CodeBase.Logic.GreenCommand
{
    public class GreenCommandAnimatorStateReporter : StateMachineBehaviour
    {
        private IGreenCommandAnimationStateReader _animationStateReader;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            FindReader(animator);

            _animationStateReader.StateEntered(stateInfo.shortNameHash);
        }

        private void FindReader(Animator animator)
        {
            if (_animationStateReader != null)
                return;

            _animationStateReader = animator.GetComponent<IGreenCommandAnimationStateReader>();
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            FindReader(animator);

            _animationStateReader.StateExited(stateInfo.shortNameHash);
        }
    }
}