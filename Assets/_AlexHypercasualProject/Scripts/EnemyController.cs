using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float MoveSpeed = 2f;
    Rigidbody rb;
    public Transform target;
    Vector3 moveDirection;
    public float rotationSpeed = 3; //speed of turning
    public int health;
    public ParticleSystem DestroyedParticle;
    public Animator _EnemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = this.gameObject.GetComponent<Rigidbody>();

        _EnemyAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x ) * Mathf.Rad2Deg;
            
            moveDirection = direction;
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(target.position - transform.position), rotationSpeed * Time.deltaTime);

        }
    }
    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y,moveDirection.z) * MoveSpeed;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControllerSc.Instance.GameOver();
        }
    }

    public void Damage(int Damage,float Seconds)
    {
        
        health -= Damage;
        
        
        if (health <= 0 )
        {
            DestroyedParticle.transform.position = new Vector3(transform.position.x, DestroyedParticle.transform.position.y, transform.position.z);
            DestroyedParticle.Play();
            DestroyedParticle.transform.parent = null;
            gameObject.SetActive(false);

        }
        
        
    }
   
}
