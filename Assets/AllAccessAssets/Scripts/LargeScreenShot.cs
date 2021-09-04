using UnityEngine;
using System.Collections;
using System.IO;

public class LargeScreenShot : MonoBehaviour
{

    private Texture2D ScreenShot;
    private RenderTexture rt;
    int res = 2560;
    int res2 = 1440;

    // Use this for initialization
    void Start()
    {
        rt = new RenderTexture(res, res2, 32);
        ScreenShot = new Texture2D(res, res2,TextureFormat.RGBA32,false);
        Camera.main.targetTexture = rt;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Camera.main.Render();
            RenderTexture.active = rt;
            ScreenShot.ReadPixels(new Rect(0, 0, res, res2), 0, 0);
            ScreenShot.Apply();

            byte[] bytes = ScreenShot.EncodeToPNG();
            Destroy(ScreenShot);
            File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
        }
    }

    IEnumerator take()
    {
        yield return new WaitForEndOfFrame();
       
    }
}
