using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ScanningLineFactory : IScanningLineFactory
    {
        private readonly IAssets _assets;

        public ScanningLineFactory(IAssets assets)
        {
            _assets = assets;
        }

        public ScanningLine CreateScanningLine(Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.ScanningLinePath, at, Quaternion.identity);

            ScanningLine scanningLine = gameObject.GetComponent<ScanningLine>();

            return scanningLine;
        }
    }
}

