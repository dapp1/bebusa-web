using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedlightGreenlightController : MonoBehaviour
{
    public static RedlightGreenlightController instance;

    [SerializeField] private Image greenImage;
    [SerializeField] private Image redImage;
    [SerializeField] public float minInterval = 2f;
    [SerializeField] public float maxInterval = 5f;
    [SerializeField] private Animator doll;
    [SerializeField] private int killsEveryLevel = 2;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _songClip;

    public static bool isGreen = true;
    public static event OnGreenlight onGreen;
    public static event OnRedlight onRed;

    public static int needKills = 0;
    public static float currentInterval = 0f;

    private void Start()
    {
        instance = this;
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        while (true)
        {
            float greenInterval = Random.Range(minInterval, maxInterval);
            float redInterval = Random.Range(minInterval, maxInterval);

            GreenLight(greenInterval);
            yield return new WaitForSecondsRealtime(redInterval);

            RedLight();
            yield return new WaitForSecondsRealtime(greenInterval);
        }
    }

    private void GreenLight(float greenInterval)
    {
        if ((doll.GetCurrentAnimatorStateInfo(0).IsName("Doll_Idle") ||
          doll.GetCurrentAnimatorStateInfo(0).IsName("Empty")) == false)
            doll.Play("Doll_Idle");
        doll.SetTrigger("StartCount");

        currentInterval = greenInterval;
        needKills = killsEveryLevel;

        isGreen = true;
        onGreen?.Invoke();

        PlayAudioClip(_songClip, 4.8f / greenInterval);

        greenImage.gameObject.SetActive(true);
        redImage.gameObject.SetActive(false);
    }

    private void RedLight()
    {
        if (doll.GetCurrentAnimatorStateInfo(0).IsName("Doll_Count") == false)
            doll.Play("Doll_Count");
        doll.SetTrigger("StopCount");

        isGreen = false;
        onRed?.Invoke();

        greenImage.gameObject.SetActive(false);
        redImage.gameObject.SetActive(true);

        _audioSource.Stop();
    }

    private void PlayAudioClip(AudioClip clip, float pitch = 1f)
    {
        _audioSource.pitch = pitch;
        _audioSource.clip = clip;
        _audioSource.Play();
    }

}

public delegate void OnGreenlight();
public delegate void OnRedlight();
