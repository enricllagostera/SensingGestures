using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using SensingGestures.OneDollar;

namespace SensingGestures
{
    public class TrainingSetRecorder : MonoBehaviour
    {
        public KeyCode recordKey;

        public TrainingSet trainingSetFile;

        public UnityEvent onStartedRecording, onFinishedRecording;

        private List<Vector2> points;
        private bool isRecording;
        private string targetGestureName;
        private Vector3 inputValue;



        public void StartRecording(BaseEventData eventData)
        {
            targetGestureName = eventData.selectedObject.name;
            print(targetGestureName);
            points.Clear();
            onStartedRecording?.Invoke();
            isRecording = true;
        }

        public void StopRecording()
        {
            // has a recorded training set
            if (points.Count > 0)
            {
                var repeated = trainingSetFile.allGestures.Find(g => g.name == targetGestureName);
                if (repeated != null)
                {
                    trainingSetFile.allGestures.Remove(repeated);
                }
                var temp = new Unistroke(targetGestureName, points);
                trainingSetFile.allGestures.Add(temp);
            }
            onFinishedRecording?.Invoke();
            isRecording = false;
        }

        public void UpdateInputValue(Vector3 value)
        {
            inputValue = value;
        }

        private void Awake()
        {
            points = new List<Vector2>();
            isRecording = false;
        }
        void Update()
        {
            if (isRecording)
            {
                points.Add(new Vector2(inputValue.y, inputValue.z));
            }
            if (Input.GetKeyDown(recordKey))
            {
                if (onStartedRecording != null) onStartedRecording.Invoke();
            }

            if (Input.GetKeyUp(recordKey))
            {
                if (onFinishedRecording != null) onFinishedRecording.Invoke();
            }
        }
    }

}