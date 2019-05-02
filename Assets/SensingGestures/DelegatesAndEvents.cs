using System;
using UnityEngine;
using UnityEngine.Events;
using SensingGestures.OneDollar;

namespace SensingGestures
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }

    [Serializable]
    public class Vector3Event : UnityEvent<Vector3> { }

    [Serializable]
    public class ResultEvent : UnityEvent<Result> { }

    [Serializable]
    public class RecognizerEvent : UnityEvent<GestureRecognizer> { }
}