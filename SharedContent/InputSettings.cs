using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

// HAD TO REMOVE THE XML FILE AS I COULDN@T SOLVE YOUR ISSUES.. IT WAS:
// InputSettings.xml
// CONTENTS CAN BE FOUND IN THE REGION BELOW... SORRY.

#region contents
/*<?xml version="1.0" encoding="utf-8" ?>
<XnaContent>
    <Asset Type="SharedContent.InputConfig">
        <!--
          # *************
          # Set Gamepad
          # *************
        -->
      <A>A</A>
      <B>B</B>
      <X>X</X>
      <Y>Y</Y>
      <LeftShoulder>LSH</LeftShoulder>
      <RightShoulder>RSH</RightShoulder>
      <LeftTrigger>LT</LeftTrigger>
      <RightTrigger>RT</RightTrigger>
      <LeftStick>LST</LeftStick>
      <RightStick>RST</RightStick>
      <Back>BACK</Back>
      <Start>START</Start>
      <DPadDown>DD</DPadDown>
      <DPadLeft>DL</DPadLeft>
      <DPadRight>DR</DPadRight>
      <DPadUp>DU</DPadUp>
      <LeftThumbstickDown>LTD</LeftThumbstickDown>
      <LeftThumbstickLeft>LTL</LeftThumbstickLeft>
      <LeftThumbstickRight>LTR</LeftThumbstickRight>
      <LeftThumbstickUp>LTU</LeftThumbstickUp>
      <RightThumbstickDown>RTD</RightThumbstickDown>
      <RightThumbstickLeft>RTL</RightThumbstickLeft>
      <RightThumbstickRight>RTR</RightThumbstickRight>
      <RightThumbstickUp>RTU</RightThumbstickUp>
      
    </Asset>
</XnaContent>*/

#endregion

namespace SharedContent
{
    public struct KeyboardSettings
    {
        public Keys A;
        public Keys B;
        public Keys X;
        public Keys Y;
        public Keys LeftShoulder;
        public Keys RightShoulder;
        public Keys LeftTrigger;
        public Keys RightTrigger;
        public Keys LeftStick;
        public Keys RightStick;
        public Keys Back;
        public Keys Start;
        public Keys DPadDown;
        public Keys DPadLeft;
        public Keys DPadRight;
        public Keys DPadUp;
        public Keys LeftThumbstickDown;
        public Keys LeftThumbstickLeft;
        public Keys LeftThumbstickRight;
        public Keys LeftThumbstickUp;
        public Keys RightThumbstickDown;
        public Keys RightThumbstickLeft;
        public Keys RightThumbstickRight;
        public Keys RightThumbstickUp;
    }

    public struct inputSettings
    {
        public KeyboardSettings[] KeyboardSettings;
    }

    public class InputSettings
    {
        public static inputSettings Read(string settingsFilename)
        {
            inputSettings gameSettings;
            Stream stream = File.OpenRead(settingsFilename);
            XmlSerializer serializer =
               new XmlSerializer(typeof(inputSettings));
            gameSettings = (inputSettings)serializer.Deserialize(stream);
            return gameSettings;
        }

        public static void Save(string settingsFilename, inputSettings InputSettings)
        {
            Stream stream = File.OpenWrite(settingsFilename);
            XmlSerializer serializer = new
               XmlSerializer(typeof(inputSettings));
            serializer.Serialize(stream, InputSettings);
        }

    }
}