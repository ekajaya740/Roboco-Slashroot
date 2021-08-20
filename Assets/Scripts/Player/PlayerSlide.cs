using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    public bool isSlide;
    private bool isFacingRight;

    private float slidePower;
    private float maxSlideTime;
    public float slideTimer;
    private float initialSlidePower;

    private float initialSlideCooldown;
    private float slideCooldown;

    private Animator playerAnimator;

    private Rigidbody2D playerRB;
    
    private PlayerMove playerMove;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;


    void Awake()
    {
        maxSlideTime = .5f;
        initialSlidePower = 350f;
        slidePower = initialSlidePower;
        initialSlideCooldown = 1f;
        slideCooldown = 0f;
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        isFacingRight = playerMove.isFacingRight;
        slideAnimHandler();
    }

    void FixedUpdate()
    {
        slideCooldownHandler();
        if(isSlide && slideCooldown <= 0f){
            if(playerMove.isMove){
                slidePower = 150;
            }else{
                slidePower = initialSlidePower;
            }
            slideTimer += Time.deltaTime;
            Vector3 slideDirection = Vector3.zero;
            if(isFacingRight){
                playerRB.AddForce(Vector2.right * slidePower);
                
            }else if(!isFacingRight){
                playerRB.AddForce(Vector2.left * slidePower);
            }
            Physics2D.IgnoreLayerCollision(3,8, true);

            transform.Translate(slideDirection);
            if(slideTimer > maxSlideTime){
                slideCooldown = initialSlideCooldown;
                isSlide = false;
                slideTimer = 0;
            }
            
        }
    }

    private void slideAnimHandler(){
        if(isSlide){
            playerAnimator.SetBool("isSlide", true);
            playerAttack.atkCooldownCount = 0;
            
        }else{
            playerAnimator.SetBool("isSlide", false);
            Physics2D.IgnoreLayerCollision(3,8, false);
        }
    }
    
    public void PointerSlide(){
        if(playerJump.isGrounded() && slideCooldown <= 0f){
            isSlide = true;
        }
    }

    private void slideCooldownHandler(){
        if(slideCooldown >= 0f){
            slideCooldown -= Time.fixedDeltaTime;
            isSlide = false;
        }
    }
}
