using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    static GameObject gameManager;
    DependenciesInitializer dependenciesInitializer = new DependenciesInitializer();

    private void Awake() {

        if (gameManager) {
            Destroy(gameObject);
            return;
        } else {
            gameManager = gameObject;
            dependenciesInitializer.Initialize();
            DontDestroyOnLoad(gameObject);
        }
    }
}