﻿using Assets.CodeBase.Infrastructure.Services;
using System.Collections.Generic;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IParabolaFactory : IService
    {
        void CreateAndStoreParabolas(int sitesCount);
    }
}

