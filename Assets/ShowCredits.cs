using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMP;

public class ShowCredits : MonoBehaviour {
    GameObject umpPlayer;
    UniversalMediaPlayer ump;
    string path;

    private bool showing = false;
    public void Start()
    {
        path = Application.streamingAssetsPath + "/credits.mp4";

    }

    public void PlayCreditsRoll()
    {
        umpPlayer = GameObject.FindWithTag("UMP");
        ump = umpPlayer.GetComponent<UniversalMediaPlayer>();

        if (!showing)
        {
            ump.Path = path;
            ump.Play();
            showing = true;
        }
        else
        {
            ump.PlayRate = 2;
        }

    }
}
