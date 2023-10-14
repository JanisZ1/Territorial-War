using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

public interface IChooseCommandMediator : IService
{
    void SubscribeToChooseCommand(GameObject window);
}
