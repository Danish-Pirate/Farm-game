using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace Utility {
    public class AudioManager : Singleton<AudioManager> {
        [SerializeField] private AudioSource[] audioSources;
        [SerializeField] private AudioClip[] audioClips;

        private Dictionary<Sound, AudioClip> audioDictionary;
        private void Start() {
            audioSources[0].volume = 0.4f;
            
            audioDictionary = new Dictionary<Sound, AudioClip>();

            // Iterate through all values of the Sound enum
            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                audioDictionary.Add(sound, audioClips[(int)sound]);
            }
        }

        public void PlaySound(Sound sound, int audioChannel) {
            if (!(audioChannel >= 0 && audioChannel < audioSources.Length)) return;
            AudioSource audioSource = audioSources[audioChannel];

            if (!audioSource.isPlaying) {
                AudioClip audioClip = audioDictionary[sound];
                audioSource.PlayOneShot(audioClip);
            }
        }

        public void StopSound(int audioChannel) {
            if (!(audioChannel >= 0 && audioChannel < audioSources.Length)) return;
            audioSources[audioChannel].Stop();
        }
    }
}