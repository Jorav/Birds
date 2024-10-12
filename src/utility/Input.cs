using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Text;

namespace Birds.src.utility
{
    public class Input
    {
        public Camera Camera { get; set; }
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Pause { get; set; }
        public Keys Build { get; set; }
        public Keys Enter { get; set; }
        public Vector2 MousePositionGameCoords { get { return (Mouse.GetState().Position.ToVector2() - new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2) )/Camera.Zoom + Camera.Position; } }
        public Vector2 TouchPadPositionGameCoords { get { return (TouchPadPosition - new Vector2(Game1.ScreenWidth / 2, Game1.ScreenHeight / 2)) / Camera.Zoom + Camera.Position; } }
        public Vector2 TouchPadPosition
        {
            get
            {
                TouchPanelCapabilities tc = TouchPanel.GetCapabilities();
                if (tc.IsConnected)
                {
                    TouchCollection touchCollection = TouchPanel.GetState();
                    if (trackedTLID != -1) //remove last tracked touch location if its not active anymore
                    {
                        foreach (TouchLocation tl in touchCollection)
                        {
                            if (tl.Id == trackedTLID)
                            {
                                if (tl.State == TouchLocationState.Released)
                                    trackedTLID = -1;
                            }
                        }
                    }
                    if (trackedTLID == -1)//track new location if untracked
                    {
                        foreach (TouchLocation tl in touchCollection)
                        {
                            if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved))
                            {
                                trackedTLID = tl.Id;
                            }
                        }
                    }
                    if (trackedTLID != -1) //return to the tracked location
                    {
                        foreach (TouchLocation tl in touchCollection)
                        {
                            if (tl.Id == trackedTLID && ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved)))
                            {
                                return tl.Position;
                            }
                        }
                    }
                }
                return Vector2.Zero;
            }
        }
        private int trackedTLID = -1;
        public bool TouchPadActive { 
            get 
            { 
                TouchPanelCapabilities tc = TouchPanel.GetCapabilities();
                if(tc.IsConnected)
                {
                    TouchCollection touchCollection = TouchPanel.GetState();
                    foreach (TouchLocation tl in touchCollection)
                    {
                        if ((tl.State == TouchLocationState.Pressed) || (tl.State == TouchLocationState.Moved))
                            return true;
                    }
                }
                return false;
            } 
        }
        private bool pauseDown;
        public bool PauseClicked //OBS, new state of button needs to change each update
        {
            get
            {
                bool pauseClicked = false;
                bool newPauseDown = Keyboard.GetState().IsKeyDown(Pause);
                if (!pauseDown && newPauseDown)
                {
                    pauseClicked = true;
                }
                pauseDown = newPauseDown;
                return pauseClicked;
            }
        }
        private bool buildDown;
        public bool BuildClicked //OBS, new state of button needs to change each update
        {
            get
            {
                bool buildClicked = false;
                bool newBuildDown = Keyboard.GetState().IsKeyDown(Build);
                if (!buildDown && newBuildDown)
                {
                    buildClicked = true;
                }
                buildDown = newBuildDown;
                return buildClicked;
            }
        }
        private bool enterDown;
        public bool EnterClicked //OBS, new state of button needs to change each update
        {
            get
            {
                bool enterClicked = false;
                bool newEnterDown = Keyboard.GetState().IsKeyDown(Enter);
                if (!enterDown && newEnterDown)
                {
                    enterClicked = true;
                }
                enterDown = newEnterDown;
                return enterClicked;
            }
        }
        private bool leftMBDown;
        public bool LeftMBClicked //OBS, new state of button needs to change each update
        {
            get
            {
                bool leftMBClicked = false;
                bool newLeftMBDown = Mouse.GetState().LeftButton == ButtonState.Pressed;
                if (!leftMBDown && newLeftMBDown)
                {
                    leftMBClicked = true;
                }
                leftMBDown = newLeftMBDown;
                return leftMBClicked;
            }
        }
        public bool LeftMBDown
        {
            get
            {
                return Mouse.GetState().LeftButton == ButtonState.Pressed;
            }
        }
        public bool RightMBDown
        {
            get
            {
                return Mouse.GetState().RightButton == ButtonState.Pressed;
            }
        }
        public int PreviousScrollValue { get; set; }
        private int scrollValue;
        public int ScrollValue
        {
            get
            {
                PreviousScrollValue = scrollValue;
                scrollValue = Mouse.GetState().ScrollWheelValue;
                return scrollValue;
            }
        }
    }
}
