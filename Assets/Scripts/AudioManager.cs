using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private int audioSourceToPool = 5;
    [SerializeField] private AudioMixerGroup bgmGroup, sfxGroup;
    [SerializeField] private List<Sound> sounds = new List<Sound>();
    private AudioSource bgmSource;
    private Queue<AudioSource> sfxSourceQueue = new Queue<AudioSource>();
    private List<AudioSource> inUseSources = new List<AudioSource>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //Add AudioSources
        bgmSource = this.gameObject.AddComponent<AudioSource>();
        bgmSource.outputAudioMixerGroup = bgmGroup;
        bgmSource.loop = true;

        for (int i = 0; i < audioSourceToPool; i++)
        {
            CreateNewSource();
        }
    }

    public void Start()
    {
        PlaySound("BGM"); 
    }

    private AudioSource GetSource()
    {
        if (sfxSourceQueue.Count == 0)
        {
            CreateNewSource();
        }
        var source = sfxSourceQueue.Dequeue();
        inUseSources.Add(source);
        return source;
    }

    private void CreateNewSource()
    {
        var sfxSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource.outputAudioMixerGroup = sfxGroup;
        sfxSourceQueue.Enqueue(sfxSource);
    }

    public void PlaySoundOneShot(string name, float minPitch, float maxPitch)
    {
        Sound s = sounds.Find((sound) => sound.Name == name);
        if (s == null)
        {
            Debug.Log(name + " isn't in the list");
            return;
        }
        if (s.Type == SoundType.BGM)
        {
            SetSourceValue(bgmSource, s);
            bgmSource.pitch = Random.Range(minPitch, maxPitch);
            bgmSource.Play();
        }
        AudioSource source = GetSource();
        SetSourceValue(source, s);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        StartCoroutine(ReturnSourceToQueue(source));
    }

    public void PlaySoundOneShot(string name)
    {
        Sound s = sounds.Find((sound) => sound.Name == name);
        if (s == null)
        {
            Debug.Log(name + " isn't in the list");
            return;
        }
        if (s.Type == SoundType.BGM)
        {
            SetSourceValue(bgmSource, s);
            bgmSource.pitch = 1f;
            bgmSource.Play();
        }
        AudioSource source = GetSource();
        SetSourceValue(source, s);
        source.pitch = 1f;
        source.Play();
        StartCoroutine(ReturnSourceToQueue(source));
    }

    public void PlaySound(string name, float minPitch, float maxPitch)
    {
        Sound s = sounds.Find((sound) => sound.Name == name);
        if (s == null)
        {
            Debug.Log(name + " isn't in the list");
            return;
        }
        if (s.Type == SoundType.BGM)
        {
            SetSourceValue(bgmSource, s);
            bgmSource.pitch = Random.Range(minPitch, maxPitch);
            bgmSource.Play();
        }
        AudioSource source = inUseSources.Find((x) => x.clip == s.Clip);
        if (source == null) source = GetSource();
        SetSourceValue(source, s);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
        StartCoroutine(ReturnSourceToQueue(source));
    }

    public void PlaySound(string name)
    {
        Sound s = sounds.Find((sound) => sound.Name == name);
        if (s == null)
        {
            Debug.Log(name + " isn't in the list");
            return;
        }
        if (s.Type == SoundType.BGM)
        {
            SetSourceValue(bgmSource, s);
            bgmSource.pitch = 1f;
            bgmSource.Play();
        }
        //AudioSource source = inUseSources.Find((x) => x.clip == s.Clip);
        //if (source == null) 
        AudioSource source = GetSource();
        SetSourceValue(source, s);
        source.pitch = 1f;
        source.Play();
        //StartCoroutine(ReturnSourceToQueue(source));
    }

    public void StopSound(string name)
    {
        Sound s = sounds.Find((sound) => sound.Name == name);
        if (s == null)
        {
            Debug.Log(name + " isn't in the list");
            return;
        }
        if (s.Type == SoundType.BGM)
        {
            SetSourceValue(bgmSource, s);
            bgmSource.pitch = 1f;
            bgmSource.Stop();
        }
        //AudioSource source = inUseSources.Find((x) => x.clip == s.Clip);
        //if (source == null) 
        AudioSource source = GetSource();
        SetSourceValue(source, s);
        source.pitch = 1f;
        source.Stop();
        //StartCoroutine(ReturnSourceToQueue(source));
    }

    private IEnumerator ReturnSourceToQueue(AudioSource source)
    {
        yield return new WaitWhile(() => source.isPlaying);

        int x = inUseSources.FindIndex((s) => s == source);
        if (x >= inUseSources.Count)
        {
            yield break;
        }

        sfxSourceQueue.Enqueue(source);
        inUseSources.RemoveAt(x);
    }

    private void SetSourceValue(AudioSource source, Sound s)
    {
        source.clip = s.Clip;
        source.volume = s.Volume;
        source.loop = s.Loop;
    }
}
