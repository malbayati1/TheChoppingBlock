using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMover : MonoBehaviour
{
    public float bounceHeight = 1f;
    public float airTime = .5f;
    public float groundedCheckRadius;
    public float groundedCheckOffsetDown;
    public LayerMask groundedCheckLM;

    private AudioSource audioSource;

    private bool animating = false;

    //private Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        //rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Move()
    {   
        transform.localPosition = Vector3.up * transform.localPosition.y;
        if (!animating)
        {
            StartCoroutine(Arc(bounceHeight, airTime));
        }  
    }

    public IEnumerator Arc(float height, float time)
    {
        animating = true;        
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.up * height, "isLocal", true, "easeType", "easeOutQuad", "time", time * 2/3));
        iTween.MoveTo(gameObject, iTween.Hash("position",  Vector3.zero, "isLocal", true, "easeType", "easeInQuad", "time", time * 1/3, "delay", time * 2/3));
        yield return new WaitForSeconds(time);

        audioSource.clip = AudioManager.instance.stepAudio;

        audioSource.Play();

        animating = false;
    }

    public bool IsGrounded()
    {
        return Physics.OverlapSphere(
                transform.position + Vector3.down * groundedCheckOffsetDown,
                groundedCheckRadius,
                groundedCheckLM).Length > 0;
    }
}
