using System;
using UnityEngine;

public class HeroAnimationEvents : MonoBehaviour
{
    public Action<AnimationEvent> OnLandEvent;
    public Action<AnimationEvent> OnFootstepEvent;

    private void OnFootstep(AnimationEvent animationEvent)
    {
        OnFootstepEvent?.Invoke(animationEvent);
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        OnLandEvent?.Invoke(animationEvent);
    }
}
