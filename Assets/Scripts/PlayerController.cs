using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using XInputDotNetPure;


public class PlayerController : MonoBehaviour {

    public float speed = 2;

    Transform sensedGhost;

    public float maxLife = 100;
    float life;
    public float drainFactor = 2;
    public float healFactor = 10;

    public float Life
    {
        get { return life; }
        set
        {
            life = Mathf.Clamp(value, 0, maxLife);
        }
    }

	SpriteRenderer spriteRenderer;
	Animator animator;

    void Start ()
    {
        Life = maxLife;
		spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
		animator = GetComponentInChildren<Animator> ();
    }

    void Update () {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        HandleAnimation();

//        if(sensedGhost!= null)
//        {
//            if (sensedGhost.position.x < transform.position.x)
//                GamePad.SetVibration(0, (sensedGhost.GetComponent<CircleCollider2D>().radius - Vector2.Distance(sensedGhost.position, transform.position)) / sensedGhost.GetComponent<CircleCollider2D>().radius, 0);
//            else
//                GamePad.SetVibration(0, 0, (sensedGhost.GetComponent<CircleCollider2D>().radius-Vector2.Distance(sensedGhost.position, transform.position)) / sensedGhost.GetComponent<CircleCollider2D>().radius);
//        }

        if (sensedGhost == null)
        {

            Life -= Time.deltaTime * drainFactor;
            UIManager.instance.UpdateLife(life);
			if (Life <= 0) {
				Die ();
			}
        }
        else
        {

            Life += Time.deltaTime * healFactor;
            UIManager.instance.UpdateLife(Life);
        }
    }

    void Die()
    {
		SceneManager.LoadScene ("GameOver");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11) //ghost sensing range
        {
            sensedGhost = collision.transform;
        }
        else if (collision.gameObject.layer == 9) //ghost layer
        {
			if (collision.gameObject.GetComponent<SpriteRenderer> ().color.a == 0 && !collision.gameObject.GetComponent<Ghost>().hasReadText) {
				GameManager.instance.SwitchPlanes();
			}
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11) //ghost sensing range
        {
            sensedGhost = null;
            ClearVibration();
        }
        else if (collision.gameObject.layer == 9) //ghost layer
        {

		}
    }

    void HandleAnimation()
    {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if (moveHorizontal > 0){
            transform.localScale = new Vector2(1,1);
        }
		else if (moveHorizontal < 0){
            transform.localScale = new Vector2(-1, 1);
		}

		if (moveHorizontal == 0 && moveVertical == 0) {
			animator.SetBool ("IsMoving", false);
		} else {
			animator.SetBool ("IsMoving", true);
		}
    }

    void ClearVibration()
    {
        //GamePad.SetVibration(0, 0,0);
    }

    private void OnApplicationQuit()
    {
        ClearVibration();
    }
}
