using Assets.CodeBase.Infrastructure.Services.Factory.Ui;
using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUiFactory _uiFactory;
        private readonly IChooseCommandMediator _chooseCommandMediator;

        public WindowService(IUiFactory uiFactory, IChooseCommandMediator chooseCommandMediator)
        {
            _uiFactory = uiFactory;
            _chooseCommandMediator = chooseCommandMediator;
        }

        public void OpenWindow(WindowType windowType)
        {
            GameObject window = _uiFactory.CreateWindow(windowType);
            switch (windowType)
            {
                case WindowType.ChooseCommand:
                    _chooseCommandMediator.SubscribeToChooseCommand(window);
                    break;
            }
        }
    }
}
