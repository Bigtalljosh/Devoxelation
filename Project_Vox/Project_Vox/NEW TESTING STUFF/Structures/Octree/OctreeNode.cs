//using System;
//using OpenTK;
//using System.Collections.Generic;

//namespace Devoxelation
//{
//    public class OctreeNode
//    {
//        public OctreeNode[] subNodes = null;
//        public OctreeNode parent;
//        public bool hasSubNodes
//        {
//            get
//            {
//                return subNodes != null;
//            }
//        }
//        public bool hasParent
//        {
//            get
//            {
//                return parent != null;
//            }
//        }
//        public bool cantCombine = false;

//        //Needed for reasons
//        public byte blockSize = 0;
//        public int level = 0;
//        public Vector3 origin;

//        //What we store in this node
//        public BlockData blockData;

//        public OctreeNode(byte blockSize, int blockID, int level, Vector3 origin)
//        {
//            this.blockSize = blockSize;
//            this.blockData = new BlockData(blockID);
//            this.origin = origin;
//            this.subNodes = null;
//            this.level = level;
//        }

//        public OctreeNode(byte blockSize, int blockID, int level, Vector3 origin, OctreeNode parent)
//        {
//            this.blockSize = blockSize;
//            this.blockData = new BlockData(blockID);
//            this.origin = origin;
//            this.subNodes = null;
//            this.level = level;
//            this.parent = parent;
//        }

//        public OctreeNode(byte blockSize, BlockData data, int level, Vector3 origin)
//        {
//            this.blockSize = blockSize;
//            this.blockData = data;
//            this.origin = origin;
//            this.subNodes = null;
//            this.level = level;
//        }

//        public OctreeNode(byte blockSize, BlockData data, int level, Vector3 origin, OctreeNode parent)
//        {
//            this.blockSize = blockSize;
//            this.blockData = data;
//            this.origin = origin;
//            this.subNodes = null;
//            this.level = level;
//            this.parent = parent;
//        }

//        public void Split()
//        {
//            subNodes = new OctreeNode[8];
//            float addDegSize = this.blockSize / 4f;
//            for (int i = 0; i < subNodes.Length; i++)
//            {
//                bool x = (i & 1) != 0, y = (i & 2) != 0, z = (i & 4) != 0;

//                subNodes[i] = new OctreeNode((byte)(this.blockSize / 2), this.blockData.blockID, level - 1, origin + new OpenTK.Vector3(x ? addDegSize : -addDegSize,
//                                                                                                                    y ? addDegSize : -addDegSize,
//                                                                                                                    z ? addDegSize : -addDegSize), this);
//            }
//        }

//        public void Combine()
//        {
//            //if(hasSubNodes)
//            //{
//            //	for(int i = 0; i < subNodes.Length; i++)
//            //	{
//            //		subNodes[i].Combine();
//            //	}
//            //	if(hasParent)
//            //	{
//            //		List<int> foundBlocks = new List<int>();
//            //		for(int i = 0; i < subNodes.Length; i++)
//            //		{
//            //			if(foundBlocks.Contains(subNodes[i].blockID))
//            //				foundBlocks.Add(subNodes[i].blockID);
//            //		}
//            //
//            //		bool combine = foundBlocks.Count == 1;
//            //		if(combine)
//            //		{
//            //			this.blockID = foundBlocks[0];
//            //			this.subNodes = null;
//            //		}
//            //	}
//            //}
//            if (hasSubNodes)
//            {

//                for (int i = 0; i < subNodes.Length; i++)
//                {
//                    subNodes[i].cantCombine = false;
//                    subNodes[i].Combine();
//                    if (subNodes[i].cantCombine)
//                        this.cantCombine = true;
//                }
//                if (parent != null)
//                {
//                    List<int> foundBlocks = new List<int>();
//                    for (int i = 0; i < subNodes.Length; i++)
//                    {
//                        if (!foundBlocks.Contains(subNodes[i].blockData.blockID))
//                            foundBlocks.Add(subNodes[i].blockData.blockID);
//                    }
//                    if (foundBlocks.Count == 1 && !this.cantCombine)
//                        this.cantCombine = false;
//                    else if (foundBlocks.Count > 1)
//                        this.cantCombine = true;

//                    if (!this.cantCombine)
//                    {
//                        this.blockData.blockID = foundBlocks.ToArray()[0];
//                        subNodes = null;
//                    }
//                }
//            }
//        }

//        public List<OctreeNode> GetChildren()
//        {
//            return GetChildren(1);
//        }

//        public List<OctreeNode> GetChildren(int tolevel)
//        {
//            if (hasSubNodes && tolevel <= level)
//            {
//                List<OctreeNode> nodes = new List<OctreeNode>();
//                for (int i = 0; i < subNodes.Length; i++)
//                    nodes.AddRange(subNodes[i].GetChildren(tolevel));
//                return nodes;
//            }
//            else
//            {
//                List<OctreeNode> nodes = new List<OctreeNode>();
//                nodes.Add(this);
//                return nodes;
//            }
//        }

//        public List<int> GetChildrenIDs()
//        {
//            List<int> foundBlocks = new List<int>();
//            if (hasSubNodes)
//            {

//                for (int i = 0; i < subNodes.Length; i++)
//                    foundBlocks.AddRange(subNodes[i].GetChildrenIDs());

//                for (int i = 0; i < subNodes.Length; i++)
//                {
//                    if (!foundBlocks.Contains(subNodes[i].blockData.blockID))
//                        foundBlocks.Add(subNodes[i].blockData.blockID);
//                }
//            }
//            else
//                foundBlocks.Add(this.blockData.blockID);
//            return foundBlocks;
//        }

//        ~OctreeNode()
//        {

//        }

//        private bool disposed = false;

//        public void Dispose()
//        {
//            Dispose(true);

//            GC.SuppressFinalize(this);
//        }

//        // Dispose(bool disposing) executes in two distinct scenarios. 
//        // If disposing equals true, the method has been called directly 
//        // or indirectly by a user's code. Managed and unmanaged resources 
//        // can be disposed. 
//        // If disposing equals false, the method has been called by the 
//        // runtime from inside the finalizer and you should not reference 
//        // other objects. Only unmanaged resources can be disposed. 
//        protected virtual void Dispose(bool disposing)
//        {
//            // Check to see if Dispose has already been called. 
//            if (!this.disposed)
//            {
//                // If disposing equals true, dispose all managed 
//                // and unmanaged resources. 
//                if (disposing)
//                {
//                    // Dispose managed resources.
//                    subNodes = null;
//                    parent = null;
//                    blockData = null;
//                }
//                // Note disposing has been done.
//                disposed = true;

//            }
//        }
//    }
//}