using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    int currentLevel;
    int currentStar = 0;
    public TMPro.TextMeshProUGUI LevelNameText;
    public GameObject CurrentPlayer;
    public GameObject DieParticle;
    public GameObject PanelFinish;
    public GameObject PanelDie;
    public GameObject PanelPause;
    private void Start() {
        currentStar = 0;
        checkHighestLevel();
        DisableAllPanel();
        LevelNameText.text = "Level "+SceneManager.GetActiveScene().name;
    }
    void checkHighestLevel(){
        int levelThru = PlayerPrefs.GetInt("highest level");
        string name = SceneManager.GetActiveScene().name;
        currentLevel = int.Parse(name);
        PlayerPrefs.SetInt("last level",currentLevel);
        if(currentLevel>levelThru){
            PlayerPrefs.SetInt("highest level",currentLevel);
        }
    }
    public void AddStar(){
        currentStar++;
    }
    public void GameOver(){
        StartCoroutine(PlayerDie(CurrentPlayer));
    }
    IEnumerator PlayerDie(GameObject player){
        Camera.main.gameObject.GetComponentInParent<CameraControll>().shakeDuration = .5f;
        player.GetComponent<Animator>().SetBool("off",true);
        player.GetComponent<PlayerScript>().play = false;
        Instantiate(DieParticle,player.transform.position,new Quaternion());

        GameObject.FindGameObjectWithTag("lava").GetComponent<LavaMove>().StopMove();

        yield return new WaitForSeconds(2);
        player.SetActive(false);
        PanelDie.SetActive(true);
        
        int diecount = PlayerPrefs.GetInt("die");
        PlayerPrefs.SetInt("die",diecount+1);
        if(diecount > 1){
            PlayerPrefs.SetInt("die",0);
            // GetComponent<AdController>().ShowRewardedAd();
        }
        
        yield return true;
    }
    public void NextLevel(){
        string name = SceneManager.GetActiveScene().name;
        int num = int.Parse(name);
        SceneManager.LoadScene(num+1);
    }
    public void Restart(){
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }
    public void Finish(GameObject player, GameObject ItemFinish){
        if(SceneManager.GetSceneByName((currentLevel+1)+"").IsValid()){
            PlayerPrefs.SetInt("last level",currentLevel+1);

            int levelThru = PlayerPrefs.GetInt("highest level");
            if(currentLevel+1>levelThru){
                PlayerPrefs.SetInt("highest level",currentLevel);
            }
        }

        int stars = PlayerPrefs.GetInt("stars level"+currentLevel);
        if(stars<currentStar){
            PlayerPrefs.SetInt("stars level"+currentLevel,currentStar);
        }

        StartCoroutine(FinishFlow(player,ItemFinish));
    }
    void DisableAllPanel(){
        PanelPause.SetActive(false);
        PanelFinish.SetActive(false);
        PanelDie.SetActive(false);
    }
    public void Pause(){
        PanelPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue(){
        PanelPause.SetActive(false);
        Time.timeScale = 1;
    }
    IEnumerator FinishFlow(GameObject player, GameObject ItemFinish){
        Camera.main.gameObject.GetComponentInParent<CameraControll>().shakeDuration = 2.8f;
        GameObject.FindGameObjectWithTag("lava").GetComponent<LavaMove>().StopMove();
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Animator>().SetBool("off",true);
        ItemFinish.GetComponent<Animator>().SetBool("off",true);
        yield return new WaitForSeconds(2.5f);
        PanelFinish.SetActive(true);
        yield return true;
    }
}
