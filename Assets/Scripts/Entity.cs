using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float maxHP = default;
    [SerializeField] protected float currentHP = default;
    [SerializeField] protected float invincibleSpan = default;
    [SerializeField] protected bool isInvincible = false;
    [SerializeField] Image hpBar;
    public bool isActive = true;
    public SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetSprite();
    }

    protected virtual SpriteRenderer GetSprite()
    {
        return GetComponent<SpriteRenderer>();
    }

    protected void TallyHP()
    {
        if (currentHP < 0)
        {
            currentHP = 0;
            isActive = false;
        }
        if (currentHP > maxHP) currentHP = maxHP;
        Vector3 scale = hpBar.gameObject.transform.localScale;
        scale.x = currentHP / maxHP;
        hpBar.rectTransform.localScale = scale;
    }

    protected void ActivateInvincibility()
    {
        StartCoroutine(Invincible());
    }

    IEnumerator Invincible()
    {
        isInvincible = true;
        float timeElapsed = 0f;
        while (timeElapsed < invincibleSpan)
        {
            if (sprite != null)
            {
                sprite.color = Color.red;
                yield return new WaitForSeconds(0.2f);
                sprite.color = Color.white;
            } else
            {
                yield return new WaitForSeconds(0.2f);
            }
            
            timeElapsed += 0.2f;
            yield return null;
        }
        // yield return new WaitForSeconds(invincibleSpan);
        isInvincible = false;
    }

}
