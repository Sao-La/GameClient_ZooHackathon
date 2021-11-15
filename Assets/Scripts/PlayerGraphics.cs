using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerGraphics : MonoBehaviour
{
    private Animator _animator;
    private bool _isFacingRight = true;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SpriteAlign(float horizontalValue)
    {
        if (_isFacingRight && horizontalValue < -.1f)
        {
            _isFacingRight = false;
            Flip();
        }
        else if (!_isFacingRight && horizontalValue > .1f)
        {
            _isFacingRight = true;
            Flip();
        }
    }

    public void AnimatorAlign(float moveSpeed)
    {
        _animator.SetFloat("move_speed", moveSpeed);
    }

    private void Flip()
    {
        var scale = transform.localScale;
        transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
    }
}
