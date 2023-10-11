namespace Assets.CodeBase.Infrastructure.Services.Factory.HumanControlTools
{
    public interface IHumanControlToolsFactory : IService
    {
        void CreateHumanControlledTools(CommandColor commandColor);
    }
}
