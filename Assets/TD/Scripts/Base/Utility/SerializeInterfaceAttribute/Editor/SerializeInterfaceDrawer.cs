using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
 
[CustomPropertyDrawer(typeof(SerializeInterfaceAttribute))]
public class SerializeInterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializeInterfaceAttribute serializedInterface = attribute as SerializeInterfaceAttribute;
        Type serializedType = serializedInterface.SerializedType;
 
        if (IsValid(property, serializedType))
        {
            label.tooltip = "Serialize " + serializedInterface.SerializedType.Name + " interface";
            CheckProperty(property, serializedType);
 
            if (position.Contains(Event.current.mousePosition) == true)
            {
                if (DragAndDrop.objectReferences.Length > 0)
                {
                    if (TryGetInterfaceFromObject(DragAndDrop.objectReferences[0], serializedType) == null)
                        DragAndDrop.visualMode = DragAndDropVisualMode.Rejected;
                }
            }
        }
 
        label.text += $" ({serializedType.Name})";
        EditorGUI.PropertyField(position, property, label);
    }
 
    private Object TryGetInterfaceFromObject(Object targetObject, Type serializedType)
    {
        if (serializedType.IsInstanceOfType(targetObject) == false)
        {
            if (targetObject is Component)
                return (targetObject as Component).GetComponent(serializedType);
            else if (targetObject is GameObject)
                return (targetObject as GameObject).GetComponent(serializedType);
        }
 
        return targetObject;
    }
 
    private bool IsValid(SerializedProperty property, Type targetType)
    {
        return targetType.IsInterface && property.propertyType == SerializedPropertyType.ObjectReference;
    }
 
    private void CheckProperty(SerializedProperty property, Type targetType)
    {
        if (property.objectReferenceValue == null)
            return;
 
        property.objectReferenceValue = TryGetInterfaceFromObject(property.objectReferenceValue, targetType);
    }
}