using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed {get; private set; } = 5f;
    private float moveHorizontal;
    
    private float atkCooldown;
    private float atkCooldownCount;
    private float playerHealth;
    private float playerBaseDamage = 25f;
    private float playerDamage;

    private bool isMoveL;
    private bool isMoveR;
    private bool isMove;
    private bool isAttack;
    private bool isMelee;
    public bool isFacingRight {get; private set;}

    private char whichWeaponNotBuffed;
    private char weaponNow;

    private BoxCollider2D playerCollider;

    private Rigidbody2D playerRB;
    private Animator playerAnimator;
    [SerializeField] private Object playerBulletRef;
    [SerializeField] private GameObject playerBulletPos;

    private PlayerJump playerJump;


    private void Awake() {
        isMoveL = false;
        isMoveR = false;
        isMove = false;
        isAttack = false;
        isMelee = false;
        isFacingRight = true;

        whichWeaponNotBuffed = 'M';

        atkCooldown = 1f;
        playerHealth = 1000f;
        atkCooldownCount = 0f;
        
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        playerAnimator = GetComponent<Animator>();

        playerJump = GetComponent<PlayerJump>();
    }
    
    private void Update(){
        playerAnimator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        move();
        attackCooldownHandler();
        
        
        attackHandler();
    }

    private void FixedUpdate(){
        playerRB.velocity = new Vector2(moveHorizontal, playerRB.velocity.y);

    }

    // Move
    public void move(){
        if(!this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !this.playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("player_slide")){
            if(isMoveL){
                moveHorizontal = -movementSpeed;
                isMove = true;
                isFacingRight = false;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            }else if(isMoveR){
                moveHorizontal = movementSpeed;
                isMove = true;
                isFacingRight = true;
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            }else{
                isMove = false;
                moveHorizontal = 0;
            }
        }
    }

    // Attack
    private void attackHandler(){
        if(isAttack && atkCooldownCount > atkCooldown){
            playerAnimator.SetTrigger("attack");

            if(isMelee && playerJump.isGrounded()){
                weaponNow = 'M';
                playerAnimator.SetBool("isMelee", true);
                
            }else if(!isMelee){
                weaponNow = 'R';
                playerAnimator.SetBool("isMelee", false);
                GameObject bulletObj = (GameObject)Instantiate(playerBulletRef);

                if(isFacingRight){
                    bulletObj.transform.right = transform.right.normalized;
                    bulletObj.transform.rotation = Quaternion.Euler(transform.eulerAngles.y, 0f, transform.eulerAngles.z);

                }else if(!isFacingRight){
                    bulletObj.transform.rotation = Quaternion.Euler(transform.eulerAngles.y, 180f, transform.eulerAngles.z);
                }

                bulletObj.transform.position = new Vector3(playerBulletPos.transform.position.x, playerBulletPos.transform.position.y, playerBulletPos.transform.position.z);
            }

            attackDamageCalc();
            
            isAttack = false;
            atkCooldownCount = 0;
        }
    }

    private void attackCooldownHandler(){
        if(atkCooldownCount <= atkCooldown + .1f){
            atkCooldownCount += Time.deltaTime;
        }
    }
    
    private void attackDamageCalc(){
        if(whichWeaponNotBuffed != weaponNow){
            this.playerDamage = playerBaseDamage * 10;
            whichWeaponNotBuffed = weaponNow;
        }else if (whichWeaponNotBuffed == weaponNow){
            this.playerDamage = playerBaseDamage;
        }
    }

    // Pointers
    public void PointerDownMoveL(){
        isMoveL = true;
    }

    public void PointerUpMoveL(){
        isMoveL = false;
    }

    public void PointerDownMoveR(){
        isMoveR = true;
    }

    public void PointerUpMoveR(){
        isMoveR = false;
    }

    public void PointerAttack(){
        isAttack = true;
    }

    // Trigger Detect
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            isMelee = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Enemy"){
            isMelee = false;
        }
    }
}
