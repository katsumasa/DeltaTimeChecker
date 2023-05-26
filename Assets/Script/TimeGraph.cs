using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class TimeGraph : MonoBehaviour
{
    [SerializeField] Texture texture;

    List<float> deltaTimes;
    List<float> realtimeSinceStartups;
    List<float> smoothDeltaTimes;
    List<float> effectiveRenderFrameRates;
    List<float> willCurrentFrameRenders;
    bool toggleDeltaTimes;
    bool toggleRealtimeSinceStartup;
    bool toggleSmoothDeltaTime;
    bool toggleEffectiveRenderFrameRate;
    bool toggleWillCurrentFrameRender;
    float realtimeSinceStartupLast;
    int warmUpCount;

    int targetFrameRateIdx;

    static string[] VsyncCounts =
    {
        "Don`t sync",
        "Every Sync",
        "Even Sync"
    };

    static string[] targetFrameRates =
    {
        "-1",
        "30",
        "45",
        "60",
        "90",
        "120",
    };


    int GetTargetFrameIdx(int targetFrameRate)
    {
        switch (targetFrameRate)
        {
            case -1:return  0;                
            case 30:return 1;                
            case 45:return 2;
            case 60:return 3;
            case 90:return 4;
            case 120:return 5;
            default: return 0;
        }
    }

    int GetTargetFrameRate(int idx)
    {
        switch (idx)
        {
            case 0: return -1;
            case 1: return 30;
            case 2: return 45;
            case 3: return 60;
            case 4: return 90;
            case 5: return 120;
            default: return -1;                
        }
    }

    static class Styles
    {
        public static GUIContent deltaTime = new GUIContent("deltaTime:[ms]");
        public static GUIContent realtimeSinceStartup = new GUIContent("realtimeSinceStartup  - Last realtimeSinceStartup :[ms]");
        public static GUIContent smoothDeltaTime = new GUIContent("smoothDeltaTime:[ms]");
        public static GUIContent effectiveRenderFrameRate = new GUIContent("OnDemandRendering.effectiveRenderFrameRate");
        public static GUIContent willCurrentFrameRender = new GUIContent("willCurrentFrameRender:");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Screen.SetResolution(480,800,true);
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        warmUpCount = 10;
        realtimeSinceStartupLast = Time.realtimeSinceStartup;
        toggleDeltaTimes = true;
        toggleRealtimeSinceStartup = true;
        toggleSmoothDeltaTime = true;
        toggleEffectiveRenderFrameRate = true;
        toggleWillCurrentFrameRender = true;
    }


    private void OnEnable()
    {
        deltaTimes = new List<float>();
        realtimeSinceStartups = new List<float>();
        smoothDeltaTimes = new List<float>();
        effectiveRenderFrameRates = new List<float>();
        willCurrentFrameRenders = new List<float>();
    }


    // Update is called once per frame
    void Update()
    {
        var realTime = Time.realtimeSinceStartup;

        if (warmUpCount > 0)
        {
            warmUpCount--;
        }
        else
        {
            deltaTimes.Add(Time.deltaTime * 1000.0f);
            if (deltaTimes.Count > 400)
            {
                deltaTimes.RemoveAt(0);
            }            
           Styles.deltaTime = new GUIContent(GraphLayout.Format("deltaTime: {0,3:F3}[ms]  {1:F1}[FPS]", deltaTimes[deltaTimes.Count - 1],1000.0f/ deltaTimes[deltaTimes.Count - 1]));

            realtimeSinceStartups.Add((realTime - realtimeSinceStartupLast) * 1000);
            if (realtimeSinceStartups.Count > 400)
            {
                realtimeSinceStartups.RemoveAt(0);
            }
            Styles.realtimeSinceStartup = new GUIContent(GraphLayout.Format("realtimeSinceStartup  - Last realtimeSinceStartup: {0,3:F1}[ms]", realtimeSinceStartups[realtimeSinceStartups.Count - 1]));

            smoothDeltaTimes.Add(Time.smoothDeltaTime * 1000.0f);
            if(smoothDeltaTimes.Count > 400)
            {
                smoothDeltaTimes.RemoveAt(0);
            }
            Styles.smoothDeltaTime = new GUIContent(GraphLayout.Format("smoothDeltaTime: {0,3:F1}[ms]", smoothDeltaTimes[deltaTimes.Count - 1]));

            effectiveRenderFrameRates.Add(OnDemandRendering.effectiveRenderFrameRate);
            if(effectiveRenderFrameRates.Count >400)
            {
                effectiveRenderFrameRates.RemoveAt(0);
            }
            Styles.effectiveRenderFrameRate = new GUIContent(GraphLayout.Format("OnDemandRendering.effectiveRenderFrameRate:{0}[FPS]",OnDemandRendering.effectiveRenderFrameRate));

            willCurrentFrameRenders.Add(OnDemandRendering.willCurrentFrameRender ? 1.0f : 0f);
            if(willCurrentFrameRenders.Count > 400)
            {
                willCurrentFrameRenders.RemoveAt(0);
            }
            Styles.willCurrentFrameRender = new GUIContent(GraphLayout.Format("willCurrentFrameRenders:{0}", OnDemandRendering.willCurrentFrameRender ? "true" : "false"));
        }

        realtimeSinceStartupLast = realTime;
    }

    private void OnGUI()
    {
        var rect = new Rect(40, 10, 16, 16);
        toggleDeltaTimes = GUI.Toggle(rect, toggleDeltaTimes, "");
        rect = new Rect(40, 10, 400, 100);        
        GraphLayout.FloatField(Styles.deltaTime, deltaTimes, texture, Color.yellow, rect,toggleDeltaTimes);

        rect = new Rect(40, 120, 16,16);
        toggleRealtimeSinceStartup = GUI.Toggle(rect, toggleRealtimeSinceStartup, "");
        rect = new Rect(40, 120, 400, 100);
        GraphLayout.FloatField(Styles.realtimeSinceStartup, realtimeSinceStartups, texture, Color.green, rect,toggleRealtimeSinceStartup);


        rect = new Rect(40, 230, 16, 16);
        toggleSmoothDeltaTime = GUI.Toggle(rect, toggleSmoothDeltaTime, "");
        rect = new Rect(40, 230, 400, 100);
        GraphLayout.FloatField(Styles.smoothDeltaTime, smoothDeltaTimes, texture, Color.cyan, rect,toggleSmoothDeltaTime);

        rect = new Rect(40, 340, 16, 16);
        toggleSmoothDeltaTime = GUI.Toggle(rect, toggleEffectiveRenderFrameRate, "");
        rect = new Rect(40, 340, 400, 100);
        GraphLayout.FloatField(Styles.effectiveRenderFrameRate, effectiveRenderFrameRates, texture, Color.magenta, rect, toggleEffectiveRenderFrameRate);


        rect = new Rect(40, 450, 16, 16);
        toggleWillCurrentFrameRender = GUI.Toggle(rect,toggleWillCurrentFrameRender,"");
        rect = new Rect(40, 450, 400, 100);
        GraphLayout.FloatField(Styles.willCurrentFrameRender, willCurrentFrameRenders, texture, Color.blue, rect, toggleEffectiveRenderFrameRate);

        GUILayoutUtility.GetRect(520, 560);
        GUILayout.Label("Screen.currentResolution.refreshRate " + Screen.currentResolution.refreshRate + "[Hz]");        
        GUILayout.Label("QualitySettings.vSyncCount " + QualitySettings.vSyncCount);
        var vSyncCount = GUILayout.Toolbar(QualitySettings.vSyncCount, VsyncCounts);        
        if(QualitySettings.vSyncCount != vSyncCount)
        {
            QualitySettings.vSyncCount = vSyncCount;
        }        
        GUILayout.Label("Application.targetFrameRate " + Application.targetFrameRate + "[FPS]");        
        var idx = GetTargetFrameIdx(Application.targetFrameRate);
        idx = GUILayout.Toolbar(idx, targetFrameRates);
        var targetFrameRate = GetTargetFrameRate(idx);
        if (targetFrameRate != Application.targetFrameRate)
        {
            Application.targetFrameRate = targetFrameRate;
        }

#if UNITY_2019_3_OR_NEWER
        GUILayout.Label("OnDemandRendering.effectiveRenderFrameRate " + OnDemandRendering.effectiveRenderFrameRate + "[FPS]");
        GUILayout.BeginHorizontal();
        GUILayout.Label("OnDemandRendering.renderFrameInterval " + OnDemandRendering.renderFrameInterval);
        idx = OnDemandRendering.renderFrameInterval;
        idx = (int)GUILayout.HorizontalSlider(idx, 0f, 20);
        if(OnDemandRendering.renderFrameInterval != idx)
        {
            OnDemandRendering.renderFrameInterval = idx;
        }
        GUILayout.EndHorizontal();        
#endif
        if (GUILayout.Button("Reset"))
        {
            deltaTimes.Clear();
            realtimeSinceStartups.Clear();
            smoothDeltaTimes.Clear();
            effectiveRenderFrameRates.Clear();
            willCurrentFrameRenders.Clear();
        }

    }
}
