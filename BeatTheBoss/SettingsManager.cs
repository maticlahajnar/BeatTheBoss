using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss
{
    class SettingsManager
    {
        public static string playerName = "player";

        public static bool isGameSaved = false;
        public static int savedGameScore = 0;
        public static int savedGameRoomNumber = 0;
        public static float savedPlayerHp = 200f;

        public static float savedVolume = 0.5f;
        public static bool savedMusicMuted = false;

        public static void SaveSettings()
        {
            Properties.GameSettings.Default.playerName = playerName;
            Properties.GameSettings.Default.isGameSaved = isGameSaved;
            Properties.GameSettings.Default.savedGameScore = savedGameScore;
            Properties.GameSettings.Default.savedGameRoomNumber = savedGameRoomNumber;
            Properties.GameSettings.Default.savedGamePlayerHp = savedPlayerHp;
            Properties.GameSettings.Default.savedVolume = savedVolume;
            Properties.GameSettings.Default.savedMusicMuted = savedMusicMuted;

            Properties.GameSettings.Default.Save();
        }

        public static void ReadSettings()
        {
            playerName = Properties.GameSettings.Default.playerName;
            isGameSaved = Properties.GameSettings.Default.isGameSaved;
            savedGameScore = Properties.GameSettings.Default.savedGameScore;
            savedGameRoomNumber = Properties.GameSettings.Default.savedGameRoomNumber;
            savedPlayerHp = Properties.GameSettings.Default.savedGamePlayerHp;
            savedVolume = Properties.GameSettings.Default.savedVolume;
            savedMusicMuted = Properties.GameSettings.Default.savedMusicMuted;
        }
    }
}
