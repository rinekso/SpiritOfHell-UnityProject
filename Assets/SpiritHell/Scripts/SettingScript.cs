using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    public Slider sfxSlider;
    public Slider bgmSlider;

    void Awake()
    {
        GetVolume();
    }
    public void GetVolume(){
        if(PlayerPrefs.HasKey("sfx")){
            float val = PlayerPrefs.GetFloat("sfx");
            sfxSlider.value = val;
        }else{
            PlayerPrefs.SetFloat("sfx",1);
            sfxSlider.value = 1;
        }

        if(PlayerPrefs.HasKey("bgm")){
            float val = PlayerPrefs.GetFloat("bgm");
            bgmSlider.value = val;
        }else{
            PlayerPrefs.SetFloat("bgm",1);
            sfxSlider.value = 1;
        }
    }
    public void ResumePlay(){
        int level = 1;
        if(PlayerPrefs.HasKey("last level")){
            level = PlayerPrefs.GetInt("last level");
        }
        SceneManager.LoadScene(level);
    }
    public void SoundChange(bool sfx){
        if(sfx){
            float val = GameObject.FindGameObjectWithTag("sfx").GetComponent<Slider>().value;
            PlayerPrefs.SetFloat("sfx",val);
        }else{
            float val = GameObject.FindGameObjectWithTag("bgm").GetComponent<Slider>().value;
            GetComponent<AudioSource>().volume = val;
            PlayerPrefs.SetFloat("bgm",val);
        }

    }
}
