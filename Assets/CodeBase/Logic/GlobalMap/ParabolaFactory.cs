using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;

        public ParabolaFactory(IAssets assets)
        {
            _assets = assets;
        }
        public GameObject CreateParabola(Vector2 focusPosition)
        {
            GameObject gameObject = _assets.Instantiate(AssetPath.ParabolaPath);

            gameObject.GetComponent<Parabola>().Focus = focusPosition;

            return gameObject;
        }
    }
}

