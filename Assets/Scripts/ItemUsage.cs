using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public enum ItemType
{
    WEAPON,
    INFO,
    HEALTH
}

public class ItemUsage : Singleton<ItemUsage>
{
    [SerializeField] Button button_activate;
    [SerializeField] Button button_weapon;
    [SerializeField] Button button_hand;
    [SerializeField] Button button_health;
    [SerializeField] Image image_activate;
    [SerializeField] Sprite sprite_weapon;
    [SerializeField] Sprite sprite_health;
    [SerializeField] Sprite sprite_info;

    private ItemType _selectedItemType = ItemType.WEAPON;

    private void Start()
    {
        button_weapon.onClick.AddListener(() => SelectItemType(ItemType.WEAPON));
        button_hand.onClick.AddListener(() => SelectItemType(ItemType.INFO));
        button_health.onClick.AddListener(() => SelectItemType(ItemType.HEALTH));

        button_activate.onClick.AddListener(() => UseItem());
    }

    public void UseItem()
    {
        switch (_selectedItemType)
        {
            case ItemType.WEAPON:
                // player attacks
                PlayerController.Instance.Heal(false);
                PlayerController.Instance.Info(false);
                PlayerController.Instance.Attack(true);
                // image_activate.sprite = sprite_weapon;
                break;
            case ItemType.HEALTH:
                PlayerController.Instance.Heal(true);
                PlayerController.Instance.Info(false);
                PlayerController.Instance.Attack(false);
                // image_activate.sprite = sprite_health;
                // player recovers health to animals
                break;
            case ItemType.INFO:
                PlayerController.Instance.Heal(false);
                PlayerController.Instance.Info(true);
                PlayerController.Instance.Attack(false);
                // image_activate.sprite = sprite_info;
                // player feeds animal
                break;
        }
    }

    public void SelectItemType(ItemType type)
    {
        _selectedItemType = type;
        switch (_selectedItemType)
        {
            case ItemType.WEAPON:
                // player attacks
                PlayerController.Instance.Heal(false);
                PlayerController.Instance.Info(false);
                PlayerController.Instance.Attack(true);
                image_activate.sprite = sprite_weapon;
                break;
            case ItemType.HEALTH:
                PlayerController.Instance.Heal(true);
                PlayerController.Instance.Info(false);
                PlayerController.Instance.Attack(false);
                image_activate.sprite = sprite_health;
                // player recovers health to animals
                break;
            case ItemType.INFO:
                PlayerController.Instance.Heal(false);
                PlayerController.Instance.Info(true);
                PlayerController.Instance.Attack(false);
                image_activate.sprite = sprite_info;
                // player feeds animal
                break;
        }
    }

}
