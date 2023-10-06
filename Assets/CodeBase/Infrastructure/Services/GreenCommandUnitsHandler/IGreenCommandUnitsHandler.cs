using Assets.CodeBase.Logic.GreenCommand;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler
{
    public interface IGreenCommandUnitsHandler : IService
    {
        List<GreenCommandUnit> GreenCommandUnits { get; }
    }
}
