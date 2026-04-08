using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AuthManager : MonoBehaviour
{
    [Header("TMP Input Fields Login")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;

    [Header("TMP Input Fields Register")]
    public TMP_InputField emailInputR;
    public TMP_InputField passwordInputR;
    public TMP_InputField usernameInputR;

    public GameObject loginPanel;
    public GameObject registerPanel;

    [Header("TMP Text for feedback")]
    public TMP_Text feedbackText;

    private ServiceFactory services;

    private void Awake()
    {
        services =
            new ServiceFactory();
    }

    public void ShowLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public void ShowRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }

    public void OnRegisterButton()
    {
        RegisterData data =
            new RegisterData
            {
                email = emailInputR.text,
                username = usernameInputR.text,
                password = passwordInputR.text
            };

        if (string.IsNullOrEmpty(data.email) ||
            string.IsNullOrEmpty(data.username) ||
            string.IsNullOrEmpty(data.password))
        {
            feedbackText.text =
                "Заполните все поля";

            return;
        }

        StartCoroutine(
            services.Auth.Register(
                data,
                OnRegisterResult));
    }

    private void OnRegisterResult(bool success)
    {
        if (success)
        {
            feedbackText.text =
                "Пользователь зарегистрирован!";
        }
        else
        {
            feedbackText.text =
                "Ошибка регистрации";
        }
    }

    public void OnLoginButton()
    {
        LoginData data =
            new LoginData
            {
                email = emailInput.text,
                password = passwordInput.text
            };

        if (string.IsNullOrEmpty(data.email) ||
            string.IsNullOrEmpty(data.password))
        {
            feedbackText.text =
                "Заполните email и пароль";

            return;
        }

        StartCoroutine(
            services.Auth.Login(
                data,
                OnLoginSuccess));
    }

    private void OnLoginSuccess(
        LoginResponse response)
    {
        feedbackText.text =
            "Вход успешен!";

        SceneManager.LoadScene(
            "Main_Menu");
    }
}