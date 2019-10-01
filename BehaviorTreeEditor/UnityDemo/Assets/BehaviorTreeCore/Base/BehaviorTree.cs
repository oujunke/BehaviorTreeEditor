﻿using BehaviorTreeData;

namespace R7BehaviorTree
{
    public class BehaviorTree
    {
        public string AgentID { get; set; }
        public AgentData AgentData { get; set; }
        public ENodeStatus Status { get; set; } = ENodeStatus.None;
        public BaseNode StartNode { get; set; }
        public IContext Context { get; private set; }
        public int BehaviorTreeType { get; set; }

        internal void SetData(AgentData agentData)
        {
            AgentData = agentData;
            AgentID = agentData.ID;
        }

        internal void SetContext(IContext context)
        {
            Context = context;
            StartNode?.SetContext(context);
        }

        internal void CreateProxy()
        {
            StartNode?.CreateProxy();
        }

        internal void Run(float deltatime)
        {
            if (StartNode == null)
                return;

            if (Status == ENodeStatus.Failed || Status == ENodeStatus.Error || Status == ENodeStatus.Succeed)
                return;

            StartNode.Run(deltatime);

            Status = StartNode.Status;
        }

        internal void SetActive(bool active)
        {
            StartNode?.SetActive(active);
        }

        internal void Destroy()
        {
            StartNode?.Destroy();
        }
    }
}