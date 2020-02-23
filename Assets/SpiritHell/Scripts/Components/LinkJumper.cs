using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkJumper : MonoBehaviour
{
    public void GoToLink(string link){
        Application.OpenURL(link);
    }
}
