using UnityEditor;
using System.IO;

public static class CreateEnum
{
    private const string Extension = ".cs";

    public static void Go(string EnumName,string[] enumEntries,string FilePath = "Assets/Scripts/Enums/")
    {
    string FilePathAndName = FilePath + EnumName + Extension;

        using (StreamWriter streamWriter = new StreamWriter(FilePathAndName))
        {
            streamWriter.WriteLine("public enum " + EnumName);
            streamWriter.WriteLine("{");
            for (int i = 0; i < enumEntries.Length; i++)
            {
                string entry = enumEntries[i];
                entry = entry.Replace(" ", "_");
                if (!string.IsNullOrEmpty(entry))
                    streamWriter.WriteLine("\t" + entry + " = " + i + ",");
            }
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
