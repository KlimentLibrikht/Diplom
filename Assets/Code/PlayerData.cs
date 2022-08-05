[System.Serializable]
public class PlayerData
{
    public int level;
    public int health;
    public float[] postition;
    public int countkills;
    public int countTriggers;
    public int countMessages;

    public PlayerData(PlayerController player)
    {
        level = player.scene.buildIndex;
        this.health = player._health;
        this.countTriggers = player.countTriggers;
        this.countMessages = player.countMessages;
        this.countkills = player._countKillEnemy;
        postition = new float[3];
        this.postition[0] = player.transform.position.x;
        this.postition[1] = player.transform.position.y;
        this.postition[2] = player.transform.position.z;
    }
}
