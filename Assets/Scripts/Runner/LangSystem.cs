using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Text;

public class LangSystem : MonoBehaviour
{
    public Image langButttonImage;
    public Sprite[] flags;
    private string[] langArray = { "ru_RU", "en_EN" };
    private string json;
    public static lang lng = new lang();
    private int langIndex = 1;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                PlayerPrefs.SetString("Language", "ru_RU");
            }
            else
            {
                PlayerPrefs.SetString("Language", "en_EN");
            }
            langLoad();
        }
    }
    private void Start()
    {
        for (int i = 0; i < langArray.Length; i++)
        {
            if (PlayerPrefs.GetString("Language") == langArray[i])
            {
                langIndex = i + 1;
                langButttonImage.sprite = flags[i];
                break;
            }
        }
    }

    public void langLoad()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        string path =Path.Combine(Application.streamingAssetsPath,"Language/" + PlayerPrefs.GetString("Language") + ".json");
        WWW reader = new WWW(path);
        while (!reader.isDone) { }
        json = reader.text;
#endif
#if UNITY_EDITOR
        json = File.ReadAllText(Application.streamingAssetsPath + "/Language/" + PlayerPrefs.GetString("Language") + ".json");        
        lng = JsonUtility.FromJson<lang>(json);
#endif
    }
    public void SwitchButton()
    {
        if (langIndex != langArray.Length)
        {
            langIndex++;
        }
        else
        {
            langIndex = 1;
        }
        langButttonImage.sprite = flags[langIndex - 1];
        PlayerPrefs.SetString("Language", langArray[langIndex - 1]);
        langLoad();
    }
}
public class lang
{
    public string Menu;
    public string TapToPlay;
    public string RatingForFreePizzaInStartPanel;
    public string RatingInMenuCount;
}
