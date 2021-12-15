using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controlers : MonoBehaviour
{
    Rigidbody2D playerRB;
    public float movespeed;
    public Animator animator;
    private bool faceright=true;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enmyLayers;

    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public Joystick joystick;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float yatayhareket = joystick.Horizontal;
        float dikeyhareket = joystick.Vertical;
        playerRB.velocity = new Vector3(yatayhareket * movespeed, dikeyhareket * movespeed);
        animator.SetFloat("run", Mathf.Abs(yatayhareket));
        animator.SetFloat("run2", Mathf.Abs(dikeyhareket));

       

        if ((yatayhareket > 0 && !faceright) || (yatayhareket < 0 && faceright))
        {
            faceright = !faceright;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }

       if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
           {
                Attack();
                nextAttackTime = Time.time + 1f / attackRange; 
            }
        }
    }


  public  void Attack()
    {
        animator.SetTrigger("attack");

      Collider2D[] hitEnemies= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enmyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}