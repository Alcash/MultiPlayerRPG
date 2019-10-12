using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// менеджер отображения выбора персонажа
/// </summary>
public class AvatarChangeManager : MonoBehaviour
{
    private GameObject avatar;

    [SerializeField]
    private AvatarData defaultAvatar;

    private Dictionary<string, GameObject> spawnedAvatars;

    private void Awake()
    {
        PlayerSettings.SetNewAvatar += SpawnAvatar;
        spawnedAvatars = new Dictionary<string, GameObject>();
    }

    private void Start()
    {
        SpawnAvatar(defaultAvatar);
    }

    private void SpawnAvatar(AvatarData avatarData)
    {
        if(avatar != null)
        {
            avatar.SetActive(false);
        }
        if(spawnedAvatars.ContainsKey(avatarData.name))
        {
            avatar.SetActive(false);
            avatar = spawnedAvatars[avatarData.name].gameObject;
           
        }
        else
        {
            avatar = Instantiate(avatarData.AvatarPrefab, transform);
            spawnedAvatars.Add(avatarData.name, avatar);
        }

        if (avatar != null)
        {
            avatar.SetActive(true);
        }
    }
}
