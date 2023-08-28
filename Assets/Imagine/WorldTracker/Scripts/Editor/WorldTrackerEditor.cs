using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Imagine.WebAR.Editor
{
    [CustomEditor(typeof(WorldTracker))]
    public class WorldTrackerEditor : UnityEditor.Editor
    {
        WorldTracker _target;

        bool showKeyboardCameraControls = false;


        private void OnEnable()
        {
            _target = (WorldTracker)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("trackerCamera"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mode"));

            if (_target.mode == WorldTracker.TrackingMode.MODE_3DOF)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                ;
                var rssProp = serializedObject.FindProperty("s3dof");
                //rssProp.isExpanded = true;
                EditorGUILayout.PropertyField(rssProp, new GUIContent("3DOF Settings"));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }
            else if (_target.mode == WorldTracker.TrackingMode.MODE_6DOF)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                ;
                var ttsProp = serializedObject.FindProperty("s6dof");
                //ttsProp.isExpanded = true;
                EditorGUILayout.PropertyField(ttsProp, new GUIContent("6DOF Settings"));
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mainObject"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("cameraStartHeight"));

            EditorGUILayout.Space(20);
            var usePlacementIndicatorProp = serializedObject.FindProperty("usePlacementIndicator");
            EditorGUILayout.PropertyField(usePlacementIndicatorProp);
            if(usePlacementIndicatorProp.boolValue){
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                ;
                var psProp = serializedObject.FindProperty("placementIndicatorSettings");
                //psProp.isExpanded = true;
                EditorGUILayout.PropertyField(psProp, new GUIContent("Placement Indicator Settings"));
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }
            else{
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Your main object will auto-placed");
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.Space(20);
            var esProp = serializedObject.FindProperty("eventSettings");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(esProp, new GUIContent("Event Settings"));
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space(20);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("debugMode"));



            EditorGUILayout.Space();
            //keyboard camera controls
            showKeyboardCameraControls = EditorGUILayout.Toggle ("Show Keyboard Camera Controls", showKeyboardCameraControls);
            if(showKeyboardCameraControls){
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                EditorGUILayout.LabelField("W", "Move Forward (Z)");
                EditorGUILayout.LabelField("S", "Move Backward (Z)");
                EditorGUILayout.LabelField("A", "Move Left (X)");
                EditorGUILayout.LabelField("D", "Move Right (X)");
                EditorGUILayout.LabelField("R", "Move Up (Y)");
                EditorGUILayout.LabelField("F", "Move Down (Y)");
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Up Arrow", "Tilt Up (along X-Axis)");
                EditorGUILayout.LabelField("Down Arrow", "Tilt Down (along X-Axis)");
                EditorGUILayout.LabelField("Left Arrow", "Tilt Left (along Y-Axis)");
                EditorGUILayout.LabelField("Right Arrow", "Tilt Right (Along Y-Axis)");
                EditorGUILayout.LabelField("Period", "Tilt Clockwise (Along Z-Axis)");
                EditorGUILayout.LabelField("Comma", "Tilt Counter Clockwise (Along Z-Axis)");
                EditorGUILayout.Space(40);
                EditorGUILayout.PropertyField(serializedObject.FindProperty("debugCamMoveSensitivity"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("debugCamTiltSensitivity"));
                EditorGUILayout.EndVertical();
            }

            serializedObject.ApplyModifiedProperties();


        }
    }
}

