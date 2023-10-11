using Assets.CodeBase.Infrastructure.Services;

public interface IChooseCommandMediator : IService
{
    void ChooseCommand(CommandColor commandColor);
}
