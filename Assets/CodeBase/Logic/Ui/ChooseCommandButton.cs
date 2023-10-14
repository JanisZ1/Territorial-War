using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.CodeBase.Logic.Ui
{
    public class ChooseCommandButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _window;
        [SerializeField] private CommandColor _commandColor;

        public event Action<CommandColor> CommandColorChoosed;

        private void Start() =>
            _button.onClick.AddListener(ChooseCommand);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(ChooseCommand);

        private void ChooseCommand()
        {
            CommandColorChoosed?.Invoke(_commandColor);
            Destroy(_window);
        }
    }
}
