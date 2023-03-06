using SuperTiled2Unity.Scripts.Editor.Importers;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.Extensions
{
    public static class SuperMapExtensions
    {
        public static Vector2 CellPositionToLocalPosition(this SuperMap superMap, int cx, int cy, SuperImportContext context)
        {
            var grid = superMap.GetComponentInChildren<Grid>();
            var local = grid.CellToLocal(new Vector3Int(cx, cy, 0));
            return local;
        }
    }
}
