using Assets.CodeBase.StaticData;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.Factory.Ui
{
    public interface IUiFactory : IService
    {
        void CreateUiRoot();
        void CreateHumanControlledUi(WindowType windowType);
        GameObject CreateWindow(WindowType windowType);
    }
}
