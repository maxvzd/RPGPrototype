using NPC.Senses;
using UnityEditor;
using UnityEngine;

namespace EditorTest
{
    [CustomEditor(typeof(SightDetection))]
    public class FieldOfViewVisualiser : Editor
    {
        private void OnSceneGUI()
        {
            if (target is not SightDetection sight) return;
            
            Handles.color = Color.white;
            Handles.DrawWireArc(sight.transform.position, Vector3.up, Vector3.forward, 360, sight.Radius);
            var viewAngleRight = DirectionFromAngle(sight.transform.eulerAngles.y, -sight.FieldOfView / 2);
            var viewAngleLeft = DirectionFromAngle(sight.transform.eulerAngles.y, sight.FieldOfView / 2);
            
            Handles.color = Color.red;
            Handles.DrawLine(sight.transform.position, sight.transform.position + viewAngleRight * sight.Radius);
            Handles.DrawLine(sight.transform.position, sight.transform.position + viewAngleLeft * sight.Radius);

            foreach (var entity in sight.VisiblePoints)
            {
                Handles.color = Color.green;
                Handles.DrawLine(sight.EyePos, entity.position);
            }
        }

        private static Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}
