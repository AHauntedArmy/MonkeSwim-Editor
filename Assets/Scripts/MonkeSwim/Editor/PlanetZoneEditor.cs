using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonkeSwim.Config.PlanetZone))]
[CanEditMultipleObjects]
public class PlanetZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MonkeSwim.Config.PlanetZone myTarget = (MonkeSwim.Config.PlanetZone)target;

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField("Visual Rotation Distance Settings", EditorStyles.boldLabel);

        myTarget.ShowWireSphere = EditorGUILayout.Toggle("Show Wire Visual Rotation Distance", myTarget.ShowWireSphere);
        myTarget.WireSphereColour = EditorGUILayout.ColorField("Wire Rotation distance Colour", myTarget.WireSphereColour);

        EditorGUILayout.Space(10f);

        myTarget.ShowSolidSphere = EditorGUILayout.Toggle("Show Solid Visual Rotation Distance", myTarget.ShowSolidSphere);
        myTarget.SolidSphereColour = EditorGUILayout.ColorField("Solid Rotation Distance Colour", myTarget.SolidSphereColour);
    }

    public virtual void OnSceneGUI()
    {
        MonkeSwim.Config.PlanetZone myTarget = (MonkeSwim.Config.PlanetZone)target;

        Handles.matrix = Matrix4x4.TRS(myTarget.transform.position, myTarget.transform.rotation, Vector3.one);

        if (myTarget.ShowSolidSphere) {
            Handles.color = myTarget.SolidSphereColour;
            Handles.SphereHandleCap(0, Vector3.zero, Quaternion.identity, myTarget.RotationDistance * 2f, EventType.Repaint); 
        }

        if (myTarget.ShowWireSphere) {

            Handles.color = myTarget.WireSphereColour;
            Handles.DrawLine(Vector3.zero, Vector3.up * myTarget.RotationDistance);
            Handles.DrawLine(Vector3.zero, Vector3.up * -myTarget.RotationDistance);

            Handles.DrawLine(Vector3.zero, Vector3.left * myTarget.RotationDistance);
            Handles.DrawLine(Vector3.zero, Vector3.left * -myTarget.RotationDistance);

            Handles.DrawLine(Vector3.zero, Vector3.forward * myTarget.RotationDistance);
            Handles.DrawLine(Vector3.zero, Vector3.forward * -myTarget.RotationDistance);

            Handles.DrawWireDisc(Vector3.zero, Vector3.up, myTarget.RotationDistance);
            Handles.DrawWireDisc(Vector3.zero, Vector3.left, myTarget.RotationDistance);
            Handles.DrawWireDisc(Vector3.zero, Vector3.forward, myTarget.RotationDistance);
        }
    }
}
