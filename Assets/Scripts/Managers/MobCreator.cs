using UnityEngine;

public class MobCreator : MonoBehaviour
{

    public GameObject mobPrefab;

    private void OnEnable()
    {
        GameManager.createMobs += CreateMobs;
        GameManager.createNewMob += CreateNewMob;
    }

    private void OnDisable()
    {
        GameManager.createMobs -= CreateMobs;
        GameManager.createNewMob -= CreateNewMob;
    }

    private void CreateMobs(GameData data)
    {
        foreach (MobData mobData in data.mobs)
        {
            GameObject go = Instantiate(mobPrefab) as GameObject;
            Mob mob = go.GetComponent<Mob>() ?? go.AddComponent<Mob>();
            mob.data = mobData;
            go.name = "Mob_" + mobData.mobName;
        }
    }

    private void CreateNewMob(MobNames mobNames)
    {
        float xPos = Random.Range(-5, 5);
        float zPos = Random.Range(-5, 5);

        Vector3 spawnPosition = new Vector3(xPos, 1, zPos);
        Quaternion spawnRotation = new Quaternion();
        spawnRotation.eulerAngles = new Vector3(0f, Random.Range(0f, 360f));
        GameObject go = Instantiate(mobPrefab, spawnPosition, spawnRotation);
        string name = mobNames.GetRandomName();
        go.GetComponent<Mob>().mobName = name;
        go.name = "Mob_" + name;
    }

}
