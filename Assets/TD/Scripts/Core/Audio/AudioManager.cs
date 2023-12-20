using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private List<AudioClip> _musics;
    [SerializeField] private List<SoundData> _sounds;

    private int _currentClipIndex;

    private void Start()
    {
        _musicSource.loop = false;
        Observable.EveryUpdate()
            .First(_ => !_musicSource.isPlaying)
            .Subscribe(_ => { NextClip(); });
    }

    public void NextClip()
    {
        if (_currentClipIndex + 1 < _musics.Count)
        {
            _currentClipIndex++;
        }
        else
        {
            _currentClipIndex = 0;
        }

        _musicSource.clip = _musics[_currentClipIndex];
        _musicSource.Play();
    }

    public void PlayShoot(SoundType type)
    {
        var data = _sounds.FirstOrDefault(x => x.Type == type);
        if (data != null)
        {
            _soundSource.PlayOneShot(data.Clip);
        }
    }
}

[Serializable]
public class SoundData
{
    public SoundType Type;
    public AudioClip Clip;
}

public enum SoundType
{
    ButtonClick, Swipe
}