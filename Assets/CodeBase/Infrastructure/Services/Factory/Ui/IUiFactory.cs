using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        GameObject CreateChooseCommandButtons();
        void CreateQueueButtons(CommandColor commandColor);
        void CreateUpgradeButtons(CommandColor commandColor);
    }
}
