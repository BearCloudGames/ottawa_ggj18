using System.Collections;
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

public class SoundMaster : MonoBehaviour {

    public static SoundMaster instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private Dictionary<string, AudioSource> _musicDict;
    private Dictionary<string, AudioSource> _sfxDict;

    private float loopLength = 0;
    private float barLength = 0;
    private float beatLength = 0;
    private float currentTime = 0;

    private List<Tuple<AudioSource,float>> _toBeEdited;

    private float _randomTime;
    private float _timer;

	// Use this for initialization
	void Start () {
        _musicDict = new Dictionary<string, AudioSource>();
        _sfxDict = new Dictionary<string, AudioSource>();
        _toBeEdited = new List<Tuple<AudioSource, float>>();
        AudioSource[] musicSources = transform.GetChild(0).GetComponentsInChildren<AudioSource>();
        foreach (AudioSource src in musicSources)
        {
            loopLength = Mathf.Max(loopLength, src.clip.length);
            _musicDict.Add(src.name, src);
        }
        AudioSource[] sfxSources = transform.GetChild(1).GetComponentsInChildren<AudioSource>();
        foreach (AudioSource src in sfxSources)
        {
            _sfxDict.Add(src.name, src);
        }
        barLength = loopLength / 4;
        beatLength = barLength / 4;
        _randomTime = Random.Range(10f, 20f);
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

        _timer += Time.deltaTime;
        if (_timer >= _randomTime)
        {
            _timer = 0;
            PlayRandomSoundEffect();
            _randomTime = Random.Range(10f, 20f);
        }

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

        /*
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
        */
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

    public void PlayMusic()
    {
        foreach (AudioSource src in _musicDict.Values)
        {
            src.Play();
        }
    }

    public IEnumerator PlayMusicInTwoSeconds()
    {
        yield return new WaitForSeconds(2.5f);
        PlayMusic();
    }

    public void AstralPlaneMode()
    {
        _sfxDict["Arpeggio1"].Play();
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

    public void MortalPlaneMode()
    {
        _sfxDict["Arpeggio2"].Play();
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

    void PlayRandomSoundEffect()
    {
        Debug.Log("MY HUSBAND WHERE");
        int random = Random.Range(0, 5);
        string soundName;
        switch (random) {
            case 0:
                soundName = "BirdCall1";
                break;
            case 1:
                soundName = "BirdCall2";
                break;
            case 2:
                soundName = "BirdCall3";
                break;
            case 3:
                soundName = "Wind";
                break;
            case 4:
                soundName = "Rain";
                break;
            default:
                soundName = "Rain";
                break;
        }
        _sfxDict[soundName].panStereo = Random.Range(-1f, 1f);
        _sfxDict[soundName].pitch = Random.Range(0.8f, 1.2f);
        _sfxDict[soundName].Play();
    }
}
