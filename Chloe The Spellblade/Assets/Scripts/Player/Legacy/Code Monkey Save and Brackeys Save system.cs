/*
public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        currentHealth = data.currentHealth;
        maxHealth = data.maxHealth;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

    }

    #region //code monkey save system       left unfinished
    public float[] position;
    int coin;
    public static string sceneIndexString;
    public int maxHealthSS;
    public int currentHealthSS;

    public int manaSS;
    public int maxManaSS;

    public int berriesSS;
    public int maxBerriesSS;

    public void Save()
    {
        position[0] = transform.position.x;
        position[1] = transform.position.y;
        position[2] = transform.position.z;
        coin = playerCoin;
        maxHealthSS = maxHealth;
        currentHealthSS = currentHealth;
        manaSS = mana;
        maxManaSS = maxMana;
        berriesSS = berries;
        maxBerriesSS = maxBerries;

        PlayerPrefs.SetInt(sceneIndexString, SceneManager.GetActiveScene().buildIndex);

        SaveSystem.SavePlayer(this);

    }
    public void Load()
    {
        GetCoin(coin);
    }

    #endregion
*/