using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SharedContent
{
    public class KeybindingsConfig
    {
        // Menu Options for Keyboard and Gamepad
        public Keys KBMenuSelect;
        public Keys KBMenuCancel;
        public Keys KBMenuUp;
        public Keys KBMenuDown;
        public Buttons GPMenuSelect;
        public Buttons GPMenuCancel;

        // Player Options for Keyboard and Gamepad
        public Keys KBPlayerMoveUp;
        public Keys KBPlayerMoveDown;
        public Keys KBPlayerMoveLeft;
        public Keys KBPlayerMoveRight;
        public Keys KBPlayerAction01;
        public Keys KBPlayerAction02;
        public Keys KBPlayerAction03;
        public Buttons GPPlayerAction01;
        public Buttons GPPlayerAction02;
        public Buttons GPPlayerAction03;

        // Interaction Demo specific Keybindings
        public Keys KBEnableEditUI;
        public Keys KBDisableEditUI;
    }
}
