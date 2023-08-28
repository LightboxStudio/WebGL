using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Runtime.InteropServices;
using System.Globalization;


namespace Imagine.WebAR
{
    [System.Serializable]
    public class Settings3DOF
    {
        public float armLength = 0.4f;
    }

    public partial class WorldTracker
    {

        void Awake_3DOF()
        {
        }

        void Start_3DOF()
        {            
            var json = "{";
            json += "\"MODE\":\"3DOF\"";
            json += "}";
#if !UNITY_EDITOR && UNITY_WEBGL
            SetWebGLwTrackerSettings(json);
#endif        
        }

        public void Update_3DOF()
        {
            
        }


        void UpdateCameraTransform_3DOF(string data)
        {
            var vals = data.Split(new string[] { "," }, System.StringSplitOptions.RemoveEmptyEntries);

            var w = float.Parse(vals[0], CultureInfo.InvariantCulture);
            var i = float.Parse(vals[1], CultureInfo.InvariantCulture);
            var j = float.Parse(vals[2], CultureInfo.InvariantCulture);
            var k = float.Parse(vals[3], CultureInfo.InvariantCulture);
            var x = float.Parse(vals[4], CultureInfo.InvariantCulture);
            var y = float.Parse(vals[5], CultureInfo.InvariantCulture);
            var z = float.Parse(vals[6], CultureInfo.InvariantCulture);
            var s = float.Parse(vals[7], CultureInfo.InvariantCulture);
            var c = float.Parse(vals[8], CultureInfo.InvariantCulture);
                
            trackerCamera.transform.position = new Vector3(x, y, z);
            trackerCamera.transform.rotation = new Quaternion(i, j, k, w);

        }

        void Place_3DOF(Vector3 pos){
            mainObject.transform.position = pos;
        }

        void Reset_3DOF()
        {
            
        }
    }
}
