using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PlayerController : MonoBehaviour {

    public float speed = 2;

    public float leftVibrate = 3;
    public float rightVibrate = 3;

    private void Start()
    {
        Vibration();
    }

    void Update () {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(movement.normalized * speed * Time.deltaTime);
        HandleAnimation();
	}

    void HandleAnimation()
    {

    }

    //Placeholder method to show the vibration
    void Vibration()
    {
        GamePad.SetVibration(0, rightVibrate, leftVibrate);
    }

    private void OnApplicationQuit()
    {
        GamePad.SetVibration(0, 0, 0);
    }
}
