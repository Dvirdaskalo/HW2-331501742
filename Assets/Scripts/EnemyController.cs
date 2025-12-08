using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IHpManager, IAnimated
{
    
    [Header("Enemy Settings")]
    [SerializeField] private GameObject _player;
    [SerializeField] private spawnManager _spawner;
    [SerializeField] private float _speed = 0.05f;
    [SerializeField] private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private int bounus_damage;
    public int hp { get; set; } = 100;
    public int Damage { get; set; }
    [SerializeField] private float dist = 1.5f;
    string tagPlayer = "Player";
    public bool isDead = false;
    private Vector3 orginalScale;
    private Coroutine Attack = null;
    string tagspawner = "spawner";
    private bool animation_done = false;
    
    public void Died()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }

    IEnumerator attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(2f);
        Attack = null;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        _player = GameObject.FindGameObjectWithTag(tagPlayer);
        animator = GetComponent<Animator>();
        orginalScale =  transform.localScale;
        _spawner = GameObject.FindGameObjectWithTag(tagspawner).GetComponent<spawnManager>();
        Damage = 30 + bounus_damage;
    }

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (isDead)
        {
            if (!((IAnimated)this).IsAnimationFinised(animator, "EnemyDie"))
            {
                return;
            }
            if (!((IAnimated)this).IsAnimationFinised(animator, "enemy blink"))
            {
                animation_done = true;
                return;
            }
            if (animation_done) {
                PlayerMovement playerMovement = _player.GetComponent<PlayerMovement>();
                if (playerMovement != null)
                {
                    playerMovement.AddScore(Damage);
                }
                _spawner.spawn();
                Destroy(gameObject);
            }
        }

        if (isDead)
        {
            return;
        }
        Vector3 direction = _player.transform.position - transform.position;
        if (direction.magnitude >= dist)
        {
            transform.localScale = new Vector3(direction.x < 0 ? orginalScale.x : -orginalScale.x, orginalScale.y, 1);
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            if (Attack == null)
            {
                Attack = StartCoroutine(attack());
            }

        }
        
    }

    void FixedUpdate()
    {
        //OldMovement();
        if (isDead)
        {
            return;
        }
        
        if (rb == null)
        {
            Debug.LogWarning("Missing Rigidbody2D");
            return;
        }

        if ((_player.transform.position - transform.position).magnitude < dist)
        {
            return;
        }
        var direction = (_player.transform.position - transform.position).normalized*( _speed * Time.deltaTime);

        rb.MovePosition(rb.position + (Vector2)direction * (_speed * Time.fixedDeltaTime));

        //transform.position += new Vector3(pos.x, pos.y, 0);
    }

    private void OldMovement()
    {
        Transform playerTransform = _player.transform;
        var playerPos = playerTransform.position;
        var selfPos = transform.position;
        var direction = playerPos - selfPos;
        var normalized = direction.normalized;
        var movement = normalized*_speed;
        transform.position += new Vector3(movement.x, movement.y, 0) * Time.deltaTime;

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PunchCollision"))
        {
            PlayerMovement othercontroller = other.GetComponentInParent<PlayerMovement>();
            if (othercontroller != null)
            {
                ((IHpManager)this).lose_hp(othercontroller.Damage, tag);
                if (hp > 0)
                {
                    animator.SetTrigger("hurt");
                }
            }
        }
    }
}