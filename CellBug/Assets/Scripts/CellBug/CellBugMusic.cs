using UnityEngine;
using System.Collections;

public class CellBugMusic : MonoBehaviour {

    public AudioClip[] cellBugMateMusic;
    public AudioClip[] cellBugDeadMusic;
    public AudioClip[] cellAttackMusic;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlayMateMusic(int mateLevel)
    {
        AudioSource source = this.GetComponent<AudioSource>();
        source.clip = cellBugMateMusic[mateLevel];
        source.Play();
    }

    public void PlayDeadMusic(Const.DeadEnum deadType)
    {
        AudioSource source = this.GetComponent<AudioSource>();
        source.clip = cellBugDeadMusic[0];
        source.Play();
    }

    public void PlayAttackMusic()
    {
        AudioSource source = this.GetComponent<AudioSource>();
        source.clip = cellAttackMusic[0];
        source.Play();
    }
}
