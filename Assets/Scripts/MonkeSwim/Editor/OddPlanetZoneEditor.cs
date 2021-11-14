using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MonkeSwim.Config.OddPlanetZone))]
[CanEditMultipleObjects]
public class OddPlanetZoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MonkeSwim.Config.OddPlanetZone myTarget = (MonkeSwim.Config.OddPlanetZone)target;

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField("Visual Gravity Constraints Settings", EditorStyles.boldLabel);

        myTarget.ShowWireCube = EditorGUILayout.Toggle("Show Wire Gravity Constraints", myTarget.ShowWireCube);
        myTarget.WireCubeColour = EditorGUILayout.ColorField("Wire Gravity Constraints Colour", myTarget.WireCubeColour);

        EditorGUILayout.Space(10f);
        EditorGUILayout.LabelField("Visual Rotation Distance Settings", EditorStyles.boldLabel);

        myTarget.GravityPosition = EditorGUILayout.Vector3Field("Visual Gravity Constrained Position", myTarget.GravityPosition);
        myTarget.GravityPosition = Vector3Clamp(myTarget.GravityPosition, myTarget.MinConstraints, myTarget.MaxConstraints);

        myTarget.ShowWireSphere = EditorGUILayout.Toggle("Show Wire Visual Rotation Distance", myTarget.ShowWireSphere);
        myTarget.WireSphereColour = EditorGUILayout.ColorField("Wire Rotation distance Colour", myTarget.WireSphereColour);

        EditorGUILayout.Space(10f);

        myTarget.ShowSolidSphere = EditorGUILayout.Toggle("Show Solid Visual Rotation Distance", myTarget.ShowSolidSphere);
        myTarget.SolidSphereColour = EditorGUILayout.ColorField("Solid Rotation Distance Colour", myTarget.SolidSphereColour);
    }

    public void OnSceneGUI()
    {

        MonkeSwim.Config.OddPlanetZone myTarget = (MonkeSwim.Config.OddPlanetZone)target;

        Handles.matrix = Matrix4x4.TRS(myTarget.transform.position, myTarget.transform.rotation, Vector3.one);
        Vector3 gizmoScale = myTarget.MaxConstraints + -myTarget.MinConstraints;
        Vector3 gizmoCenter = 0.5f * (myTarget.MaxConstraints - -myTarget.MinConstraints);

        if (myTarget.ShowWireCube) {
            Handles.color = myTarget.WireCubeColour;
            Handles.DrawWireCube(gizmoCenter, gizmoScale);
        }

        if (myTarget.ShowSolidSphere || myTarget.ShowWireSphere) {
            Vector3 gravityPoint = myTarget.GravityPosition;
            gravityPoint = Handles.PositionHandle(gravityPoint, Quaternion.identity);
            myTarget.GravityPosition = Vector3Clamp(gravityPoint, myTarget.MinConstraints, myTarget.MaxConstraints);

        }

        if (myTarget.ShowSolidSphere) {
            Handles.color = myTarget.SolidSphereColour;
            Handles.SphereHandleCap(0, myTarget.GravityPosition, Quaternion.identity, myTarget.RotationDistance * 2f, EventType.Repaint);
        }

        if (myTarget.ShowWireSphere) {
            Handles.color = myTarget.WireSphereColour;
            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.up * myTarget.RotationDistance);
            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.up * -myTarget.RotationDistance);

            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.left * myTarget.RotationDistance);
            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.left * -myTarget.RotationDistance);

            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.forward * myTarget.RotationDistance);
            Handles.DrawLine(myTarget.GravityPosition, myTarget.GravityPosition + Vector3.forward * -myTarget.RotationDistance);

            Handles.DrawWireDisc(myTarget.GravityPosition, Vector3.up, myTarget.RotationDistance);
            Handles.DrawWireDisc(myTarget.GravityPosition, Vector3.left, myTarget.RotationDistance);
            Handles.DrawWireDisc(myTarget.GravityPosition, Vector3.forward, myTarget.RotationDistance);
        }
    }

    public Vector3 Vector3Clamp(Vector3 current, Vector3 min, Vector3 max)
    {
        return new Vector3(Mathf.Clamp(current.x, min.x, max.x),
                           Mathf.Clamp(current.y, min.y, max.y),
                           Mathf.Clamp(current.z, min.z, max.z));
    }
}