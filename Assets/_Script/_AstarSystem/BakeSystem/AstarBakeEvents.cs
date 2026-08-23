using _Script._Core._EventSystem;
using UnityEngine;

namespace _Script._AstarSystem.BakeSystem
{
    public static class AstarBakeEvents
    {
        public static readonly AddAstarNodeEvent AddAstarNode = new();
        public static readonly RemoveAstarNodeEvent RemoveAstarNode = new();
    }

    public class AddAstarNodeEvent : IEvent
    {
        public Vector2Int Position { get; private set; }

        public AddAstarNodeEvent Init(Vector2Int pos)
        {
            Position = pos;
            return this;
        }
    }
    public class RemoveAstarNodeEvent : IEvent
    {
        public Vector2Int Position { get; private set; }

        public RemoveAstarNodeEvent Init(Vector2Int pos)
        {
            Position = pos;
            return this;
        }
    }
}