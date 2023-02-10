using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAudioController : MonoBehaviour
{
    [SerializeField] private HeroAnimationEvents heroAnimationEvents;
    public AudioClip LandingAudioClip;
    public AudioClip[] FootstepAudioClips;
    [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        heroAnimationEvents.OnFootstepEvent += FootstepAudio;
        heroAnimationEvents.OnLandEvent += LandingAudio;
    }

    private void Unsubscribe()
    {
        heroAnimationEvents.OnFootstepEvent -= FootstepAudio;
        heroAnimationEvents.OnLandEvent -= LandingAudio;
    }


    private void FootstepAudio(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (FootstepAudioClips.Length > 0)
            {
                int index = Random.Range(0, FootstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, FootstepAudioVolume);
            }
        }
    }

    private void LandingAudio(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(LandingAudioClip, transform.position, FootstepAudioVolume);
        }
    }
}
