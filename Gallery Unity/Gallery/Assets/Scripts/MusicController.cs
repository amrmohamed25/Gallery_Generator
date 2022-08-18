using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject playButton;
    public GameObject muteButton;
    public GameObject unmuteButton;
    public AudioSource myAudio;
    public void muteAudio(){
        myAudio.volume=0;
        muteButton.SetActive(false);
        unmuteButton.SetActive(true);
    }
    public void unMuteAudio(){
        myAudio.volume=1;
        muteButton.SetActive(true);
        unmuteButton.SetActive(false);
    }

    public void pauseAudio(){
        myAudio.Pause();
        pauseButton.SetActive(false);
        playButton.SetActive(true);
    }
    public void unPauseAudio(){
        myAudio.UnPause();
        pauseButton.SetActive(true);
        playButton.SetActive(false);
    }
}
