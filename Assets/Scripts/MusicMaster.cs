﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T1, T2>
{
    public T1 value1;
    public T2 value2;
    public Tuple(T1 v1,T2 v2)
    {
        value1 = v1;
        value2 = v2;
    }
}

public class MusicMaster : MonoBehaviour {

    private Dictionary<string, AudioSource> _musicDict;
    private float loopLength = 0;
    private float barLength = 0;
    private float beatLength = 0;
    private float currentTime = 0;

    private List<Tuple<AudioSource,float>> _toBeEdited;

    bool isAstralPlane = false;

	// Use this for initialization
	void Start () {
        _musicDict = new Dictionary<string, AudioSource>();
        _toBeEdited = new List<Tuple<AudioSource, float>>();
        AudioSource[] sources = transform.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource src in sources)
        {
            loopLength = Mathf.Max(loopLength, src.clip.length);
            _musicDict.Add(src.name, src);
        }
        barLength = loopLength / 4;
        beatLength = barLength / 4;

        MortalPlaneMode();
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;
        /* //DEBUG
        if (currentTime >= loopLength)
        {
            currentTime -= loopLength;
            RandomizeMusicLayers_Binary();
        }
        */

        foreach (Tuple<AudioSource,float> tuple in _toBeEdited)
        {
            // dont worry about it, its all good
            if (tuple.value1.volume > tuple.value2)
            {
                tuple.value1.volume -= beatLength * Time.deltaTime;
                if (tuple.value1.volume <= tuple.value2)
                {
                    tuple.value1.volume = tuple.value2;
                }
            }
            if (tuple.value1.volume < tuple.value2)
            {
                tuple.value1.volume += beatLength * Time.deltaTime;
                if (tuple.value1.volume >= tuple.value2)
                {
                    tuple.value1.volume = tuple.value2;
                }
            }
        }
        _toBeEdited.RemoveAll(x => x.value1.volume == x.value2);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAstralPlane = !isAstralPlane;
            if (isAstralPlane)
            {
                AstralPlaneMode();
            }
            else
            {
                MortalPlaneMode();
            }
        }
    }

    void RandomizeMusicLayers_Fuzzy() // DEBUG
    {
        foreach (string name in _musicDict.Keys)
        {
            SetMusicLayerVolume(name, Random.Range(0f, 1f));
        }
    }
    void RandomizeMusicLayers_Binary() // DEBUG
    {
        foreach (string name in _musicDict.Keys)
        {
            SetMusicLayerVolume(name, Random.Range(0,2));
        }
    }

    void SetMusicLayerVolume(string layerName, float volume)
    {
        _toBeEdited.RemoveAll(x => x.value1.name == layerName);
        AudioSource src;
        _musicDict.TryGetValue(layerName, out src);
        if (src != null)
        {
            _toBeEdited.Add(new Tuple<AudioSource, float>(_musicDict[layerName], volume));
        }
    }

    void AstralPlaneMode()
    {
        _musicDict["Arpeggio1"].Play();
        SetMusicLayerVolume("Beat1", 0);
        SetMusicLayerVolume("Beat2", 0);
        SetMusicLayerVolume("Beat3", 1);
        SetMusicLayerVolume("Bass", 0);
        SetMusicLayerVolume("Harmony1", 1);
        SetMusicLayerVolume("Harmony2", 1);
        SetMusicLayerVolume("Harmony3", 1);
        SetMusicLayerVolume("Harmony4", 1);
        SetMusicLayerVolume("Hook1", 1);
        SetMusicLayerVolume("Hook2", 1);
        SetMusicLayerVolume("Hook3", 1);
    }

    void MortalPlaneMode()
    {
        _musicDict["Arpeggio2"].Play();
        SetMusicLayerVolume("Beat1", 1);
        SetMusicLayerVolume("Beat2", 1);
        SetMusicLayerVolume("Beat3", 0);
        SetMusicLayerVolume("Bass", 1);
        SetMusicLayerVolume("Harmony1", 1);
        SetMusicLayerVolume("Harmony2", 1);
        SetMusicLayerVolume("Harmony3", 1);
        SetMusicLayerVolume("Harmony4", 1);
        SetMusicLayerVolume("Hook1", 0);
        SetMusicLayerVolume("Hook2", 0);
        SetMusicLayerVolume("Hook3", 0);
    }
}
