using UnityEngine;
namespace SPWN
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioClip clip1;
        public AudioClip clip2;

        private AudioSource src;

        void Start(){
            src=GetComponent<AudioSource>();
        }

        protected override void DoAwake() { }

        public void PlayAudio(AudioClip clip, float volume = 1f){
            src.PlayOneShot(clip, volume);
        }
    }
}
