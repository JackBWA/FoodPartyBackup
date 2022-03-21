using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RottenTomato : MonoBehaviour
{
    public BoardItemControls inputActions;

    private Scene simulationScene;
    private PhysicsScene physicsScene;

    private void Awake()
    {
        inputActions = new BoardItemControls();
    }

    public void CreateSimulationScene()
    {
        simulationScene = SceneManager.CreateScene("SimulationScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
}
