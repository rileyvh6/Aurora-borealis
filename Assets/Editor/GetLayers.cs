using UnityEngine;
using UnityEditor;
[InitializeOnLoad]
public static class GetLayers
{
    public static string[] AllLayers = new string[31];

    static GetLayers()
    {
            GrabLayers();
        CreateEnum.Go("Layers", AllLayers);

    }

    private static void GrabLayers()
    {
        for (int i = 0; i < 31; i++)
            AllLayers[i] = LayerMask.LayerToName(i);
    }
}
