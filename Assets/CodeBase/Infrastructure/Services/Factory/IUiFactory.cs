namespace Assets.CodeBase.Infrastructure.Services.Factory
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        void CreateChooseButtons();
        void CreateQueueButtons(CommandColor commandColor);
        void CreateUpgradeButtons(CommandColor commandColor);
    }
}
