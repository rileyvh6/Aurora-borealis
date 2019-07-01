using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
//Made using the answer on stackoverflow here: https://answers.unity.com/questions/393992/custom-inspector-multi-select-enum-dropdown.html
public static class EditorExtension
{
    public static int DrawBitMaskField (Rect aPosition,int aMask, Type aType,GUIContent aLabel)
    {
        string[] itemNames = Enum.GetNames(aType);
        int[] itemValues = Enum.GetValues(aType) as int[];

        int val = aMask;
        int maskVal = 0;

        for (int i = 0;i < itemValues.Length;i++)
        {
            if (itemValues[i] != 0)
                if ((val & itemValues[i]) == itemValues[i])
                {
                    maskVal |= 1 << i;
                }
                else if (val == 0)
                {
                    maskVal |= 1 << i;
                }
        }
        int newMaskVal = EditorGUI.MaskField(aPosition, aLabel, maskVal, itemNames);
        int changes = maskVal ^ newMaskVal;

        for (int i=0;i < itemValues.Length;i++)
        {
            if ((changes & (1 << i)) != 0) //has this list item been changed?
            {
                if ((newMaskVal & (1 << i)) != 0) //has it been set?
                {

                    if (itemValues[i] == 0) //Special case: if '0' has been set, set the value to 0
                    {
                        val = 0;
                        break;
                    }
                    val |= itemValues[i];
                }
                else //it has been reset
                    val &= ~itemValues[i];
            }
        }
        if (val == 0)
            val = 1;
        return val;
    }
}

[CustomPropertyDrawer(typeof(BitMaskAttribute))]
public class EnumBitmaskPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        BitMaskAttribute typeAttr = attribute as BitMaskAttribute;
        // add the actual int value behind the field name
        label.text = label.text + "(" + property.intValue + ")";
        property.intValue = EditorExtension.DrawBitMaskField(position, property.intValue, fieldInfo.FieldType, label);
        }
}
