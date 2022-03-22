using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RottenTomato : BoardItem_Base
{
    public BoardItemControls inputActions;

    public ProjectileLauncher projectilePrefab;

    public LineRenderer lineRenderer;

    private Scene simulationScene;
    private PhysicsScene physicsScene;

    public float damage = 10f;
    public float effectRadius = 3f;

    //public float forceMultiplier = 1f;
    public float forceIncreaseRate = 1f;
    public float minForce = 1f;
    public float maxForce = 25f;

    private bool isCharging
    {
        get
        {
            return _isCharging;
        }
        set
        {
            _isCharging = value;
            if (_isCharging)
            {
                StartCoroutine(Charge());
            }
        }
    }
    private bool _isCharging;

    private void Awake()
    {
        inputActions = new BoardItemControls();
        InitializeControls();
        TryGetComponent(out lineRenderer);
        isCharging = false;
        CreateSimulationScene();
    }

    public void InitializeControls()
    {
        inputActions.RottenTomato.Charge.performed += _ => isCharging = true;
        inputActions.RottenTomato.Charge.canceled += _ => isCharging = false;
    }

    public IEnumerator Charge(float updateRate = 0.05f, float maxHoldTime = 10f)
    {
        float i = 0;
        float currentForce = minForce;

        while(isCharging && i < maxHoldTime)
        {
            Debug.Log($"Charging... {i}");
            Simulate(currentForce);
            currentForce += forceIncreaseRate;
            currentForce = Mathf.Clamp(currentForce, minForce, maxForce);
            yield return new WaitForSeconds(updateRate);
            i += Time.deltaTime + updateRate;
        }

        lineRenderer.positionCount = 0;

        ProjectileLauncher projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectileInstance.ignore.Add(owner.gameObject);
        projectileInstance.Launch((transform.forward + transform.up).normalized * currentForce, false);

        while (!projectileInstance.hasHit && projectileInstance.lifeTime > 0)
        {
            yield return new WaitForSeconds(updateRate);
        }

        if (projectileInstance.hasHit)
        {
            foreach(Collider c in Physics.OverlapSphere(projectileInstance.hitPoint, effectRadius))
            {
                BoardEntity entity;
                if(c.gameObject.TryGetComponent(out entity)){
                    entity.health -= damage;
                }
            }
        }

        Destroy(projectileInstance.gameObject);
        owner.inventory.EndUsingItem(this);
        SceneManager.UnloadSceneAsync(simulationScene);
        Destroy(gameObject);
        yield return null;
    }

    public void CreateSimulationScene()
    {
        simulationScene = SceneManager.CreateScene("SimulationScene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        physicsScene = simulationScene.GetPhysicsScene();

        foreach (GameObject gO in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (gO.activeInHierarchy && gO.CompareTag("MapStatic"))
            {
                GameObject objInstance = Instantiate(gO, gO.transform.position, gO.transform.rotation);
                Renderer rndr = objInstance.GetComponent<Renderer>();
                if (rndr != null) rndr.enabled = false;
                SceneManager.MoveGameObjectToScene(objInstance, simulationScene);
            }
        }
    }

    public void Simulate(float force, int frameIterations = 150)
    {
        ProjectileLauncher ghostProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        SceneManager.MoveGameObjectToScene(ghostProjectile.gameObject, simulationScene);
        ghostProjectile.ignore.Add(owner.gameObject);
        ghostProjectile.Launch((transform.forward + transform.up).normalized * force, true);

        int i = 0;
        lineRenderer.positionCount = frameIterations;
        while (i < frameIterations)
        {
            if (ghostProjectile.hasHit || ghostProjectile.bounces > 1)
            {
                i = frameIterations;
            } else
            {
                physicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, ghostProjectile.transform.position);
                i++;
            }
        }

        Destroy(ghostProjectile.gameObject);
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