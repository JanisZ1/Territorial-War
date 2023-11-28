﻿using Assets.CodeBase.Infrastructure.Services.AssetProvider;
using UnityEngine;

namespace Assets.CodeBase.Logic.GlobalMap
{
    public class ParabolaFactory : IParabolaFactory
    {
        private readonly IAssets _assets;

        public ParabolaFactory(IAssets assets) =>
            _assets = assets;

        public Parabola CreateParabola(Vector2 focusPointPosition)
        {
            Parabola parabola = _assets.Instantiate(AssetPath.ParabolaPath).GetComponent<Parabola>();

            parabola.Construct(focusPointPosition);

            return parabola;
        }

        public Parabola CreateParabola(Vector2 focusPointPosition, Vector2 nextParabolaFocusPointPosition)
        {
            Parabola parabola = _assets.Instantiate(AssetPath.ParabolaPath).GetComponent<Parabola>();

            parabola.Construct(focusPointPosition, nextParabolaFocusPointPosition);

            return parabola;
        }
    }
}

