using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AudioEvent),true)]
public class AudioEventEditor : Editor
{
   [SerializeField] private AudioSource _previewer;

	public void OnEnable()
	{
		_previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
	}

	public void OnDisable()
	{
		DestroyImmediate(_previewer.gameObject);
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
		if (GUILayout.Button("Preview"))
		{
			PlayPreview();
		}
		if (GUILayout.Button("Stop"))
		{
			StopPreview();
		}
		EditorGUI.EndDisabledGroup();
	}

	private void PlayPreview()
	{
		((AudioEvent) target).Play(_previewer);
	}

	private void StopPreview()
	{
		_previewer.Stop();
	}
}
