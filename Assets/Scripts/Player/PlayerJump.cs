using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private bool isJump;
    
    private float jumpH;

    private int extraJump;
    private int initialExtraJump;

    private Animator playerAnimator;

    private BoxCollider2D playerCollider;

    private Rigidbody2D playerRB;

    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private LayerMask wallsLayerMask;
    
    void Awake()
    {
        isJump = false;
        initialExtraJump = 1;
        jumpH = 6f;
    }
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isGrounded()){
            extraJump = initialExtraJump;
        }

        jumpAnimHandler();
    }
    
    void FixedUpdate(){
        if(isJump){
            extraJump--;
            playerRB.velocity = Vector2.up * jumpH;
            isJump = false;
        }
    }

    private void jumpAnimHandler(){
        if(!isGrounded()){
            playerAnimator.SetBool("isJumping", true);
        }else{
            playerAnimator.SetBool("isJumping", false);
        }
    }

    public bool isGrounded(){
        float extraHeight = 0.1f;
        RaycastHit2D groundHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        // RaycastHit2D wallHit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, extraHeight, wallsLayerMask);
        // Debug.DrawRay(playerCollider.bounds.center + new Vector3(playerCollider.bounds.extents.x,0),Vector2.down * (playerCollider.bounds.extents.y + extraHeight));
        // Debug.DrawRay(playerCollider.bounds.center - new Vector3(playerCollider.bounds.extents.x,0),Vector2.down * (playerCollider.bounds.extents.y + extraHeight));
        // Debug.DrawRay(playerCollider.bounds.center - new Vector3(0,playerCollider.bounds.extents.y),Vector2.right * (playerCollider.bounds.extents.x));
        // Debug.Log(groundHit.collider);
        return groundHit.collider != null;
    }

    public void PointerJump(){
        if(extraJump >= 0){
            isJump = true;
        }
    }
}
