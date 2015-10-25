using UnityEngine;
using System;
using System.Text;
using System.IO;
using System.Collections;

public class LogMessage : MonoBehaviour
{

    private static LogMessage logMessage = null;
    string path = Application.dataPath + "//log.txt";

    public static LogMessage GetInstance()
    {
        if (logMessage == null)
        {
            logMessage = new LogMessage();
        }
        return logMessage;
    }

    /** 
     * 用于读写可读写空间的文件 
     */
    public string readStreamingFile(string name)
    {
        string data = "";
        FileInfo fi = new FileInfo(path);
        if (!fi.Exists)
        {
            //Application.platform == RuntimePlatform.Android  
            WWW www = new WWW(path);
            while (!www.isDone)
            {
            }
            data = www.text + " ERROR:" + www.error;
        }
        else
        {
            StreamReader sr = null;
            sr = fi.OpenText();
            data = sr.ReadToEnd();
            sr.Close();
            sr.Dispose();
        }
        return data;
    }

    public void WriteFile(string info)
    {
        WriteFile(info, false);
    }

    public void WriteFile(string info, bool overwrite)
    {
        //文件流信息  
        StreamWriter sw;
        FileInfo t = new FileInfo(path);
        if (!t.Exists)
        {
            //如果此文件不存在则创建  
            sw = t.CreateText();
        }
        else
        {
            if (overwrite)
                sw = t.CreateText();
            else
                sw = t.AppendText();
        }
        //以行的形式写入信息  
        sw.WriteLine(info);
        //关闭流  
        sw.Close();
        //销毁流  
        sw.Dispose();
    }

    /**   用于写可读写空间的文件 
    * name：读取文件的名称    
    */
    public string ReadFile(string name)
    {
        //使用流的形式读取  
        StreamReader sr = null;
        sr = File.OpenText(path);
        string data = sr.ReadToEnd();

        //关闭流  
        sr.Close();
        //销毁流  
        sr.Dispose();
        //将数组链表容器返回     
        return data;
    }

    void DeleteFile()
    {
        File.Delete(path);
    }
}
