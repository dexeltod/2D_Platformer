﻿using SuperTiled2Unity.Scripts.Editor.Importers;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.Math
{
    // Create concrete collider types based on inheritence and geometry input
    public abstract class ColliderFactory
    {
        public ColliderFactory(SuperImportContext importContext)
        {
            ImportContext = importContext;
        }

        public SuperImportContext ImportContext { get; private set; }

        public Vector2 TransformPoint(float x, float y)
        {
            return TransformPoint(new Vector2(x, y));
        }

        public abstract Vector2 TransformPoint(Vector2 point);

        public abstract Collider2D MakeBox(GameObject go, float width, float height);
        public abstract Collider2D MakeEllipse(GameObject go, float width, float height);
        public abstract Collider2D MakePolygon(GameObject go, Vector2[] points);
        public abstract Collider2D MakePolyline(GameObject go, Vector2[] points);
    }
}
