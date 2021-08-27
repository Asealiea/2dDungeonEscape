using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    //protected means that only things that inherate from enemy can change the value, like private on it's own script.
    [SerializeField] protected GameObject _diamonds;
    [SerializeField] protected int gems;
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected Transform pointA, pointB;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Vector3 currentTarget;
    protected bool isDead= false;

    protected bool isHit;
    protected Player player;
    public int Health { get; set; }
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim == null) Debug.LogError(transform.name + ":child: Animator is null");


        sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite == null) Debug.LogError(transform.name + ":child: SpiteRenderer is null");

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (player == null) Debug.LogError(transform.name + ":: Player is null");

        currentTarget = pointB.position;

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
        //if in idle state and not in combat, don't move.
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat"))
        {
            return;
        }
        if (!isDead)
        {
            Movement();
        }
    }

    public virtual void Movement()
    {
        //changes flip on sprite to look towards the right direction/anti moonwalk code.
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else if (currentTarget == pointB.position)
        {
            sprite.flipX = false;
        }

        //if monster is at point A start moving towards point B
        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        //if monster is at point B start moving towards point A
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        //moves the monster if the monster hasn't been hit.
        if (!isHit)
        {
            transform.position = Vector3.MoveTowards(transform.localPosition, currentTarget, speed * Time.deltaTime);
        }
        
        //checking for distance between enemy and player.
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        
        //direction forumlar = destination - source;
        Vector3 direction = player.transform.localPosition - transform.localPosition;
        
        //disables agro on player
        if (distance >= 2 && isHit) 
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }
        
        //enables agro if facing the player and less then 1m (still able to sneak up from behind for sneak attacks)
        else if (distance < 1f) 
        {
            if ((sprite.flipX == true && direction.x < 0)|| (sprite.flipX == false && direction.x > 0))
            {
                isHit = true;
                anim.SetBool("InCombat", true);
            }
        }

        if (isHit && anim.GetBool("InCombat"))
        {
            //same code as below just in 1 line.
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
    
    public virtual void Damage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            return;
        Debug.Log("you hit " + transform.name);
        Health -= damage;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            isDead = true;
            Health = 0;
            anim.SetTrigger("Death");
            GameObject Dia = Instantiate(_diamonds, transform.position, Quaternion.identity);
            Dia.GetComponent<Diamond>().SpawnDiamonds(gems); //private variable  and using a method to change the gems.
            Destroy(this.gameObject, 2.5f);
        }
    }
}
