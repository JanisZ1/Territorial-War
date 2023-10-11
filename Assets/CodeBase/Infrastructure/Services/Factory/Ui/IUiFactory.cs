namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        void CreateChooseCommandButtons();
        void CreateQueueButtons(CommandColor commandColor);
        void CreateUpgradeButtons(CommandColor commandColor);
    }
}
