using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class HangmanDraw
    {
        public string SwitchErrors(byte count)
        {
            // https://stackoverflow.com/a/57819758
            return count switch
            {
                0 => DrawInitial(),
                1 => DrawHead(),
                2 => DrawBody(),
                3 => DrawLeftArm(),
                4 => DrawRightArm(),
                5 => DrawLeftLeg(),
                6 => DrawRightLeg(),
                _ => "default"
            };
        }

        private string DrawInitial()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |\n" +
                "     |\n" +
                "     |\n" +
                "     |\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawHead()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |\n" +
                "     |\n" +
                "     |\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawBody()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |       |\n" +
                "     |       |\n" +
                "     |\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawLeftArm()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |      \\|\n" +
                "     |       |\n" +
                "     |\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawRightArm()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |      \\|/\n" +
                "     |       |\n" +
                "     |\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawLeftLeg()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |      \\|/\n" +
                "     |       |\n" +
                "     |      /\n" +
                "     |\n" +
                "    _|___\n";
        }

        private string DrawRightLeg()
        {
            return
                "      _______\n" +
                "     |/      |\n" +
                "     |      (_)\n" +
                "     |      \\|/\n" +
                "     |       |\n" +
                "     |      / \\\n" +
                "     |\n" +
                "    _|___\n";
        }

        public string DrawIntro()
        {
            return " _                                             \r\n| |__   __ _ _ __   __ _ _ __ ___   __ _ _ __  \r\n| '_ \\ / _` | '_ \\ / _` | '_ ` _ \\ / _` | '_ \\ \r\n| | | | (_| | | | | (_| | | | | | | (_| | | | |\r\n|_| |_|\\__,_|_| |_|\\__, |_| |_| |_|\\__,_|_| |_|\r\n                   |___/                       ";
        }

        public string DrawVictory()
        {
            return "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n~__   __           __        __          ~\r\n~\\ \\ / /__  _   _  \\ \\      / /__  _ __  ~\r\n~ \\ V / _ \\| | | |  \\ \\ /\\ / / _ \\| '_ \\ ~\r\n~  | | (_) | |_| |   \\ V  V / (_) | | | |~\r\n~  |_|\\___/ \\__,_|    \\_/\\_/ \\___/|_| |_|~\r\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
        }

        public string DrawFail()
        {
            return "*****************************************************\r\n*  ____                         ___                 *\r\n* / ___| __ _ _ __ ___   ___   / _ \\__   _____ _ __ *\r\n*| |  _ / _` | '_ ` _ \\ / _ \\ | | | \\ \\ / / _ \\ '__|*\r\n*| |_| | (_| | | | | | |  __/ | |_| |\\ V /  __/ |   *\r\n* \\____|\\__,_|_| |_| |_|\\___|  \\___/  \\_/ \\___|_|   *\r\n*****************************************************";
        }
    }
}
