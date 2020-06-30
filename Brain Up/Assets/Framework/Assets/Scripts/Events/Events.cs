using UnityEngine.Events;

namespace Assets.Scripts.Framework.Events
{
    public class DoubleIntEvent : UnityEvent<int, int>
    {

    }

    public class IntEvent : UnityEvent<int>
    {

    }

    public class VoidEvent : UnityEvent
    {

    }

    public class FloatEvent : UnityEvent<float>
    {

    }

    public class BoolEvent : UnityEvent<bool>
    {

    }
}
