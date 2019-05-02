using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;
using SensingGestures.OneDollar;

namespace SensingGestures
{
    public class GestureRecognizer : MonoBehaviour
    {
        public TrainingSet trainingSetFile;
        public CoordinatePair coordinatesToUse;
        public GameObject oscSensor, nativeSensor;
        public KeyCode debugRecognizingKey;
        public ResultEvent onRecognizedGesture;
        public StringEvent onRecognizedName;
        public StringEvent onDebugInputValue;
        public StringEvent onDebugProcessedValue;
        private List<Vector2> points;
        private Vector3 inputValue;
        private bool isRecordingForTrainingSet;
        private bool isRecordingForRecognizing;
        private DollarRecognizer recognizer;


        #region [Public API]

        public void UpdateInputValue(Vector3 value)
        {
            inputValue = value;
            onDebugInputValue?.Invoke(value.ToString());
            ProcessInputValue();
        }

        public void SetRecordingMode(bool recordMode)
        {
            // just started recording
            if (recordMode)
            {
                points.Clear();
            }
            // just stopped recording
            else
            {
                // has a recorded training set
                if (points.Count > 0)
                {
                    var temp = recognizer.SavePattern("gesture_" + (trainingSetFile.allGestures.Count + 1), points);
                    trainingSetFile.allGestures.Add(temp);
                }
            }
            isRecordingForTrainingSet = recordMode;
        }

        public void SetRecognizingMode(bool recognizingMode)
        {
            if (recognizingMode)
            {
                points.Clear();
            }
            else
            {
                // runs the classification algorithm
                if (points.Count > 0)
                {
                    var result = recognizer.Recognize(points, trainingSetFile.allGestures);
                    onRecognizedGesture?.Invoke(result);
                    if (result.Match != null)
                    {
                        onRecognizedName?.Invoke(result.Match.name);
                    }
                    else
                    {
                        onRecognizedName?.Invoke("No match for gesture.");
                    }
                    Debug.Log(string.Format("Gesture:{0}, with score {1}", result.Match.name, result.Score));
                }
            }
            isRecordingForRecognizing = recognizingMode;
        }

        #endregion

        #region [Messages]

        private void Awake()
        {
            recognizer = new DollarRecognizer();
#if UNITY_EDITOR
            oscSensor.SetActive(true);
            nativeSensor.SetActive(false);
#else
            oscSensor.SetActive(false);
            nativeSensor.SetActive(true);
#endif
        }

        void Start()
        {
            points = new List<Vector2>();
            isRecordingForRecognizing = false;
            isRecordingForTrainingSet = false;
        }

        void Update()
        {
            if (Input.GetKeyDown(debugRecognizingKey))
            {
                SetRecognizingMode(true);
            }
            if (Input.GetKeyUp(debugRecognizingKey))
            {
                SetRecognizingMode(false);
            }
            if (isRecordingForTrainingSet || isRecordingForRecognizing)
            {
                points.Add(ProcessInputValue());
            }
        }

        #endregion

        Vector2 ProcessInputValue()
        {
            Vector2 coordinate = Vector2.zero;
            switch (coordinatesToUse)
            {
                case CoordinatePair.XY:
                    coordinate = new Vector2(inputValue.x, inputValue.y);
                    break;
                case CoordinatePair.XZ:
                    coordinate = new Vector2(inputValue.x, inputValue.z);
                    break;
                case CoordinatePair.YZ:
                    coordinate = new Vector2(inputValue.y, inputValue.z);
                    break;
            }
            onDebugProcessedValue?.Invoke(coordinate.ToString());
            return coordinate;
        }
    }

}