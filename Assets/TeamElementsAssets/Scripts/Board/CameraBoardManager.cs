using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineBlenderSettings;

public class CameraBoardManager : MonoBehaviour
{

    public CameraBoardManager singleton;

    [HideInInspector]
    public static CinemachineBlenderSettings cbs;

    public static GameObject topCameraPrefab;
    public static GameObject thirdPersonCameraPrefab;

    private void Awake()
    {
        if(singleton != null)
        {
            Destroy(this);
            return;
        }
        singleton = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private static void CheckIfBlendSettingsAreInitialized()
    {
        if (cbs == null) cbs = ScriptableObject.CreateInstance<CinemachineBlenderSettings>();// new CinemachineBlenderSettings();
    }

    public static void CreateEntityCameras(BoardEntity entity)
    {
        if (topCameraPrefab == null) topCameraPrefab = Resources.Load("Cameras/TopCamera") as GameObject;
        if (thirdPersonCameraPrefab == null) thirdPersonCameraPrefab = Resources.Load("Cameras/ThirdPersonCamera") as GameObject;

        GameObject topCamera = Instantiate(topCameraPrefab);
        if(topCamera.TryGetComponent(out entity.topCamera))
        {
            topCamera.transform.parent = entity.gameObject.transform;
            entity.topCameraController = topCamera.gameObject.GetComponent<TopViewCameraController>();
        } else
        {
            Destroy(topCamera);
        }

        GameObject thirdPersonCamera = Instantiate(thirdPersonCameraPrefab);
        if (thirdPersonCamera.TryGetComponent(out entity.thirdPersonCamera))
        {
            thirdPersonCamera.transform.parent = entity.gameObject.transform;
            //thirdPersonCamera.transform.rotation = Quaternion.Euler(new Vector3(25f, 0f, 0f));
            //entity.thirdPersonCamera.Follow = entity.transform;
            //entity.thirdPersonCamera.LookAt = entity.transform;
        }
        else
        {
            Destroy(thirdPersonCamera);
        }

        CreateCameraBlends(entity.thirdPersonCamera, entity.topCamera);
    }

    public static void CreateCameraBlends(CinemachineVirtualCameraBase cam1, CinemachineVirtualCameraBase cam2)
    {
        CheckIfBlendSettingsAreInitialized();
        List<CustomBlend> newBlends = new List<CustomBlend>();
        if (cbs.m_CustomBlends == null)
        {
            List<CustomBlend> aux = new List<CustomBlend>();
            CustomBlend tempCB = new CustomBlend();
            tempCB.m_From = "**ANY CAMERA**";
            tempCB.m_To = "**ANY CAMERA**";
            CinemachineBlendDefinition blendTypeAux = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, .5f);
            tempCB.m_Blend = blendTypeAux;
            aux.Add(tempCB);
            cbs.m_CustomBlends = aux.ToArray();
        }
        foreach (CustomBlend cB in cbs.m_CustomBlends)
        {
            newBlends.Add(cB);
        }

        CustomBlend blend = new CustomBlend();
        blend.m_From = cam1.Name;
        blend.m_To = cam2.Name;
        CinemachineBlendDefinition blendType = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, .5f);
        blend.m_Blend = blendType;
        newBlends.Add(blend);

        CustomBlend blend2 = new CustomBlend();
        blend2.m_From = cam2.Name;
        blend2.m_To = cam1.Name;
        CinemachineBlendDefinition blendType2 = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, .5f);
        blend2.m_Blend = blendType2;
        newBlends.Add(blend2);

        cbs.m_CustomBlends = newBlends.ToArray();
        UpdateCinemachineBrain();
    }

    public static void UpdateCinemachineBrain()
    {
        CinemachineBrain brain;
        if(!Camera.main.TryGetComponent(out brain))
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }
        brain.m_CustomBlends = cbs;
    }
}
