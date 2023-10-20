﻿using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public interface IScanningLineFactory : IService
    {
        ScanningLine ScanningLine { get; }

        GameObject CreateScanningLine();
    }
}

