using UnityEngine;
using DG.Tweening;
using Cinemachine;


[RequireComponent(typeof(Rigidbody2D))]

public class Player : MonoBehaviour
{
    [Header("Walk")]
    [SerializeField, Range(1f, 10f)] private float speed;
    private float current_speed;
    [SerializeField] private float acceleration;
    [SerializeField] private float decceleration;
    [Space]
    [Header("Jump")]
    [SerializeField] private float jumpforce;
    [SerializeField] private float jumpingGravity;
    [SerializeField] private float normalGravity;
    [SerializeField] private float coyotoTime;
    [SerializeField] private float jumpBufferDuration;
    private float LastGroundedTime = -10f;
    private float LastJumpTryTime = -10f;
    [Space]
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private float groundCheckRadius;
    [Space]
    [Header("Effects")]
    [SerializeField] private Transform visuals;
    //[SerializeField] private float jumpScale;
    //[SerializeField] private float jumpAnimTime;
    //[SerializeField] private float landScale;
    //[SerializeField] private float landAnimTime;
    //[SerializeField] private float land_CamShakeAmplitude;
    //[SerializeField] private float land_CamShakeDuration;


    private Rigidbody2D rb;
    private CinemachineBasicMultiChannelPerlin camShake;
    private bool isGrounded;
    private bool wasGrounded;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        camShake = FindAnyObjectByType<CinemachineVirtualCamera>()
            .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }


    private void Update()
    {
        Jump();
        Walk();


        if (!wasGrounded && isGrounded)
        {
           // LandingEffects();
        }

        wasGrounded = isGrounded;
    }


    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers))
        {
            LastGroundedTime = Time.fixedTime;
        }

        isGrounded = Time.fixedTime - LastGroundedTime < coyotoTime;

    }









    private void Walk()
    {
        var input_axis = Input.GetAxisRaw("Horizontal");


        if ((rb.velocity.x > 0 && input_axis > 0) || ((rb.velocity.y < 0)) && input_axis < 0)
        {
            current_speed = Mathf.Lerp(current_speed, speed, acceleration * Time.deltaTime);
        }
        else if (input_axis != 0)
        {
            current_speed = Mathf.Lerp(current_speed, speed, decceleration * Time.deltaTime);
        }
        else
        {
            current_speed = Mathf.Lerp(current_speed, speed, 0f * Time.deltaTime);
        }

        rb.velocity = new Vector2(input_axis * current_speed, rb.velocity.y);
    }








    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LastJumpTryTime = Time.time;
        }


        if (Time.time - LastJumpTryTime < jumpBufferDuration && isGrounded)
        {

            //JumpEffects();
            LastJumpTryTime = -10f;

            rb.velocity = new(rb.velocity.x, 0f);
            rb.AddForce(transform.up * (jumpforce * 100f));
        }
        else if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        {
            rb.gravityScale = jumpingGravity;
        }
        else if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y >= 0)
        {
            rb.velocity = new(rb.velocity.x, 0f);
        }
        else
        {
            rb.gravityScale = normalGravity;
        }
    }








    //private void JumpEffects()
    //{
    //    visuals.DOScale(new Vector3(1 - jumpScale, 1 + jumpScale, 1f), jumpAnimTime)
    //        .OnComplete(() => visuals.DOScale(new Vector3(1f, 1f, 1f), jumpAnimTime));
    //}


    //private void LandingEffects()
    //{
    //    visuals.DOScale(new Vector3(1 + landScale, 1 - landScale, 1f), landAnimTime)
    //       .OnComplete(() => visuals.DOScale(new Vector3(1f, 1f, 1f), landAnimTime));

    //    Set_CamShake(land_CamShakeAmplitude, land_CamShakeDuration);
    //}


    //private void Set_CamShake(float _amplitude, float _duration)
    //{
    //    Set_CamShake_Amplitude(_amplitude, _duration * .15f)
    //        .OnComplete(() => Set_CamShake_Amplitude(0f, _duration * .15f)
    //        .SetDelay(_duration * .7f));
    //}



    //private DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions>
    //    Set_CamShake_Amplitude(float amplitude_, float duration_)
    //{
    //    return DOTween.To(() => camShake.m_AmplitudeGain,
    //       val => camShake.m_AmplitudeGain = val, amplitude_, duration_);
    //}





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
