using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SensingGestures
{
    public class AverageFilter : MonoBehaviour
    {
        public List<Vector3> readings;
        public Vector3 avgV3;
        public int frameWindow;
        public Vector3Event OnNewAverage;

        #region [Public API]

        public void AddNewReading(Vector3 value)
        {
            readings.Add(value);
            if (readings.Count > frameWindow)
            {
                readings.RemoveAt(0);
            }
            avgV3 = new Vector3();
            avgV3.x = readings.Average(a => a.x);
            avgV3.y = readings.Average(a => a.y);
            avgV3.z = readings.Average(a => a.z);
            if (OnNewAverage != null) OnNewAverage.Invoke(avgV3);
        }
        #endregion

        #region [Messages]
        private void OnEnable()
        {
            readings = new List<Vector3>();
            avgV3 = new Vector3();
        }
        #endregion
    }

}