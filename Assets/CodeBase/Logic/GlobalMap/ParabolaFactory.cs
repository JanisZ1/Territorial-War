using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;
        private readonly IScanningLineFactory _scanningLineFactory;

        public ParabolaFactory(IAssets assets, IScanningLineFactory scanningLineFactory)
        {
            _assets = assets;
            _scanningLineFactory = scanningLineFactory;
        }

        public GameObject CreateParabola(Vector2 focusPosition) =>
            _assets.Instantiate(AssetPath.ParabolaPath);
    }
}

