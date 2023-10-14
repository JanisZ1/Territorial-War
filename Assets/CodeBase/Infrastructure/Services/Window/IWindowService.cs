using Assets.CodeBase.StaticData;

namespace Assets.CodeBase.Infrastructure.Services.Window
{
    public interface IWindowService : IService
    {
        void OpenWindow(WindowType windowType);
    }
}
