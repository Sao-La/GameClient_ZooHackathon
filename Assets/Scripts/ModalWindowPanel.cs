using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ModalWindowPanel : MonoBehaviour
{
    public GameObject ModalWindowBox;
    public static ModalWindowPanel Instance;
    public GameObject ModalPanel;

    [Header("Header")]
    public Transform HeaderArea;
    public TMP_Text TitleField;

    [Header("Content")]
    public Transform ContentArea;
    public Transform VerticalLayoutArea;
    public Image ContentImage;
    public TMP_Text ContentText;

    [Header("Footer")]
    public Transform FooterArea;
    public Button ConfirmButton;
    public TMP_Text ConfirmMessage;
    public Button CancelButton;
    public TMP_Text CancelMessage;

    [Header("Button Actions")]
    private Action onConfirmAction;
    private Action onDeclineAction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ModalPanel.SetActive(false);
        ModalWindowBox.SetActive(false);
    }

    public void Confirm()
    {
        // onConfirmAction?.Invoke();
        HideModal();
    }

    public void Decline()
    {
        onDeclineAction?.Invoke();
        HideModal();
    }

    private void HideModal()
    {
        ModalPanel.SetActive(false);
        ModalWindowBox.SetActive(false);
        
    }


    public void ShowModal(string title = "", Sprite imageToShow = null, string message = "", string confirmMessage = "",
        string declineMessage = "", Action confirmAction = null, Action declineAction = null)
    {
        ModalPanel.SetActive(true);
        ModalWindowBox.SetActive(true);

        bool hasTitle = !String.IsNullOrEmpty(title);
        HeaderArea.gameObject.SetActive(hasTitle);
        TitleField.text = title;

        bool hasImage = imageToShow != null;
        ContentImage.gameObject.SetActive(hasImage);
        if (hasImage) ContentImage.sprite = imageToShow;
        ContentText.text = message;

        onConfirmAction = confirmAction;
        ConfirmMessage.text = confirmMessage;

        bool hasDecline = !String.IsNullOrEmpty(declineMessage);
        CancelButton.gameObject.SetActive(hasDecline);
        onDeclineAction = declineAction;
        CancelMessage.text = declineMessage;
    }

    public void ShowAnimalInfo(Animal animal, string confirmMessage = "",
        string declineMessage = "", Action confirmAction = null, Action declineAction = null)
    {
        ModalPanel.SetActive(true);
        ModalWindowBox.SetActive(true);


        AnimalPrefab ap = AnimalManager.Instance.GetAnimalById(animal.stat.animalId);
        if (ap == null)
        {
            print("not found");
            return;
        }

        HeaderArea.gameObject.SetActive(true);
        TitleField.text = ap.AnimalName;

        ContentImage.gameObject.SetActive(true);
        ContentImage.sprite = ap.AnimalPicture;
        
        ContentText.text = ap.Description;

        onConfirmAction = () => HideModal();
        ConfirmMessage.text = confirmMessage;

        CancelButton.gameObject.SetActive(false);
    }

}
