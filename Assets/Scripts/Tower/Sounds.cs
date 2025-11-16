using UnityEngine;
using UnityEngine.EventSystems;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sfx;
    public AudioSource source;

    public void playAudio(int no) {
        source.PlayOneShot(sfx[no]);
    }
}
