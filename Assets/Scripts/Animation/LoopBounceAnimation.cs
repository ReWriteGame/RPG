using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBounceAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 direction = Vector3.up;
    [SerializeField] private Vector3 addShiftDistance;
    [SerializeField] private float heightBounce;
    [SerializeField] private float speedMove;
    [SerializeField] private bool animated;

    [SerializeField] private bool useDelay;
    [SerializeField] private Vector2 delay;
    [SerializeField] private bool playOnAwake = true;


    private bool isUpMove = true;



    private void Start()
    {
        StartCoroutine(Bounce());
        if(playOnAwake) Play();
    }


    private IEnumerator Bounce()
    {
        Vector3 startPosition = transform.position;

        if(useDelay) yield return new WaitForSeconds(Random.Range(delay.x, delay.y));

        while (true)
        {
            if (animated)
            {
                Vector3 velocity = Vector3.zero;
                Vector3 targetPoint = startPosition + direction.normalized * heightBounce * (isUpMove ? 1 : -1);

                if (Vector3.Distance(transform.position, targetPoint + addShiftDistance) > 0.01f)
                    velocity = direction.normalized * speedMove * (isUpMove ? 1 : -1);
                else isUpMove = !isUpMove;

                transform.position += velocity * Time.deltaTime;
            }
            yield return null;
        }
    }
    public void Play()
    {
        animated = true;
    }

    public void Stop()
    {
        animated = false;
    }
}
