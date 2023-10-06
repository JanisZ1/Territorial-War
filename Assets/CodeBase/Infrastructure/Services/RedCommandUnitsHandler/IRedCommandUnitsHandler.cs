using Assets.CodeBase.Logic.RedCommand;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler
{
    public interface IRedCommandUnitsHandler : IService
    {
        List<RedCommandUnit> RedCommandUnits { get; }
    }
}
