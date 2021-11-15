using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField field_email;
    [SerializeField] TMP_InputField field_password;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Login()
    {
        string email = field_email.text;
        string password = field_password.text;
        AuthInfo info = new AuthInfo(email, password);
        Action a = () => LoadScene("Main"); 
        APIHelper.Instance.SendJson("https://saola.vegto.me/signin", Newtonsoft.Json.JsonConvert.SerializeObject(info), a);

    }

    public void Quit()
    {
        Application.Quit(0);
    }
}


