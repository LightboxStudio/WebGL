using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Imagine.WebAR.Samples
{
    public class SyncVideoSound : MonoBehaviour
    {
        [SerializeField] VideoPlayer video;
        [SerializeField] AudioSource sound;

        void OnEnable(){
            StartCoroutine("SyncRoutine");
        }

        void OnDisable(){
            StopCoroutine("SyncRoutine");
        }
        
        IEnumerator SyncRoutine()
        {
            while(!video.isPrepared){
                Debug.Log("Waiting video preparation");
                yield return null;
            }

            video.Play();
            sound.Play();

            while(true){
                if(Mathf.Abs(sound.time - (float)video.time) > 0.5){
                    sound.time = (float)video.time;
                    Debug.Log(sound.time + "=>" + video.time);
                }
                yield return new WaitForSeconds(1);
            }
        }

        
    }
}
