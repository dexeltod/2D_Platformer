using System.Xml.Linq;
using UnityEngine;

namespace SuperTiled2Unity.Scripts.Editor.Loaders
{
    public class SuperTileLayerLoader : SuperLayerLoader
    {
        public SuperTileLayerLoader(XElement xml)
            : base(xml)
        {
        }

        protected override SuperLayer CreateLayerComponent(GameObject go)
        {
            return go.AddComponent<SuperTileLayer>();
        }

        protected override void InternalLoadFromXml(GameObject go)
        {
            // No extra data to load from the xml
        }
    }
}
