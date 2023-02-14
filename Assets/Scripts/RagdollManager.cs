using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollManager : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> bones = new List<Rigidbody>();

    private void InitBones()
    {
        bones = GetComponentsInChildren<Rigidbody>().ToList();
    }

    public void DisableColliders()
    {
        foreach (var bone in bones)
        {
            bone.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            bone.isKinematic = true;
        }
    }

    private void Awake()
    {
        InitBones();
        DisableColliders();
    }

    public void EnableColliders()
    {
        foreach (var bone in bones)
        {
            bone.isKinematic = false;
            bone.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    public void AddForce(Vector3 direction)
    {
        foreach (var bone in bones)
        {
            bone.AddForce(direction, ForceMode.Impulse);
        }
    }

    public void AddExplosion(Vector3 expPos, float multiplier)
    {
        foreach (var bone in bones)
        {
            bone.AddForce((bone.transform.position-expPos).normalized*multiplier+Vector3.up*5f, ForceMode.Impulse);
        }
    }
}
