using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    private readonly string _path = "Item/";

    private readonly string _energyCell = "EnergyCell";
    private readonly string _note = "Note";
    private readonly string _questItem = "QuestItem";

    [SerializeField] private bool _isQuestSpawn;


    private void Start()
    {
        if (_isQuestSpawn && !FindObjectOfType<QuestItem>())
        {
            QuestItemSpawn();
        }

        RandomItemSpawn();
    }

    private void RandomItemSpawn()
    {
        var percentage = Random.Range(0, 100);

        if (0 <= percentage && 30 > percentage)
        {
            return;
        }

        else if (30 <= percentage && 80 > percentage)
        {
            Instantiate(Resources.Load(_path + _energyCell), transform.position, Quaternion.identity);
            return;
        }

        else
        {
            Instantiate(Resources.Load(_path + _note), transform.position, Quaternion.identity);
            return;
        }
    }

    private void QuestItemSpawn()
    {
        var percentage = Random.Range(0, 100);

        if(0 <= percentage && 50 > percentage)
        {
            Instantiate(Resources.Load(_path + _questItem), transform.position, Quaternion.identity);
        }

        else
        {
            return;
        }
    }
}
