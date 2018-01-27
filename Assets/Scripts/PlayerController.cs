using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerController : MonoBehaviour {

    public float speed = 2;

    public float leftVibrate = 3;
    public float rightVibrate = 3;

    void Update () {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        HandleAnimation();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //ghost layer
        {
            GameManager.instance.SwitchPlanes();
            SpookyVibrations();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) //ghost layer
        {
            GameManager.instance.SwitchPlanes();
            ClearVibration();
        }
    }

    void HandleAnimation()
    {

    }

    void SpookyVibrations()
    {
        GamePad.SetVibration(0, rightVibrate, leftVibrate);
    }

    void ClearVibration()
    {
        GamePad.SetVibration(0, 0,0);
    }

    private void OnApplicationQuit()
    {
        ClearVibration();
    }
}
