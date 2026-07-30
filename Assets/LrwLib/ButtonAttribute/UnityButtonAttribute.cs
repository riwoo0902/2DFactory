using System;

namespace LrwLib.ButtonAttribute
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public sealed class UnityButtonAttribute : Attribute
    {
        public string Label { get; }

        public UnityButtonAttribute()
        {
        }

        public UnityButtonAttribute(string label)
        {
            Label = label;
        }
    }
}
