using Assets.CodeBase.Logic.RedCommand;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.Services.RedCommandUnitsHandler
{
    public class RedCommandUnitsHandler : IRedCommandUnitsHandler
    {
        public List<RedCommandUnit> RedCommandUnits { get; } = new List<RedCommandUnit>();
    }
}
