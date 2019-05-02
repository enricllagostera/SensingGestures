using UnityEngine;
using System.Collections.Generic;
using SensingGestures.OneDollar;

namespace SensingGestures
{

    [CreateAssetMenu(fileName = "TrainingSetFile", menuName = "Sensing Gestures/Training Set File", order = 0)]
    public class TrainingSet : ScriptableObject
    {
        public List<Unistroke> allGestures;
    }
}