using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Json에 관한 기능을 담당하는 유틸 클래스
/// Singleton Pattern
/// </summary>
public class JsonManager
{
    private static JsonManager _instance = null;

    public static JsonManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new JsonManager();
            }

            return _instance;
        }
    }

    /// <summary>
    /// 객체를 JSON화
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string ObjectToJson(object obj)
    {
        return JsonUtility.ToJson(obj, true);
    }

    /// <summary>
    /// 플레이어 데이터 파일을 생성하는 메소드
    /// </summary>
    /// <param name="createPath">생성 경로</param>
    /// <param name="fileName">파일 이름</param>
    /// <param name="jsonData">변환한 JsonData</param>
    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        
        try
        {
            Debug.Log("Create File Success");
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
        //파일 생성 실패 예외 처리
        catch (System.NullReferenceException e)
        {
            Debug.Log("Create File Fail: " + e);
        }
    }

    /// <summary>
    /// 데이터 파일을 불러오는 메소드
    /// </summary>
    /// <typeparam name="T">데이터 클래스 자료형</typeparam>
    /// <param name="loadPath">데이터 불러오기 경로</param>
    /// <param name="fileName">파일명 ex) *.json </param>
    /// <returns></returns>
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        try
        {
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            return JsonUtility.FromJson<T>(jsonData);
        }
        catch (FileNotFoundException e)
        {
            Debug.Log("File Not Found" + e);

            return default;
        }
    }
}
