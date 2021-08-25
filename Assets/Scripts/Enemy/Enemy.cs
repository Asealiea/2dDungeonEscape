using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //protected means that only things that inherate from enemy can change the value, like private on it's own script.
    [SerializeField] protected int gems;
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected Transform pointA, pointB;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Vector3 currentTarget;

    protected bool isHit;
    protected GameObject player;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null) Debug.LogError(transform.name + ":child: Animator is null");


        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null) Debug.LogError(transform.name + ":child: SpiteRenderer is null");

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError(transform.name + ":: Player is null");

    }

    private void Start()
    {
        Init();
    }

    public virtual void Attack() //virtual allows it to be overridden in other scripts
    {

    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat"))
        {
            return;
        }
        Movement();
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }
        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.localPosition, currentTarget, speed * Time.deltaTime);
        }
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (distance >= 2 && isHit)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        else if (distance < 1f)
        {
            if ((sprite.flipX == true && direction.x < 0)|| (sprite.flipX == false && direction.x > 0))
            {
                isHit = true;
                anim.SetBool("InCombat", true);

            }
        }

        //direction forumlar = destination - source;
        if (isHit && anim.GetBool("InCombat"))
        {
            sprite.flipX = direction.x > 0 ? false : true;
          /*  if (direction.x > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            */
        }
    }
    

}
