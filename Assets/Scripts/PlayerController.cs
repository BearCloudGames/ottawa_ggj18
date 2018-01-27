﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerController : MonoBehaviour {

    public float speed = 2;

    Transform sensedGhost;

    public float life = 100;

    bool dead = false;

    void Update () {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        HandleAnimation();

        if(sensedGhost!= null)
        {
            if (sensedGhost.position.x < transform.position.x)
                GamePad.SetVibration(0, (sensedGhost.GetComponent<CircleCollider2D>().radius - Vector2.Distance(sensedGhost.position, transform.position)) / sensedGhost.GetComponent<CircleCollider2D>().radius, 0);
            else
                GamePad.SetVibration(0, 0, (sensedGhost.GetComponent<CircleCollider2D>().radius-Vector2.Distance(sensedGhost.position, transform.position)) / sensedGhost.GetComponent<CircleCollider2D>().radius);
        }

        if (!dead)
        {
            life -= Time.deltaTime;
            UIManager.instance.UpdateLife(life);
            if (life <= 0)
            {
                life = 0;
                Die();
                dead = true;
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11) //ghost sensing range
        {
            sensedGhost = collision.transform;
        }
        else if (collision.gameObject.layer == 9) //ghost layer
        {
            GameManager.instance.SwitchPlanes();
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
            GameManager.instance.SwitchPlanes();
        }
    }

    void HandleAnimation()
    {

    }

    void ClearVibration()
    {
        GamePad.SetVibration(0, 0,0);
    }

    void Die()
    {
        Debug.Log("Haha RIP");
    }

    private void OnApplicationQuit()
    {
        ClearVibration();
    }
}
