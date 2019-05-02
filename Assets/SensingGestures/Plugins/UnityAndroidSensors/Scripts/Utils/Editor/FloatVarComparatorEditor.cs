using UnityAndroidSensors.Scripts.Utils.Comparator;
using UnityEditor;

namespace UnityAndroidSensors.Scripts.Utils.Editor
{
    [CustomEditor(typeof(FloatVarComparator)), CanEditMultipleObjects]
    public class FloatVarComparatorEditor : SmartVarComparatorEditor
    {
        protected override MonoScript GetScript()
        {
            return MonoScript.FromMonoBehaviour((FloatVarComparator) target);
        }
    }
}