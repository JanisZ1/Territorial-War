using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.Logic.Ui
{
    public class ChooseCommandButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private CommandColor _commandColor;

        private IChooseCommandMediator _chooseCommandMediator;

        public void Construct(IChooseCommandMediator chooseCommandMediator) =>
            _chooseCommandMediator = chooseCommandMediator;

        private void Start() =>
            _button.onClick.AddListener(ChooseCommand);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(ChooseCommand);

        private void ChooseCommand() =>
            _chooseCommandMediator.ChooseCommand(_commandColor);
    }
}
