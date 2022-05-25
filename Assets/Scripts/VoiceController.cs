using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
using UnityEngine.UI;
using UnityEngine.Android;

public class VoiceController : MonoBehaviour
{
    private string LANG_CODE_US = "en-US";
    private string LANG_CODE_RUS = "ru-RU";

    [SerializeField]
    Text uiText;

    private string answers = "";
    private string question;

    private void Start()
    {
        Setup(LANG_CODE_RUS);

#if UNITY_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            Permission.RequestUserPermission(Permission.Microphone);
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult; 
#endif
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

        StartSpeaking("Добрый вечер, моя госпажа! Давайте решим что вы будите ужинать.");
    }

    #region Text to Speech
    public void StartSpeaking(string message)
    {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("Talking started...");
    }
    void OnSpeakStop()
    {
        Debug.Log("Talking stopped...");
    }
    #endregion

    #region Speech To Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string message)
    {
        uiText.text = message;
        switch (message)
        {
            case "Давай":
            case "давай":
                if (answers == "")
                {
                    question = "Лень готовить?";
                    StartSpeaking(question);
                }
                else
                {
                    StartSpeaking("Лаааадно");
                }
                break;
            case "Да":
            case "да":
                answers += "Да";
                switch (answers)
                {
                    case "Да":
                        question = "Больше 500 рублей?";
                        StartSpeaking(question);
                        break;
                    case "ДаДа":
                        StartSpeaking("KFC");
                        uiText.text = "KFC";
                        break;
                    case "НетДа":
                        question = "Будешь варить?";
                        StartSpeaking(question);
                        break;
                    case "НетДаДа":
                    case "НетНетДа":
                        question = "С мясом?";
                        StartSpeaking(question);
                        break;
                    case "НетДаДаДа":
                        StartSpeaking("Пельмени");
                        uiText.text = "Пельмени";
                        break;
                    case "НетДаНетДа":
                        StartSpeaking("Жаренные макароны");
                        uiText.text = "Жаренные макароны";
                        break;
                    case "НетНетДаДа":
                        StartSpeaking("Стейк");
                        uiText.text = "Стейк";
                        break;
                    case "НетНетНетДа":
                        question = "Макароны?";
                        StartSpeaking(question);
                        break;
                    case "НетНетНетДаДа":
                        StartSpeaking("Паста");
                        uiText.text = "Паста";
                        break;
                    case "НетНетНетНетДа":
                        question = "С гарниром?";
                        StartSpeaking(question);
                        break;
                    case "НетНетНетНетДаДа":
                        StartSpeaking("Ризотто");
                        uiText.text = "Ризотто";
                        break;
                    default:
                        break;
                }
                break;
            case "Нет":
            case "нет":
                answers += "Нет";
                switch (answers)
                {
                    case "Нет":
                        question = "Сейчас хочется кушать?";
                        StartSpeaking(question);
                        break;
                    case "ДаНет":
                        StartSpeaking("Шаурма");
                        uiText.text = "Шаурма";
                        break;
                    case "НетНет":
                    case "НетДаНет":
                        question = "Будешь жарить?";
                        StartSpeaking(question);
                        break;
                    case "НетДаДаНет":
                        StartSpeaking("Гречка");
                        uiText.text = "Гречка";
                        break;
                    case "НетДаНетНет":
                        StartSpeaking("Салатик");
                        uiText.text = "Салатик";
                        break;
                    case "НетНетНет":
                        question = "Будешь варить?";
                        StartSpeaking(question);
                        break;
                    case "НетНетДаНет":
                        StartSpeaking("Жаренная картошка");
                        uiText.text = "Жаренная картошка";
                        break;
                    case "НетНетНетНет":
                        question = "С мясом?";
                        StartSpeaking(question);
                        break;
                    case "НетНетНетДаНет":
                        StartSpeaking("Суп");
                        uiText.text = "Суп";
                        break;
                    case "НетНетНетНетНет":
                        StartSpeaking("Рагу");
                        uiText.text = "Рагу";
                        break;
                    case "НетНетНетНетДаНет":
                        StartSpeaking("Тушенный кролик");
                        uiText.text = "Тушенный кролик";
                        break;
                    default:
                        break;
                }
                break;
            case "Повтори":
            case "повтори":
                StartSpeaking(question);
                break;
            default:
                StartSpeaking("Лаааадно");
                break;
        }
    }

    void OnPartialSpeechResult(string message)
    {
        uiText.text = message;
    }

    #endregion

    void Setup(string langCode)
    {
        TextToSpeech.instance.Setting(langCode, 1, 1);
        SpeechToText.instance.Setting(langCode);
    }

    public void Questions()
    {
        StartListening();
    }
}
