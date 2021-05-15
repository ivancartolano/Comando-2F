using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public AudioClip[] clips;           // 0 - run| 1 - hurt

    private Vector3 direction;
    private float horizontal;
    private bool is_fire;
    private bool is_hit;
    private bool is_run;

    private AudioSource au;
    private Rigidbody rb;
    private Animation an;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        an = player.GetComponent<Animation>();
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!is_hit)
        {
            Control();
            Animations();
        }
        Sounds();
    }

    void Sounds()
    {
        if (!is_hit)
        {
            if (horizontal != 0.0f && !is_run)
            {
                is_run = true;
                au.clip = clips[0];
                au.loop = true;
                au.pitch = 0.7f;
                au.Play();
            }
            else if (horizontal == 0.0f && is_run)
            {
                is_run = false;
                au.Stop();
            }
        }
        
    }

    void Control()
    {
        if (!is_fire)
        {
            //INPUT 
            horizontal = Input.GetAxisRaw("Horizontal");

            //APLICAR VALORES OINPUT 
            direction = new Vector3(horizontal, 0.0f, 0.0f);

            //ORIENTACAO 
            if (horizontal > 0.0f)
            {
                player.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
            }
            else if (horizontal < 0.0f)
            {
                player.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
            }


            //MOVER
            controller.Move(direction * speed * Time.deltaTime);
        }
 
    }

    void Animations()
    {
        //ANIMACOES - IDLE/RUN 
        if (Input.GetAxisRaw("Horizontal") != 0.0f && !is_fire)
        {
            an.CrossFade("Run");
        }
        else if (Input.GetAxisRaw("Horizontal") == 0.0f && !is_fire)
        {
            an.CrossFade("Idle");
        }

        //ANIMACAO - FIRE 
        if (Input.GetButton("Jump") && Input.GetAxisRaw("Horizontal") == 0.0f)
        {
            horizontal = 0.0f;
            is_fire = true;
            an.CrossFade("Fire");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            is_fire = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            StartCoroutine(Hit(1.5f));
        }
    }

    IEnumerator Hit(float t)
    {
        au.clip = clips[1];
        au.loop = false;
        au.pitch = 1.0f;
        au.Play();

        is_hit = true;
        an.CrossFade("Hit");
        yield return new WaitForSeconds(t);
        is_hit = false;
    }
}
