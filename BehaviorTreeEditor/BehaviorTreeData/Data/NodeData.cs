﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BehaviorTreeData
{
    public partial class NodeData : Binary
    {
        public List<BaseFiled> Fileds = new List<BaseFiled>();
        public List<NodeData> Childs = null;

        public override void Read(ref Reader reader)
        {
            reader.Read(ref Fileds).Read(ref Childs);
        }

        public override void Write(ref Writer writer)
        {
            writer.Write(Fileds).Write(Childs);
        }
    }
}