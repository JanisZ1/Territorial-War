using Assets.CodeBase.Logic.GreenCommand;
using System.Collections.Generic;

namespace Assets.CodeBase.Infrastructure.Services.GreenCommandUnitsHandler
{
    class GreenCommandUnitsHandler : IGreenCommandUnitsHandler
    {
        public List<GreenCommandUnit> GreenCommandUnits { get; } = new List<GreenCommandUnit>();
    }
}
