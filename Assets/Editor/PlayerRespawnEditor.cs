using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerRespawn))]
public class PlayerRespawnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        PlayerRespawn playerRespawn = (PlayerRespawn)target;
        
        // 버튼 추가
        if (GUILayout.Button("주인공 리스폰"))
        {
            playerRespawn.BeginRespawn();
        }
    }
}
