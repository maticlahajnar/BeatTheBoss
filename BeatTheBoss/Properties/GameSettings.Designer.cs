﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeatTheBoss.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.5.0.0")]
    internal sealed partial class GameSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static GameSettings defaultInstance = ((GameSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new GameSettings())));
        
        public static GameSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isGameSaved {
            get {
                return ((bool)(this["isGameSaved"]));
            }
            set {
                this["isGameSaved"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("player")]
        public string playerName {
            get {
                return ((string)(this["playerName"]));
            }
            set {
                this["playerName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int savedGameScore {
            get {
                return ((int)(this["savedGameScore"]));
            }
            set {
                this["savedGameScore"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0")]
        public int savedGameRoomNumber {
            get {
                return ((int)(this["savedGameRoomNumber"]));
            }
            set {
                this["savedGameRoomNumber"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("200")]
        public float savedGamePlayerHp {
            get {
                return ((float)(this["savedGamePlayerHp"]));
            }
            set {
                this["savedGamePlayerHp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("0.5")]
        public float savedVolume {
            get {
                return ((float)(this["savedVolume"]));
            }
            set {
                this["savedVolume"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool savedMusicMuted {
            get {
                return ((bool)(this["savedMusicMuted"]));
            }
            set {
                this["savedMusicMuted"] = value;
            }
        }
    }
}
