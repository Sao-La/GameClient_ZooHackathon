using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Singleton<PlayerController>
{
    
    private Rigidbody2D _rb;

    public Joystick joystick;

    public float runSpeed = 40f;
    public PlayerGraphics graphics;
    public PlayerPhysics physics;
    public GameObject weapon;
    public GameObject healthKit;
    public GameObject infoBoard;
    public Vector2 direction => _direction;

    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;
    private Vector2 _movement;

    public void Attack(bool state)
    {
        if (!state)
        {
            weapon.SetActive(false);
            return;
        }
        weapon.SetActive(true);
        weapon.GetComponent<Weapon>().Attack();
    }

    public void Heal(bool state)
    {
        if (!state)
        {
            healthKit.SetActive(false);
            return;
        }
        healthKit.SetActive(true);
        healthKit.GetComponent<Weapon>().Attack();
    }

    public void Info(bool state)
    {
        if (!state)
        {
            infoBoard.SetActive(false);
            return;
        }
        infoBoard.SetActive(true);
        infoBoard.GetComponent<Weapon>().Attack();
    }
    

    private void Awake()
    {
        graphics = GetComponentInChildren<PlayerGraphics>();
        physics = GetComponentInChildren<PlayerPhysics>();
        if (joystick == null) joystick = GameObject.Find("Joystick").GetComponent<Joystick>();

        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontal = joystick.Horizontal;
        _vertical = joystick.Vertical;

        if (_horizontal != 0 || _vertical != 0)
        {
            _direction = new Vector2(_horizontal, _vertical);
            _movement = _direction * runSpeed;
        } 
        else
        {
            _movement = Vector2.zero;
        }

        graphics.SpriteAlign(_horizontal);
        graphics.AnimatorAlign(_movement.magnitude);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * Time.fixedDeltaTime;
    }

}
