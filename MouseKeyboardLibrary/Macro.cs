using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace MouseKeyboardLibrary
{

    /// <summary>
    /// All possible events that macro can record
    /// </summary>
    [Serializable]
    public enum MacroEventType
    {
        MouseMove,
        MouseDown,
        MouseUp,
        MouseWheel,
        KeyDown,
        KeyUp
    }

    /// <summary>
    /// Series of events that can be recorded any played back
    /// </summary>
    [Serializable]
    public class MacroEvent : ISerializable
    {

        public MacroEventType macroEventType;
        public EventArgs EventArgs;
        public int TimeSinceLastEvent;

        public MacroEvent(MacroEventType macroEventType, EventArgs eventArgs, int timeSinceLastEvent)
        {

            this.macroEventType = macroEventType;
            EventArgs = eventArgs;
            TimeSinceLastEvent = timeSinceLastEvent;

        }
        protected MacroEvent(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");
            TimeSinceLastEvent = (int)info.GetValue("TimeSinceLastEvent", typeof(int));
            macroEventType = (MacroEventType)info.GetValue("MacroEventType", typeof(int));

            int type = (int)info.GetValue("MouseEventType", typeof(int));
            if (type == 1)
            {
                MouseButtons mouseButtons =  (MouseButtons)info.GetValue("MouseButtons", typeof(int));
                int Clicks = (int)info.GetValue("Clicks", typeof(int));
                int Delta = (int)info.GetValue("Delta", typeof(int));
                int X = (int)info.GetValue("X", typeof(int));
                int Y = (int)info.GetValue("Y", typeof(int));

                EventArgs = new MouseEventArgs(mouseButtons, Clicks, X, Y, Delta);
            }
            else
            {
                Keys keyData = (Keys)info.GetValue("KeyData", typeof(int));
                EventArgs = new KeyEventArgs(keyData);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");
            info.AddValue("TimeSinceLastEvent", TimeSinceLastEvent);
            info.AddValue("MacroEventType", macroEventType);
            if (EventArgs.GetType() == typeof(MouseEventArgs))
            {
                info.AddValue("MouseEventType", 1);
                MouseEventArgs mouse = (MouseEventArgs)EventArgs;
                info.AddValue("MouseButtons", mouse.Button);
                info.AddValue("Clicks", mouse.Clicks);
                info.AddValue("Delta", mouse.Delta);
                info.AddValue("X", mouse.X);
                info.AddValue("Y", mouse.Y);
            }
            else if (EventArgs.GetType() == typeof(KeyEventArgs))
            {
                info.AddValue("MouseEventType", 2);
                KeyEventArgs keys = (KeyEventArgs)EventArgs;
                info.AddValue("KeyData", keys.KeyData);
            }

        }
    }

}
