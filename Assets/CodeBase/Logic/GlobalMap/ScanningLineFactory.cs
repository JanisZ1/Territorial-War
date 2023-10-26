using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLineFactory : IScanningLineFactory
    {
        private readonly IAssets _assets;
        private readonly IParabolaObjectPool _parabolaObjectPool;

        public ScanningLineFactory(IAssets assets, IParabolaObjectPool parabolaObjectPool)
        {
            _assets = assets;
            _parabolaObjectPool = parabolaObjectPool;
        }

        public GameObject CreateScanningLine()
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.ScanningLinePath);

            ScanningLine scanningLine = gameObject.GetComponent<ScanningLine>();
            scanningLine.Construct(_parabolaObjectPool);

            return gameObject;
        }
    }
}

