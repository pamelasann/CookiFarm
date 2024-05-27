using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour{
    public string sceneToLoad;
    public void OnTriggerEnter2D(Collider2D other){
        // Debugging log to see when the collision happens
        Debug.Log("OnTriggerEnter2D triggered");
        if(other.CompareTag("Player") && !other.isTrigger){
            // Debugging log to verify condition
            Debug.Log("Player entered the trigger");
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
