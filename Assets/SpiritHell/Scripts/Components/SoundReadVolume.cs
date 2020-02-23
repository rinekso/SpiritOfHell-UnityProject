using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReadVolume : MonoBehaviour
{
    public string key;
    // Start is called before the first frame update
    void Start()
    {
        GetVolume();
    }
    public void GetVolume(){
        if(PlayerPrefs.HasKey(key)){
            float val = PlayerPrefs.GetFloat(key);
            GetComponent<AudioSource>().volume = val;
        }else{
            PlayerPrefs.SetFloat(key,1);
            GetComponent<AudioSource>().volume = 1;
        }
    }
}
