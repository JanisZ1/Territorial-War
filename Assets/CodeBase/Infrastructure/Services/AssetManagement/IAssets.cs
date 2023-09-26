﻿using UnityEngine;

namespace Assets.CodeBase.Infrastructure.Services.AssetProvider
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string assetPath, Vector3 at, Quaternion rotation);
    }
}
