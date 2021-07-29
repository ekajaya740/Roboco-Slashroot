using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public bool isSlide{get; set;}
    private bool isFacingRight;

    private float slidePower;
    private float maxSlideTime;
    public float slideTimer;

    private float initialSlideCooldown;
    private float slideCooldown;

    private Animator playerAnimator;

    private Rigidbody2D playerRB;
    
    private PlayerMove playerMove;
    private PlayerJump playerJump;


    void Awake()
    {
        maxSlideTime = .5f;
        slidePower = 350f;
        initialSlideCooldown = 2f;
        slideCooldown = initialSlideCooldown;
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        isFacingRight = playerMove.isFacingRight;
        slideAnimHandler();
    }

    void FixedUpdate()
    {
        slideCooldownHandler();
        if(isSlide && !playerMove.isMove && slideCooldown >= initialSlideCooldown){
            slideTimer += Time.deltaTime;
            Vector3 slideDirection = Vector3.zero;
            if(isFacingRight){
                playerRB.AddForce(Vector2.right * slidePower);
                
            }else if(!isFacingRight){
                playerRB.AddForce(Vector2.left * slidePower);
            }

            transform.Translate(slideDirection);
            if(slideTimer > maxSlideTime){
                slideCooldown = 0;
                isSlide = false;
                slideTimer = 0;
            }
        }
    }

    private void slideAnimHandler(){
        if(isSlide){
            playerAnimator.SetBool("isSlide", true);
            
        }else{
            playerAnimator.SetBool("isSlide", false);
        }
    }
    
    public void PointerSlide(){
        if(playerJump.isGrounded() && slideCooldown >= initialSlideCooldown){
            isSlide = true;
        }
    }

    private void slideCooldownHandler(){
        if(slideCooldown <= initialSlideCooldown + .1f){
            slideCooldown += Time.fixedDeltaTime;
        }
    }
}
