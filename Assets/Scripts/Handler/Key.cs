using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImmersivePiano
{
    //[RequireComponent(typeof(AudioSource))]
    public class Key : MonoBehaviour
    {
        //[SerializeField] AudioClip sound;
        //[SerializeField] Animator animator;
        //[SerializeField] AudioSource audioSource;
        [SerializeField] string keyName;
        [SerializeField] GameObject child;

        public static string lastPressedKey;

        private void Start()
        {
            //audioSource = GetComponent<AudioSource>();
            //animator = GetComponent<Animator>();
            //audioSource.clip = sound;
            try
            {
                child = transform.GetChild(1).gameObject;
            }
            catch (Exception) { }
        }

        //public void PlaySound()
        //{
        //    audioSource.volume = 1;
        //    audioSource.Play();

        //    animator.SetBool("Pressed", true);
        //    StopAllCoroutines();
        //    SetLastPressedKey();
        //}

        //public void StopSound()
        //{
        //    animator.SetBool("Pressed", false);
        //    StartCoroutine(DecreaseVolume());
        //}

        //IEnumerator DecreaseVolume()
        //{
        //    while (audioSource.volume > 0.01)
        //    {
        //        audioSource.volume -= 0.02f;
        //        yield return null;
        //    }
        //}

        private void SetLastPressedKey()
        {
            lastPressedKey = keyName;
            Debug.Log(lastPressedKey);
        }
    }
}
