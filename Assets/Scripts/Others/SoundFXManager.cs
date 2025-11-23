using Unity.VisualScripting;
using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;
    public AudioSource soundOjbect;
    public AudioClip soundBGOject;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public void Start()
    {
        soundOjbect.clip = soundBGOject;   // assign the clip
        soundOjbect.loop = true;           // make it loop
        soundOjbect.Play();                // start playing
    }
    public void PlaySound(AudioClip clip,Transform spawnstransfrom)
    {
        AudioSource audioSource= Instantiate(soundOjbect, spawnstransfrom.position,Quaternion.identity);
        audioSource.clip = clip;
        audioSource.Play();
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        float cliplength=audioSource.clip.length;
        Destroy(audioSource.gameObject,cliplength);
    }
    
}


