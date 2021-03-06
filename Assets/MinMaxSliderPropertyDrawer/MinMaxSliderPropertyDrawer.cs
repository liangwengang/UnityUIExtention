﻿// (C) 2015 ERAL
// Distributed under the Boost Software License, Version 1.0.
// (See copy at http://www.boost.org/LICENSE_1_0.txt)

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class MinMaxSliderPropertyDrawer : PropertyDrawer {

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		var minMaxSliderAttribute = (MinMaxSliderAttribute)attribute;

		if (SerializedPropertyType.Vector2 == property.propertyType) {
			using (new EditorGUI.PropertyScope(position, label, property)) {
				const float kSpacing = 5.0f;

				var value = property.vector2Value;
				EditorGUI.BeginChangeCheck();

				var prefixLabelPosition = position;
				prefixLabelPosition.width = EditorGUIUtility.labelWidth;
				EditorGUI.LabelField(prefixLabelPosition, label);
				position.xMin += prefixLabelPosition.width;

				var minFloatFieldPosition = position;
				minFloatFieldPosition.width = EditorGUIUtility.fieldWidth;
				value.x = EditorGUI.FloatField(minFloatFieldPosition, value.x);
				position.xMin += minFloatFieldPosition.width + kSpacing;

				var minMaxSliderPosition = position;
				minMaxSliderPosition.width -= EditorGUIUtility.fieldWidth + kSpacing;
				EditorGUI.MinMaxSlider(minMaxSliderPosition, ref value.x, ref value.y, minMaxSliderAttribute.min, minMaxSliderAttribute.max);
				position.xMin += minMaxSliderPosition.width + kSpacing;

				var maxFloatFieldPosition = position;
				maxFloatFieldPosition.width = EditorGUIUtility.fieldWidth;
				value.y = EditorGUI.FloatField(maxFloatFieldPosition, value.y);
				position.xMin += maxFloatFieldPosition.width + kSpacing;

				if (EditorGUI.EndChangeCheck()) {
					property.vector2Value = value;
				}
			}
		} else {
			EditorGUI.LabelField(position, label, new GUIContent("Use MinMaxSlider with Vector2."));
		}
	}
}
#endif

public class MinMaxSliderAttribute : PropertyAttribute {
	public readonly float min;
	public readonly float max;

	public MinMaxSliderAttribute(float min, float max) {
		this.min = min;
		this.max = max;
	}
}
