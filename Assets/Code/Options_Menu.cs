using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options_Menu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public Toggle allSoundsOffButton;
    public Dropdown qualityComboBox;

    private void Start()
    {
        audioMixer.GetFloat("volume", out float volume);
        volumeSlider.value = volume;
        if (AudioListener.pause == true)
        {
            allSoundsOffButton.isOn = true;
        }
        else if (AudioListener.pause == false)
        {
            allSoundsOffButton.isOn = false;
        }
        qualityComboBox.value = QualitySettings.GetQualityLevel();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void Sound()
    {
        if (allSoundsOffButton.isOn == false)
        {
            AudioListener.pause = false;
        }
        else if (allSoundsOffButton.isOn == true)
        {
            AudioListener.pause = true;
        }
    }
}
