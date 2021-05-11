using UnityEngine;
using UnityEditor;

namespace BetterGridLayout
{
	[CustomEditor(typeof(BetterGridLayout))]
	public class BetterGridLayoutEditor : Editor
	{
		SerializedProperty _padding;
		SerializedProperty _spacing;
		SerializedProperty _childAlignment;

		SerializedProperty _alignment;
		SerializedProperty _fitType;

		SerializedProperty _autoFitX;
		SerializedProperty _autoFitY;

		SerializedProperty _flipX;
		SerializedProperty _flipY;

		SerializedProperty _rows;
		SerializedProperty _columns;
		SerializedProperty _cellSizeX;
		SerializedProperty _cellSizeY;

		SerializedProperty _freezeLayout;

		protected virtual void OnEnable()
		{
			_padding = serializedObject.FindProperty("m_Padding");
			_spacing = serializedObject.FindProperty("spacing");
			_childAlignment = serializedObject.FindProperty("m_ChildAlignment");

			_fitType = serializedObject.FindProperty("fitType");
			_alignment = serializedObject.FindProperty("alignment");

			_autoFitX = serializedObject.FindProperty("autoFitX");
			_autoFitY = serializedObject.FindProperty("autoFitY");

			_flipX = serializedObject.FindProperty("flipX");
			_flipY = serializedObject.FindProperty("flipY");

			_rows = serializedObject.FindProperty("rows");
			_columns = serializedObject.FindProperty("columns");
			_cellSizeX = serializedObject.FindProperty("cellSizeX");
			_cellSizeY = serializedObject.FindProperty("cellSizeY");

			_freezeLayout = serializedObject.FindProperty("freezeLayout");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			// 以下はそのままインスペクタに描画
			EditorGUILayout.PropertyField(_freezeLayout, true);
			EditorGUILayout.PropertyField(_padding, true);
			EditorGUILayout.PropertyField(_spacing, true);
			//EditorGUILayout.PropertyField(_childAlignment, true);
			EditorGUILayout.PropertyField(_fitType, true);
			EditorGUILayout.PropertyField(_alignment, true);

			// Flip設定
			GUI.enabled = true;
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Flip Axis");
				var labelWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 10f;
				EditorGUILayout.PropertyField(_flipX, new GUIContent("X"), GUILayout.Width(40f));
				EditorGUILayout.PropertyField(_flipY, new GUIContent("Y"), GUILayout.Width(40f));
				EditorGUIUtility.labelWidth = labelWidth;
			}
			EditorGUILayout.EndHorizontal();

			// AutoFit設定
			GUI.enabled = _fitType.intValue == (int)BetterGridLayout.EFitType.FixedRows || _fitType.intValue == (int)BetterGridLayout.EFitType.FixedColumns;
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Auto Fit");
				var labelWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 10f;
				EditorGUILayout.PropertyField(_autoFitX, new GUIContent("X"), GUILayout.Width(40f));
				EditorGUILayout.PropertyField(_autoFitY, new GUIContent("Y"), GUILayout.Width(40f));
				EditorGUIUtility.labelWidth = labelWidth;
			}
			EditorGUILayout.EndHorizontal();

			// rowの設定
			GUI.enabled = _fitType.intValue == (int)BetterGridLayout.EFitType.FixedRows || _fitType.intValue == (int)BetterGridLayout.EFitType.RowPriority;
			EditorGUILayout.PropertyField(_rows);

			// columnの設定
			GUI.enabled = _fitType.intValue == (int)BetterGridLayout.EFitType.FixedColumns || _fitType.intValue == (int)BetterGridLayout.EFitType.ColumnPriority;
			EditorGUILayout.PropertyField(_columns);

			// セルサイズの設定
			EditorGUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Cell Size");
				var labelWidth = EditorGUIUtility.labelWidth;
				EditorGUIUtility.labelWidth = 10f;
				GUI.enabled = !_autoFitX.boolValue;
				EditorGUILayout.PropertyField(_cellSizeX, new GUIContent("X"), GUILayout.Width(80f));
				GUI.enabled = !_autoFitY.boolValue;
				EditorGUILayout.PropertyField(_cellSizeY, new GUIContent("Y"), GUILayout.Width(80f));
				EditorGUIUtility.labelWidth = labelWidth;
			}
			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();
		}
	}
}