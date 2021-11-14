using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonkeSwim.Config.GravityZone))]
[CanEditMultipleObjects]
public class GravityZoneEditor : Editor
{
    [SerializeField]
    public Color arrowstuff;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MonkeSwim.Config.GravityZone myTarget = (MonkeSwim.Config.GravityZone)target;

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField("Visual Gravity Direction Settings", EditorStyles.boldLabel);

        myTarget.showDirection = EditorGUILayout.Toggle("Show Gravity Direction", myTarget.showDirection);
        myTarget.arrowScale = EditorGUILayout.FloatField("Size of direction arrow", myTarget.arrowScale);
        myTarget.arrowColor = EditorGUILayout.ColorField("Gravity Direction Colour", myTarget.arrowColor);
    }

    public void OnSceneGUI()
    {
        MonkeSwim.Config.GravityZone myTarget = (MonkeSwim.Config.GravityZone)target;

        if (!myTarget.showDirection || myTarget.gravityStrength == 0f) return;

        Handles.color = myTarget.arrowColor;

        Quaternion arrowDirection = Quaternion.LookRotation((myTarget.transform.up * myTarget.gravityStrength).normalized);

        Handles.ConeHandleCap(0, myTarget.transform.position, arrowDirection, HandleUtility.GetHandleSize(myTarget.transform.position) * myTarget.arrowScale, EventType.Repaint);
        Handles.ArrowHandleCap(0, myTarget.transform.position, arrowDirection, HandleUtility.GetHandleSize(myTarget.transform.position) * myTarget.arrowScale, EventType.Repaint);
    }
}
