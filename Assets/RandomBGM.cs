using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomBGM : MonoBehaviour
{
    public AudioClip[] Music;
    AudioSource AS;
    public Text BgmName;

    private void Awake()
    {
        AS = this.GetComponent<AudioSource>();
    }

    private void Update()
    {

        if(!AS.isPlaying)
        {
            RandomPlay();
        }
    }

    public void RandomPlay()
    {
        AS.clip = Music[Random.Range(0, Music.Length)];
        BgmName.text = AS.clip.name.ToString();
        AS.Play();
    }

}
