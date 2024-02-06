using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleComponent : MonoBehaviour
{
    [Header("Parameters")]
    [Tooltip("Number of units travelled per second.")]
    public float speed;
    [Tooltip("Number of jumps the player can do before touching the ground.")]
    public int jumpsAllowed;
    [Range(1, 10)]
    public float someSlider;
    public AnimationCurve acceleration;
    public Color color;

    [Space(20)]
    [Header("References")]
    public Transform selfTransform;
    public Collider selfCollider;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
