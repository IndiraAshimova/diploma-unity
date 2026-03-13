using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

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

    [Header("TMP Text for feedback (optional)")]
    public TMP_Text feedbackText;

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
        string email = emailInputR.text;
        string username = usernameInputR.text;
        string password = passwordInputR.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Заполните все поля";
            return;
        }

        StartCoroutine(RegisterCoroutine(email, username, password));
    }

    private IEnumerator RegisterCoroutine(string email, string username, string password)
    {
        RegisterData data = new RegisterData { email = email, username = username, password = password };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(APIConfig.AUTH + "/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Register Error: " + request.error);
            feedbackText.text = "Ошибка регистрации: " + request.error;
        }
        else
        {
            Debug.Log("Registered: " + request.downloadHandler.text);
            feedbackText.text = "Пользователь зарегистрирован!";
        }
    }

    public void OnLoginButton()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            feedbackText.text = "Заполните email и пароль";
            return;
        }

        StartCoroutine(LoginCoroutine(email, password));
    }

    private IEnumerator LoginCoroutine(string email, string password)
    {
        LoginData data = new LoginData { email = email, password = password };
        string jsonData = JsonUtility.ToJson(data);

        UnityWebRequest request = new UnityWebRequest(APIConfig.AUTH + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Login Error: " + request.error);
            feedbackText.text = "Ошибка входа: " + request.error;
        }
        else
        {
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            PlayerPrefs.SetString("jwt_token", response.token);
            feedbackText.text = "Вход успешен!";
            SceneManager.LoadScene("Main_Menu");
        }
    }
}