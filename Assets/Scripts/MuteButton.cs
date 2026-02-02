
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] private GameObject iconSoundOn;   // אייקון 🔊
    [SerializeField] private GameObject iconSoundOff;  // אייקון 🔇

    private const string PrefKey = "SoundOn"; // 1=on, 0=off

    private void Start()
    {
        ApplySavedSetting();
    }

    public void ToggleMute()
    {
        int soundOn = PlayerPrefs.GetInt(PrefKey, 1);
        soundOn = (soundOn == 1) ? 0 : 1;

        PlayerPrefs.SetInt(PrefKey, soundOn);
        PlayerPrefs.Save();

        ApplySavedSetting();
    }

    private void ApplySavedSetting()
    {
        bool isOn = PlayerPrefs.GetInt(PrefKey, 1) == 1;
        AudioListener.volume = isOn ? 1f : 0f;

        if (iconSoundOn != null) iconSoundOn.SetActive(isOn);
        if (iconSoundOff != null) iconSoundOff.SetActive(!isOn);
    }
}
