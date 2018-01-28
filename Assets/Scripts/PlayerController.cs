using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using XInputDotNetPure;


public class PlayerController : MonoBehaviour {

    public float speed = 2;

    Transform sensedGhost;

    public float maxLife = 100;
    public float life;
    public float drainFactor = 2;
    public float healFactor = 10;

    void Start ()
    {
        life = maxLife;
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
            if (life >= 0)
            {
                life -= Time.deltaTime * drainFactor;
            }
            else
            {
                life = 0;
            }
            UIManager.instance.UpdateLife(life);
        }
        else
        {
            if (life <= maxLife)
            {
                life += Time.deltaTime * healFactor;
            }
            else
            {
                life = maxLife;
            }
            UIManager.instance.UpdateLife(life);
        }
    }

    void Die()
    {
        Debug.Log("haha RIP");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11) //ghost sensing range
        {
            sensedGhost = collision.transform;
        }
        else if (collision.gameObject.layer == 9) //ghost layer
        {
			// Check if the ghost has been activated or not
			if (collision.gameObject.GetComponent<SpriteRenderer> ().color.a == 0) {
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
            //GameManager.instance.SwitchPlanes();
        }
    }

    void HandleAnimation()
    {

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
