namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public interface IHumanControlUiFactory : IService
    {
        void CreateHumanControlledTools(CommandColor commandColor);
    }
}
