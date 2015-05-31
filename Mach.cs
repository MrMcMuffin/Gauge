using UnityEngine;
using Mach.Extensions;
using KSP.IO;

namespace Mach
{
    public class Mach : PartModule
    {
        private static Rect _windowPosition = new Rect();

        public override void OnStart(StartState state)
        {
            if (state != StartState.Editor)
            {
                RenderingManager.AddToPostDrawQueue(0, OnDraw);
            }
        }

        public override void OnSave(ConfigNode node)
        {
 	        PluginConfiguration config = PluginConfiguration.CreateForType<Mach>();

            config.SetValue("Window Position", _windowPosition);
            config.save();
        }

        public override void OnLoad(ConfigNode node)
        {
 	        PluginConfiguration config = PluginConfiguration.CreateForType<Mach>();

            config.load();
            _windowPosition = config.GetValue<Rect>("Window Position");
        }
        
        private void OnDraw()
        {
            if (this.vessel == FlightGlobals.ActiveVessel && this.part.IsPrimary(this.vessel.parts, this.ClassID)) ;
            {
                _windowPosition = GUILayout.Window(10, _windowPosition, OnWindow, "Mach Gauge");

                if (_windowPosition.x == 0f && _windowPosition.y == 0f)
                    _windowPosition = _windowPosition.CenterScreen();
            }
        }

        private void OnWindow(int windowid)
        {
            GUILayout.Label(vessel.mach.ToString("0.0"), GUILayout.Width(100f));
            
            GUI.DragWindow();
        }  
    }
}
