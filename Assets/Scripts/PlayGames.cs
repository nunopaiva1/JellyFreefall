using UnityEngine;
using System;
using GooglePlayGames;
using GooglePlayGames.BasicApi;


public class PlayGames : MonoBehaviour
{
    //mpublic int playerScore;
    //string leaderboardID = "CgkIhNu_7M8UEAIQAA";
    string achievementID = "";
    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;
            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {

                PlayGamesPlatform.Instance.LoadScores(
              
                           "CgkIhNu_7M8UEAIQAA",
                           LeaderboardStart.PlayerCentered,
                           1,
                           LeaderboardCollection.Public,
                           LeaderboardTimeSpan.AllTime,
                       (LeaderboardScoreData data) => {
                           Debug.Log(data.Valid);
                           Debug.Log(data.Id);
                           Debug.Log(data.PlayerScore);
                           Debug.Log(data.PlayerScore.userID);
                           Debug.Log(data.PlayerScore.formattedValue);

                           PlayerPrefs.SetInt("highscore", int.Parse(data.PlayerScore.formattedValue));

                       });

                Debug.Log("Logged in successfully");

            }

            else
            {
                Debug.Log("Login Failed");
            }
        });
        UnlockAchievement();
    }

    public static void AddScoreToLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportScore(PlayerPrefs.GetInt("highscore"), "CgkIhNu_7M8UEAIQAA", success => { });
        }
    }

    public void ShowLeaderboard()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowLeaderboardUI();
        }
    }

    public void ShowAchievements()
    {
        if (Social.Active.localUser.authenticated)
        {
            platform.ShowAchievementsUI();
        }
    }

    public void UnlockAchievement()
    {
        if (Social.Active.localUser.authenticated)
        {
            Social.ReportProgress(achievementID, 100f, success => { });
        }
    }
}