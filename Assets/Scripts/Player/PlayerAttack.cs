using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private bool isAttack;
    private bool isMelee;
    private char whichWeaponNotBuffed;
    private char weaponNow;
    private float atkCooldown;
    private float atkCooldownCount;

    private float playerBaseDamage = 25f;
    private float playerDamage;
    private Animator playerAnimator;

    private PlayerMove playerMove;
    private PlayerJump playerJump;

    [SerializeField] private Object playerBulletRef;
    [SerializeField] private GameObject playerBulletPos;

    void Awake(){
        isAttack = false;
        isMelee = false;
        atkCooldown = 1f;
        whichWeaponNotBuffed = 'M';
        atkCooldownCount = 0f;
    }

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerJump = GetComponent<PlayerJump>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        attackCooldownHandler();
        attackHandler();
    }

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

                if(playerMove.isFacingRight){
                    bulletObj.transform.right = transform.right.normalized;
                    bulletObj.transform.rotation = Quaternion.Euler(transform.eulerAngles.y, 0f, transform.eulerAngles.z);

                }else if(!playerMove.isFacingRight){
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
