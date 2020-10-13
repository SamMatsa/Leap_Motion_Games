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
    static int coins = 10;
    static int totalScore = 0;
    static int numberOfGamesPlayed = 0;
    static Dictionary<int, bool> skins = new Dictionary<int, bool>();
    static bool toolTip = true;

    public static bool getToolTip()
    {
        return toolTip;
    }

    public static void setToolTip(bool bo)
    {
        toolTip = bo;
    }

    //data for cusomization
    static int productNumber;
    static int productValue;

    //data for current game / last game
    static int gameId;
    static int selectedCharacter = 1;
    static int difficulty = 1;
    static float time = 60;
    static float cooldown;
    static float totalTime;
    static int exercisesTotal;
    static int bustedTotal;
    static float exercisesPerMinute;

    //EndCard
    static int scoreExercises = 0;
    static int scoreBusted = 0;
    static int scoreDifficulty = 0;
    static int scoreStealth = 0;
    static int scoreTotal = 0;

    static bool levelUp = false;
    static int winCoins = 1;
    static int newDifficulty = 0;

    //getters and setters

    public static int getScoreExercises()
    {
        return scoreExercises;
    }

    public static int getScoreBusted()
    {
        return scoreBusted;
    }

    public static int getScoreDifficulty()
    {
        return scoreDifficulty;
    }

    public static int getScoreStealth()
    {
        return scoreStealth;
    }

    public static int getScoreTotal()
    {
        return scoreTotal;
    }

    public static bool getLevelUp()
    {
        return levelUp;
    }

    public static int getNewDifficulty()
    {
        return newDifficulty;
    }

    public static int getWinCoins()
    {
        return winCoins;
    }

    public static void putScoreOnList(int st)
    {
        int scoreForList = st;
        switch (difficulty)
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
        }
    }

    public static void calcEndCard()
    {

        setGameId();
        scoreExercises = 20 * exercisesTotal;
        scoreBusted = -100 * bustedTotal ;
        scoreDifficulty = difficulty * 100 - 100;

        if(bustedTotal == 0)
        {
            scoreStealth = 300;
        }
        else
        {
            scoreStealth = 0;
        }
        scoreTotal = scoreExercises + scoreDifficulty + scoreStealth + scoreBusted;
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
            if(totalScore >= 1000)
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
            if (totalScore >= 2500)
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
            if (totalScore >= 4500)
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
            if (totalScore >= 7000)
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
            if (totalScore >= 10000)
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

        winCoins = 2;
        if(bustedTotal == 0)
        {
            winCoins += 2;
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

        switch (numberOfGamesPlayed)
        {
            case 1:
                newDifficulty = 1;
                break;
            case 2:
                newDifficulty = 2;
                break;
            default:
                newDifficulty = 0;
                break;
        }

    }

    public static void setExercisesTotal(int ex)
    {
        exercisesTotal = ex;
    }

    public static int getExercisesTotal()
    {
        return exercisesTotal;
    }

    public static void incrementExercisesTotal()
    {
        exercisesTotal++;
    }

    public static void setBustedTotal(int bu)
    {
        bustedTotal = bu;
    }


    public static void setTime(float t)
    {
        time = t;
    }

    public static void setCooldown(float cool)
    {
        cooldown = cool;
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

    public static bool isCharacterUnlocked(int character)
    {
        return skins[character];
    }

    public static Dictionary<int, bool> getSkins()
    {
        return skins;
    }

    public static void setSkins(Dictionary<int, bool> newSkins) {
        skins = newSkins;
    }

    public static int getSelectedCharacter()
    {
        return selectedCharacter;
    }

    public void setSelectedCharacter(int charSel)
    {
        if (isCharacterUnlocked(charSel))
        {
            selectedCharacter = charSel;
        }
    }

    public static int getDifficulty()
    {
        return difficulty;
    }

    public void setDifficulty(int diff)
    {
        if(numberOfGamesPlayed >= diff - 1)
        {
            difficulty = diff;
        }
    }

    public static int getCoins()
    {
        return coins;
    }

    public static void setCoins(int newCoins)
    {
        coins = newCoins;
    }


    //File creation
    public void createSessionFile() {
        
        string path = Application.dataPath + "/" + getUserId().ToString();
        Directory.CreateDirectory(path);
        saveSessionFile();
        
    }

    public static void saveSessionFile()
    {
        string path = Application.dataPath + "/" + getUserId().ToString();
        string fileName = "/" + getUserId().ToString() + ".json";


        string skinString = "";
        bool first = true;

        foreach(KeyValuePair<int,bool> entry in skins)
        {
            if (entry.Value)
            {
                if (first)
                {
                    skinString = skinString + entry.Key.ToString();
                    first = false;
                }
                else
                {
                    skinString = skinString + "," + entry.Key.ToString();
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
            "\"skins\": \"" + 
            "[" +
            skinString +
            "]" + 
            "\"" + "\n" +
            //skins

            "}";
        File.WriteAllText(path + fileName, content);
    }

    public static void createLevelFile() {
        
        string path = Application.dataPath + "/" + getUserId().ToString();

        string content =
            "{" + "\n" +
            "\"gameId\": " + gameId.ToString() + "," + "\n" +
            "\"difficulty\": " + difficulty.ToString() + "," + "\n" +
            "\"skin\": " + selectedCharacter + "," + "\n" +
            "\"gameTime\": " + time  + "," + "\n" +
            "\"cooldownTime\": " + cooldown + "," + "\n" +
            "\"totalTime\": " + totalTime + "," + "\n" +
            "\"exercisesTotal\": " + exercisesTotal + "," + "\n" +
            "\"exercisesPerMinute\": " + exercisesPerMinute + "," + "\n" +
            "\"score\": " + scoreTotal + "\n" +
            //skins

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

    //leaderboard
    public static List<LeaderBoardElement> leaderboard1 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> leaderboard2 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> leaderboard3 = new List<LeaderBoardElement>();

    public static List<LeaderBoardElement> userboard1 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> userboard2 = new List<LeaderBoardElement>();
    public static List<LeaderBoardElement> userboard3 = new List<LeaderBoardElement>();

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
                lb = userboard1;
                break;
            case 5:
                lb = userboard2;
                break;
            case 6:
                lb = userboard3;
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

    public void initLeaderBoardData()
    {
        leaderboard1.Add(new LeaderBoardElement("Detlef", 740));
        leaderboard1.Add(new LeaderBoardElement("Karim", 920));
        leaderboard1.Add(new LeaderBoardElement("Josef", 1240));
        leaderboard1.Add(new LeaderBoardElement("Achim", 1560));
        leaderboard1.Add(new LeaderBoardElement("Jascha", 1900));

        leaderboard2.Add(new LeaderBoardElement("Detlef", 860));
        leaderboard2.Add(new LeaderBoardElement("Jascha", 1020));
        leaderboard2.Add(new LeaderBoardElement("Achim", 1400));
        leaderboard2.Add(new LeaderBoardElement("Karim", 1920));
        leaderboard2.Add(new LeaderBoardElement("Josef", 2200));

        leaderboard3.Add(new LeaderBoardElement("Detlef", 1100));
        leaderboard3.Add(new LeaderBoardElement("Karim", 1500));
        leaderboard3.Add(new LeaderBoardElement("Achim", 1680));
        leaderboard3.Add(new LeaderBoardElement("Jascha", 2120));
        leaderboard3.Add(new LeaderBoardElement("Josef", 2360));

        userboard1.Add(new LeaderBoardElement(userName, 0));
        userboard2.Add(new LeaderBoardElement(userName, 0));
        userboard3.Add(new LeaderBoardElement(userName, 0));

        //sortLeaderBoard(userboard1);

        sortLeaderBoard(leaderboard1);
        sortLeaderBoard(leaderboard2);
        sortLeaderBoard(leaderboard3);
    }

    public void initSkins()
    {
        skins.Add(1, true);
        skins.Add(2, false);
        skins.Add(3, false);
        skins.Add(4, false);
        skins.Add(5, false);
        skins.Add(6, false);
    }

}
