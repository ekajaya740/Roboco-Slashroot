using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlide : MonoBehaviour
{
    private bool isSlide;
    private bool isFacingRight;

    private float slidePower;
    private float maxSlideTime;
    private float slideTimer;

    private Animator playerAnimator;

    private Rigidbody2D playerRB;
    
    private PlayerController playerController;

    void Awake()
    {
        maxSlideTime = .5f;
        slidePower = 200f;
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        isFacingRight = playerController.isFacingRight;
        slideAnimHandler();
    }

    void FixedUpdate()
    {
        if(isSlide){
            slideTimer += Time.deltaTime;
            Vector3 slideDirection = Vector3.zero;
            if(isFacingRight){
                playerRB.AddForce(Vector2.right * slidePower);
                
            }else if(!isFacingRight){
                playerRB.AddForce(Vector2.left * slidePower);
            }

            transform.Translate(slideDirection);
            if(slideTimer > maxSlideTime){
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
        if(playerController.isGrounded()){
            isSlide = true;
        }
    }
}
