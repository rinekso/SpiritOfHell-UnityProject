using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject LevelItem;
    public int LevelMax;
    public Color filled;
    public Color unfilled;
    // Start is called before the first frame update
    void Start()
    {
        int levelThru = PlayerPrefs.GetInt("highest level");
        for(int i = 0; i < LevelMax; i++){
            GameObject item = Instantiate(LevelItem,transform);
            item.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i+1)+"";

            if(i < levelThru){
                item.GetComponent<Button>().interactable = true;
            }else{
                item.GetComponent<Button>().interactable = false;
            }

            int stars = PlayerPrefs.GetInt("stars level"+(i+1));
            readStar(stars, item);

            Button btn = item.GetComponent<Button>();
            int lvl = i+1;
            btn.onClick.AddListener(delegate{JumpScene(""+lvl);});

        }
    }
    void readStar(int starsCount, GameObject item){
        Debug.Log(starsCount);
        for(int i = 0;i < starsCount;i++){
            item.GetComponent<LevelItem>().star[i].color = filled;
        }
    }
    void JumpScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
