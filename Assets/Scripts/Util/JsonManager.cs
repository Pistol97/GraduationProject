using UnityEngine;
using System.IO;
using System.Text;

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
        //JSON으로 변환하여 반환
        return JsonUtility.ToJson(obj, true);
    }

    /// <summary>
    /// 플레이어 데이터 파일을 생성하는 메소드
    /// </summary>
    /// <param name="createPath">생성 경로</param>
    /// <param name="fileName">파일 이름</param>
    /// <param name="jsonData">변환한 JSON 데이터</param>
    public void CreateJsonFile(string createPath, string fileName, string jsonData)
    {
        //해당 경로에 새 파일 생성
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        
        try
        {
            Debug.Log("Create File Success");

            //유니코드 문자열을 바이트 시퀀스로 인코딩
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            //파일 작성
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
        //파일 생성 실패
        catch (System.NullReferenceException e)
        {
            Debug.Log("Create File Fail: " + e);
        }
    }

    /// <summary>
    /// JSON 파일을 불러오는 메소드
    /// </summary>
    /// <typeparam name="T">JSON 데이터 클래스(제네릭)</typeparam>
    /// <param name="loadPath">JSON 불러오기 경로</param>
    /// <param name="fileName">파일명 ex) *.json </param>
    /// <returns></returns>
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        try
        {
            //해당하는 경로의 파일을 불러온다
            FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);

            //파일 길이의 바이트 시퀀스 생성
            byte[] data = new byte[fileStream.Length];
            //바이트 시퀀스에 파일 내용 불러옴
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();

            //바이트 시퀀스를 유니코드 문자열로 디코딩
            string jsonData = Encoding.UTF8.GetString(data);

            //제네릭 형식으로 불러온 데이터 반환
            return JsonUtility.FromJson<T>(jsonData);
        }

        //파일 불러오기 실패
        catch (FileNotFoundException e)
        {
            Debug.Log("File Not Found" + e);

            return default;
        }
    }
}
