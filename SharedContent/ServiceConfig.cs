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

namespace SharedContent
{
    public class ServiceConfig
    {
        // Lists the variables that can be altered within the XML file.
        // They have to be in the same order they are placed here or it will flag as an error.
       // public String Service;
        public bool isInputServiceActive;
        public bool isPhysicsServiceActive;
        public bool isScreenServiceActive;
        public bool isUserServiceActive;
        public String isOtherServiceActive;
    }
}
