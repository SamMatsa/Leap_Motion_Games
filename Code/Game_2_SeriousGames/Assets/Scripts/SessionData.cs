using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SessionData : MonoBehaviour
{
    //overall user data
    static int userId;
    static string userName = "Spieler";
    static int level = 0;
    static int coins = 1;
    static int totalScore = 0;
    static int numberOfGamesPlayed = 0;
    static Dictionary<int, bool> buffs = new Dictionary<int, bool>();
    static Dictionary<int, bool> weapons = new Dictionary<int, bool>();

    static bool toolTip = true;

    #region currentgame
    static int gameId;
    static int selectedBuff = 1;
    static int selectedWeapon = 1;
    static int enemiesKilled = 0;
    static int world = 1;
    static int currentScore = 0;
    static int enemiesKilledScore = 0;
    static int failedShots = 0;
    static float time = 60;
    static float cooldown = 0;
    static float totalTime;
    static int exercisesTotal;
    static float exercisesPerMinute;
    #endregion

    public static void resetGameData()
    {
        cooldown = 0;
        currentScore = 0;
        enemiesKilled = 0;
        failedShots = 0;
        enemiesKilledScore = 0;
        exercisesTotal = 0;
    }

    #region endscreen
    static int scoreEnemiesKilled = 0;
    static int scoreWorld = 0;
    static int scoreFailedShots = 0;
    static int scoreTotal = 0;

    static bool levelUp = false;
    static int winCoins = 1;
    static int newWorld = 0;


    public static void calcEndCard()
    {
        totalTime = time - cooldown;
        exercisesPerMinute = (exercisesTotal * 60) / (time - cooldown);
        setGameId();
        scoreEnemiesKilled = calcEnemiesKilledScore();
        scoreWorld = world * 100 - 100;

        if(failedShots == 0)
        {
            scoreFailedShots = 500;
        }
        else
        {
            scoreFailedShots = 0;
        }
        scoreTotal = scoreEnemiesKilled + scoreWorld + scoreFailedShots;

        if(scoreTotal < 0)
        {
            scoreTotal = 0;
        }

        //putscore on List
        putScoreOnList(scoreTotal);

        totalScore += scoreTotal;

        //levelUp
        bool levelUpGained = false;

        if(level == 0)
        {
            if(totalScore >= 2000)
            {
                level++;
                levelUpGained = true;
            }
            else
            {
                levelUp = false;
            }
        }

        if (level == 1)
        {
            if (totalScore >= 12000)
            {
                level++;
                levelUpGained = true;
            }
            else
            {
                if (!levelUpGained)
                {
                levelUp = false;
                }
            }
        }

        if (level == 2)
        {
            if (totalScore >= 24000)
            {
                level++;
                levelUpGained = true;
            }
            else
            {
                if (!levelUpGained)
                {
                    levelUp = false;
                }
            }
        }

        if (level == 3)
        {
            if (totalScore >= 36000)
            {
                level++;
                levelUpGained = true;
            }
            else
            {
                if (!levelUpGained)
                {
                    levelUp = false;
                }
            }
        }

        if (level == 4)
        {
            if (totalScore >= 50000)
            {
                level++;
                levelUpGained = true;
            }
            else
            {
                if (!levelUpGained)
                {
                    levelUp = false;
                }
            }
        }

        if (level >= 5)
        {
                if (!levelUpGained)
                {
                    levelUp = false;
                }
        }

        if (levelUpGained)
        {
            levelUp = true;
        }

        //coins
        winCoins = 2;
        if(failedShots == 0)
        {
            winCoins += 3;
        }
        if(scoreTotal >= 2000)
        {
            winCoins += 2;
        }
        else if(scoreTotal >= 1000)
        {
            winCoins += 1;
        }
        coins = coins + winCoins;

        //newWorld
        switch (numberOfGamesPlayed)
        {
            case 1:
                newWorld = 1;
                break;
            case 2:
                newWorld = 2;
                break;
            case 3:
                newWorld = 3;
                break;
            default:
                newWorld = 0;
                break;
        }

    }
    #endregion

    #region Getters and Setters
    
    public static int getNewWorld()
    {
        return newWorld;
    }

    public static int calcEnemiesKilledScore()
    {
        enemiesKilledScore = world * enemiesKilled * 140;
        return enemiesKilledScore;
    }

    public static int getEnemiesKilledScore()
    {
        return enemiesKilledScore;
    }

    public static int getScoreWorld()
    {
        return scoreWorld;
    }

    public static int getFailedShotsScore()
    {
        return scoreFailedShots;
    }
    public static int getCurrentScore()
    {
        return currentScore;
    }

    public static void incrementEnemiesKilled()
    {
        enemiesKilled++;
    }

    public static int getEnemiesKilled()
    {
        return enemiesKilled;
    }

    public static void incrementFailedShots()
    {
        failedShots++;
    }

    public static int getFailedShots()
    {
        return failedShots;
    }

    public static bool getToolTip()
    {
        return toolTip;
    }

    public static void setToolTip(bool bo)
    {
        toolTip = bo;
    }

    public static int getScoreTotal()
    {
        return scoreTotal;
    }

    public static bool getLevelUp()
    {
        return levelUp;
    }


    public static int getWinCoins()
    {
        return winCoins;
    }

    public static void setExercisesTotal(int ex)
    {
        exercisesTotal = ex;
    }

    public static void incrementExercisesTotal()
    {
        exercisesTotal++;
    }

    public static int getExercisesTotal()
    {
        return exercisesTotal;
    }

    public static void setTime(float t)
    {
        time = t;
    }

    public static void setCooldown(float cool)
    {
        cooldown = cool;
    }

    public static void incrementCooldownBy(float incrCool)
    {
        cooldown += incrCool;
    }

    public static void setTotalTime(float toti)
    {
        totalTime = toti;
    }

    public static void setExercisesPerMinute(float expm)
    {
        exercisesPerMinute = expm;
    }

    public static void increementNumberOfGamesPlayed()
    {
        numberOfGamesPlayed++;
    }

    public static int getNumberOfGamesPlayed()
    {
        return numberOfGamesPlayed;
    }

    public static string getUserName() {
        return userName;
    }

    public static void setUserName(string name) {
        userName = name;
    }

    public static string GetTimestamp(System.DateTime value) {
        return value.ToString("MMddHHmm");
    }

    public static int getUserId() {
        return userId;
    }

    public void setUserId() {
        string timeStamp = GetTimestamp(System.DateTime.Now);
        userId = int.Parse(timeStamp);
    }

    public static void setGameId()
    {
        gameId = userId + numberOfGamesPlayed;
    }

    public static int getTotalUserScore()
    {
        return totalScore;
    }

    public static int getLevel()
    {
        return level;
    }

    public static bool isBuffUnlocked(int buff)
    {
        return buffs[buff];
    }

    public static bool isWeaponUnlocked(int weap)
    {
        return weapons[weap];
    }

    public static Dictionary<int, bool> getBuffs()
    {
        return buffs;
    }

    public static void setBuffs(Dictionary<int, bool> newBuffs) {
        buffs = newBuffs;
    }

    public static void setWeapons(Dictionary<int, bool> newWeapons)
    {
        weapons = newWeapons;
    }


    public static int getSelectedBuff()
    {
        return selectedBuff;
    }

    public void setSelectedBuff(int buffSel)
    {
        if (isBuffUnlocked(buffSel))
        {
            selectedBuff = buffSel;
        }
    }

    public static Dictionary<int, bool> getWeapons()
    {
        return weapons;
    }

    public static int getSelectedWeapon()
    {
        return selectedWeapon;
    }

    public void setSelectedWeapon(int weapSel)
    {
        if (isWeaponUnlocked(weapSel))
        {
            selectedWeapon = weapSel;
        }
    }

    public static int getWorld()
    {
        return world;
    }

    public static void setWorld(int worldSel)
    {
        world = worldSel;
    }

    public static int getCoins()
    {
        return coins;
    }

    public static void setCoins(int newCoins)
    {
        coins = newCoins;
    }
    #endregion

    #region File creation
    public void createSessionFile() {
        
        string path = Application.dataPath + "/" + getUserId().ToString();
        Directory.CreateDirectory(path);
        saveSessionFile();
        
    }

    public static void saveSessionFile()
    {
        string path = Application.dataPath + "/" + getUserId().ToString();
        string fileName = "/" + getUserId().ToString() + ".json";


        //create buff string
        string buffString = "";
        bool firstBuff = true;

        foreach(KeyValuePair<int,bool> entry in buffs)
        {
            if (entry.Value)
            {
                if (firstBuff)
                {
                    buffString = buffString + entry.Key.ToString();
                    firstBuff = false;
                }
                else
                {
                    buffString = buffString + "," + entry.Key.ToString();
                }
            }
        }

        //create buff string
        string weaponString = "";
        bool firstWeapon = true;

        foreach (KeyValuePair<int, bool> entry in weapons)
        {
            if (entry.Value)
            {
                if (firstWeapon)
                {
                    weaponString = weaponString + entry.Key.ToString();
                    firstWeapon = false;
                }
                else
                {
                    weaponString = weaponString + "," + entry.Key.ToString();
                }
            }
        }

        string content =
            "{" + "\n" +

            "\"userId\": " + getUserId().ToString() + "," + "\n" +
            "\"userName\": \"" + getUserName() + "\"" + "," + "\n" +
            "\"level\": \"" + level.ToString() + "\"" + "," + "\n" +
            "\"coins\": \"" + coins.ToString() + "\"" + "," + "\n" +
            "\"totalScore\": \"" + totalScore + "\"" + "," + "\n" +
            "\"buffs\": \"" + 
            "[" +
            buffString +
            "]" + 
            "\"" + "," + "\n" +
             "\"weapons\": \"" +
            "[" +
            weaponString +
            "]" +
            "\"" + "\n" +
            "}";
        File.WriteAllText(path + fileName, content);
    }

    public static void createLevelFile() {
        
        string path = Application.dataPath + "/" + getUserId().ToString();

        string content =
            "{" + "\n" +
            "\"gameId\": " + gameId.ToString() + "," + "\n" +
            "\"world\": " + world.ToString() + "," + "\n" +
            "\"buff\": " + selectedBuff + "," + "\n" +
            "\"weapon\": " + selectedWeapon + "," + "\n" +
            "\"gameTime\": " + time  + "," + "\n" +
            "\"cooldownTime\": " + cooldown + "," + "\n" +
            "\"totalTime\": " + totalTime + "," + "\n" +
            "\"exercisesTotal\": " + exercisesTotal + "," + "\n" +
            "\"exercisesPerMinute\": " + exercisesPerMinute + "," + "\n" +
            "\"score\": " + scoreTotal + "\n" +

            "}";

        int counter = 1;
        while (counter != 0)
        {
            string fileName = "/" + getUserId().ToString() + "_00" + counter + ".json";
            if (!File.Exists(path + fileName))
            {
                File.WriteAllText(path + fileName, content);
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        

    }
    #endregion

    #region leaderboard
    public static List<LeaderBoardElement> leaderboard1 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> leaderboard2 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> leaderboard3 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> leaderboard4 = new List<LeaderBoardElement>();

    public static void putScoreOnList(int st)
    {
        int scoreForList = st;
        switch (world)
        {
            case 1:
                leaderboard1.Add(new LeaderBoardElement(userName, scoreForList));
                sortLeaderBoard(leaderboard1);
                break;
            case 2:
                leaderboard2.Add(new LeaderBoardElement(userName, scoreForList));
                sortLeaderBoard(leaderboard2);
                break;
            case 3:
                leaderboard3.Add(new LeaderBoardElement(userName, scoreForList));
                sortLeaderBoard(leaderboard3);
                break;
            case 4:
                leaderboard4.Add(new LeaderBoardElement(userName, scoreForList));
                sortLeaderBoard(leaderboard4);
                break;
        }
    }

    public static string getLeaderboard(int board,int rank, string field)
    {
        List<LeaderBoardElement> lb = leaderboard1;
        switch (board)
        {
            case 1:
                lb = leaderboard1;
                break;
            case 2:
                lb = leaderboard2;
                break;
            case 3:
                lb = leaderboard3;
                break;
            case 4:
                lb = leaderboard4;
                break;
        }

        if(field == "name")
        {
            return lb[rank-1].name;
        }
        if (field == "score")
        {
            return lb[rank-1].score.ToString();
        }
        else return "";
    }

    public static void sortLeaderBoard(List<LeaderBoardElement> leaderboard)
    {
        for(int i = 0; i < leaderboard.Count; i++)
        {
            for(int j = i + 1; j < leaderboard.Count; j++)
            {
                if(leaderboard[j].score > leaderboard[i].score)
                {
                    //Swap
                    LeaderBoardElement tmp = leaderboard[i];
                    leaderboard[i] = leaderboard[j];
                    leaderboard[j] = tmp;
                }
            }
        }
    }
    #endregion

    #region init
    public void initLeaderBoardData()
    {
        leaderboard1.Add(new LeaderBoardElement("Detlef", 5100));
        leaderboard1.Add(new LeaderBoardElement("Karim", 4300));
        leaderboard1.Add(new LeaderBoardElement("Josef", 2600));
        leaderboard1.Add(new LeaderBoardElement("Achim", 2100));
        leaderboard1.Add(new LeaderBoardElement("Jascha", 200));

        leaderboard2.Add(new LeaderBoardElement("Detlef", 10100));
        leaderboard2.Add(new LeaderBoardElement("Jascha", 9200));
        leaderboard2.Add(new LeaderBoardElement("Achim", 7400));
        leaderboard2.Add(new LeaderBoardElement("Karim", 2600));
        leaderboard2.Add(new LeaderBoardElement("Josef", 600));

        leaderboard3.Add(new LeaderBoardElement("Detlef", 12000));
        leaderboard3.Add(new LeaderBoardElement("Karim", 8500));
        leaderboard3.Add(new LeaderBoardElement("Achim", 7900));
        leaderboard3.Add(new LeaderBoardElement("Jascha", 5500));
        leaderboard3.Add(new LeaderBoardElement("Josef", 980));

        leaderboard4.Add(new LeaderBoardElement("Detlef", 15200));
        leaderboard4.Add(new LeaderBoardElement("Karim", 13600));
        leaderboard4.Add(new LeaderBoardElement("Achim", 11900));
        leaderboard4.Add(new LeaderBoardElement("Jascha", 8200));
        leaderboard4.Add(new LeaderBoardElement("Josef", 1040));

        sortLeaderBoard(leaderboard1);
        sortLeaderBoard(leaderboard2);
        sortLeaderBoard(leaderboard3);
        sortLeaderBoard(leaderboard4);
    }

    public void initBuffs()
    {
        buffs.Add(1, true);
        buffs.Add(2, false);
        buffs.Add(3, false);
        buffs.Add(4, false);
    }

    public void initWeapons()
    {
        weapons.Add(1, true);
        weapons.Add(2, false);
        weapons.Add(3, false);
    }
    #endregion
}
