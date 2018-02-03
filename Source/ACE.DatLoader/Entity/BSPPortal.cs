using System;
using System.Collections.Generic;
using System.IO;

using ACE.Entity.Enum;

namespace ACE.DatLoader.Entity
{
    public class BSPPortal : BSPNode
    {
        public List<PortalPoly> InPortals { get; } = new List<PortalPoly>();

        /// <summary>
        /// You must use the Unpack(BinaryReader reader, BSPType treeType) method.
        /// </summary>
        /// <exception cref="NotSupportedException">You must use the Unpack(BinaryReader reader, BSPType treeType) method.</exception>
        public override void Unpack(BinaryReader reader)
        {
            throw new NotSupportedException();
        }

        public override void Unpack(BinaryReader reader, BSPType treeType)
        {
            Type = reader.ReadUInt32();

            SplittingPlane = new Plane();
            SplittingPlane.Unpack(reader);

            PosNode = BSPNode.ReadNode(reader, treeType);
            NegNode = BSPNode.ReadNode(reader, treeType);

            if (treeType == BSPType.Drawing)
            {
                Sphere = new Sphere();
                Sphere.Unpack(reader);

                var numPolys = reader.ReadUInt32();
                var numPortals = reader.ReadInt32();

                InPolys = new List<ushort>();
                for (uint i = 0; i < numPolys; i++)
                    InPolys.Add(reader.ReadUInt16());

                InPortals.Unpack(reader, numPortals);
            }
        }
    }
}
