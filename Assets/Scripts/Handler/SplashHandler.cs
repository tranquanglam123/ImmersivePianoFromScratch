using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace ImmersivePiano
{
    public class SplashHandler : MonoBehaviour
    {
        [SerializeField] GameObject SplashDIalog;

        private bool StartSPlash;
        private bool Inprogress;

        private void Start()
        {
            StartSPlash = false;
            Inprogress = false;
            SplashDIalog.SetActive(false);
        }

        private void Update()
        {
            //get the active state of the OVRCameraRig
            StartSPlash = SplashScreen.isFinished;
            if (StartSPlash && !Inprogress)
            {
                StartCoroutine(SplashEffectStart());
                Inprogress = true;

            }
        }

        IEnumerator SplashEffectStart()
        {
            SplashDIalog.SetActive(true);
            yield return new WaitForSeconds(4);
            Debug.Log("Done");
            //Load scene Asynchronous
        }
    }
}
