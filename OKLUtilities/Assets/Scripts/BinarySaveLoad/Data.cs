using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Data<T> where T : new()
{
    public static void Save(string path, T data)
    {
        BinaryFormatter bin = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        bin.Serialize(stream, data);
        stream.Close();
    }

    public static T Load(string path)
    {
        if (File.Exists(path))
        {
            BinaryFormatter bin = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            T data = (T)bin.Deserialize(stream);
            stream.Close();

            return data;
        }
        return default(T);
    }

    public static bool Exists(string path)
    {
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void Delete(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}

