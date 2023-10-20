using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLineFactory : IScanningLineFactory
    {
        private readonly IAssets _assets;

        public ScanningLine ScanningLine { get; private set; }

        public ScanningLineFactory(IAssets assets) =>
            _assets = assets;

        public GameObject CreateScanningLine()
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.ParabolaPath);
            ScanningLine = gameObject.GetComponent<ScanningLine>();

            return gameObject;
        }
    }
}

