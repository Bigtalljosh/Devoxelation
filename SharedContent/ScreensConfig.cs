using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace SharedContent
{
    public class ScreensConfig
    {
        // Lists the variables that can be altered within the XML file.
        // They have to be in the same order they are placed here or it will flag as an error.
        public String MenuSound;
        public String MenuFont;
        public String Transition_BGImage;
        public double EngineSplash_Duration;
        public float EngineSplash_Opacity;
        public String EngineSplash_BGImage;
        public double UniversitySplash_Duration;
        public float UniversitySplash_Opacity;
        public String UniversitySplash_BGImage;
        public double ControllerDetect_TranOn;
        public double ControllerDetect_TranOff;
        public String ControllerDetect_BGImage;
        public String ControllerDetect_360Image;
        public String ControllerDetect_PCImage;

        // Turn this in to an array, so users can add unlimited options. Work out distance from the bottom
        // by using if statements through the actual screen?
        public String Menuoptions01;
        public String Menuoptions02;
        public String Menuoptions03;
        public String Menuoptions04;
        public String MainMenu_BGImage;
    }
}
