using System;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour, IHpManager, IAnimated
{
    [SerializeField] TMP_Text scoreText;
    public int hp { get; set; } = 100;
    public int Damage { get; set; } = 50;
    
    public void Died()
    { 
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        foreach (var script in enemy.GetComponents<MonoBehaviour>())
        {
            script.enabled = false;
        }
        GameManager.Instance.GetComponent<GameManager>().died();
        Time.timeScale = 0;
        scoreText.text= "Your final score is: " + score.ToString()+" even a child could do better!";
        Destroy(gameObject);
        
    }
    
    [SerializeField] private float _speed = 25f;
    Vector3 direction = Vector3.zero;
    
    int score = 0;

    private Vector3 orginalScale;
    
    private Rigidbody2D rb;
    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        orginalScale =  transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //WASDControlls();
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("Attack");

        if (!((IAnimated)this).IsAnimationFinised(animator, "PlayerPunch"))
        {
            return;
        }


        var y = Input.GetAxis("Vertical");
        var x = Input.GetAxis("Horizontal");
        direction = new Vector3(x, y, 0);
        
        
        bool isWalking =   x != 0 || y != 0;
        animator.SetBool("IsWalking", isWalking);


        if (isWalking)
        {
            transform.localScale = new Vector3(x < 0 ? -orginalScale.x : orginalScale.x, orginalScale.y, 1);
        }
        
        if (rb == null)
        {
            Debug.LogWarning("Missing Rigidbody2D");
            return;
        }
        
        rb.MovePosition(rb.position + (Vector2)direction * (_speed * Time.fixedDeltaTime));
        
        // transform.position += direction * (_speed * Time.fixedDeltaTime);

    }

    

    private void FixedUpdate()
    {
            }

    public void AddScore(int amount)
    {
        score += amount;
        
        GameManager.Instance.HUD.GetComponent<HUD>().UpdateScore(score);
    }

    private void WASDControlls()
    {
        Vector2 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        transform.position +=(Vector3)direction * (_speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PunchCollision"))
        {
            EnemyController othercontroller = other.GetComponentInParent<EnemyController>();
            if ((othercontroller != null) && !(othercontroller.isDead))
            {
                ((IHpManager)this).lose_hp(othercontroller.Damage, tag);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        
    }
}
