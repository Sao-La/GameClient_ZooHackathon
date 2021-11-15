using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{
    public Vector2 offset = new Vector2(-0.2f, -0.2f);
    private SpriteRenderer caster;
    private SpriteRenderer shadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    private void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.localScale = new Vector3(transCaster.localScale.x, 0.3f , 0f);
        transShadow.Rotate(new Vector3(20f, 0, 0));
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "Shadow";
        transShadow.localRotation = Quaternion.identity;
        

        caster = GetComponent<SpriteRenderer>();
        shadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        shadow.material = shadowMaterial;
        shadow.color = shadowColor;
        shadow.sortingLayerName = caster.sortingLayerName;
        shadow.sortingOrder = caster.sortingOrder - 1;

    }

    private void LateUpdate()
    {
        transShadow.position = new Vector2(transCaster.position.x + offset.x,
            transCaster.position.y + offset.y);

        shadow.flipX = caster.flipX;
        
        shadow.sprite = caster.sprite;
        Color transparentColor = new Color(1, 1, 1, 0.5f);
        shadow.material.color = transparentColor;
        // shadow.color = new Color(shadow.color.r, shadow.color.g, shadow.color.b, 0.5f);
    }
}
