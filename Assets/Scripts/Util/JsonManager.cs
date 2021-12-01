using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Json에 관한 기능을 담당하는 유틸용 클래스
/// Singleton Pattern
/// </summary>
public class JsonManager
{
    private static JsonManager _instance = null;

    public static JsonManager Instance
    {
        get
        {
            if(null != _instance)
            {
                return null;
            }

            return _instance;
        }
    }

    private JsonManager()
    {
        if (null != _instance)
        {
            _instance = this;
        }

        else if (this != _instance)
        {

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
    /// <param name="jsonData">변환한 jsonData</param>
    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        if (null != fileStream)
        {
            Debug.Log("Create File Success");
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
        else
        {
            Debug.Log("Create File Fail!");
            return;
        }
    }

    /// <summary>
    /// 플레이어 데이터 파일을 불러오는 메소드
    /// 주로 플레이어 데이터 클래스 형식을 받으나 추후 다른 클래스의 형태로 사용가능
    /// </summary>
    /// <typeparam name="T">데이터 클래스 자료형</typeparam>
    /// <param name="loadPath">데이터 불러오기 경로</param>
    /// <param name="fileName">파일명 ex) *.json </param>
    /// <returns></returns>
    private T LoadJsonFile<T>(string loadPath, string fileName, object datas)
    {
        //파일 검사
        //파일이 존재하지 않을 시 새로 생성
        if (!File.Exists(loadPath + "/" + fileName + ".json"))
        {
            Debug.Log("File Not Found");
            string json = ObjectToJson(datas);
            CreateJsonFile(Application.dataPath, fileName, json);
        }

        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(jsonData);
    }

    /// <summary>
    /// 플레이어 데이터 저장 메소드
    /// </summary>
    public void SaveData(object datas, string fileName)
    {
        string json = ObjectToJson(datas);
        CreateJsonFile(Application.dataPath, fileName, json);
    }
}
