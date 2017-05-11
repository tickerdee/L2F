using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SlimDX;
using SlimDX.DirectInput;
using System.Windows.Forms;

namespace L2F
{
    class InputController
    {
        public enum nonJoyTypes
        {
            keyboard = -1,
            mouse = -2
        }

        List<Joystick> joysitcks;

        JoystickState state;

        DirectInput dinput;

        String activeState, oldState;

        public Microsoft.Xna.Framework.Input.MouseState oldMouse;

        public List<inputObj> inputGroup, inputGroupOld;

        bool[] buttons;

        public Random r1;

        public static InputController instance;

        public InputController()
        {

            state = new JoystickState();
            dinput = new DirectInput();

            joysitcks = new List<Joystick>();

            inputGroup = new List<inputObj>();
            inputGroupOld = new List<inputObj>();

            r1 = new Random();

            /*

            // search for devices
            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                // create the device
                try
                {
                    joystick = new Joystick(dinput, device.InstanceGuid);
                    joystick.SetCooperativeLevel(game.Window.Handle, CooperativeLevel.Exclusive | CooperativeLevel.Foreground);
                    break;
                }
                catch (DirectInputException)
                {
                }
            }

            foreach (DeviceObjectInstance deviceObject in joystick.GetObjects())
            {
                if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                    joystick.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-1000, 1000);

            }

            // acquire the device

            joystick.Acquire();
            */

        }

        public static InputController getInstance()
        {

            if (instance == null)
            {
                instance = new InputController();
                instance.getMoreControllers(Program.game);
            }

            return instance;
        }

        /// <summary>
        /// Rescan for new controllers
        /// </summary>
        public void getMoreControllers(Game game)
        {

            Joystick temp = null;

            bool alreadyAquired = false;

            // search for devices
            foreach (DeviceInstance device in dinput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly))
            {
                alreadyAquired = false;

                // create the device
                try
                {
                    temp = new Joystick(dinput, device.InstanceGuid);

                    //Check to see if the controller is already set
                    foreach (Joystick joyS in joysitcks)
                    {
                        if (joyS == null)
                            break;

                        if (joyS.Equals(temp))
                        {
                            alreadyAquired = true;
                        }

                    }

                    if (alreadyAquired)
                    {
                        temp.Dispose();
                        continue;
                    }

                    temp.SetCooperativeLevel(game.Window.Handle, CooperativeLevel.Exclusive | CooperativeLevel.Foreground);

                    foreach (DeviceObjectInstance deviceObject in temp.GetObjects())
                    {
                        if ((deviceObject.ObjectType & ObjectDeviceType.Axis) != 0)
                            temp.GetObjectPropertiesById((int)deviceObject.ObjectType).SetRange(-1000, 1000);

                    }

                    // acquire the device

                    temp.Acquire();

                    joysitcks.Add(temp);
                }
                catch (DirectInputException)
                {
                    //Error not handled
                }
            }

        }

        public bool checkIfNowReleased(inputObj iObj)
        {

            bool currentState = false, oldState = false;

            foreach (inputObj ip in inputGroup)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    currentState = true;
                }

            }

            foreach (inputObj ip in inputGroupOld)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    oldState = true;
                }

            }

            if (!currentState && oldState)
                return true;

            return false;
        }

        public bool checkIfFirstPress(inputObj iObj)
        {

            bool positive = false;

            if (iObj.value > 0)
                positive = true;

            bool currentState = false, oldState = false;

            foreach (inputObj ip in inputGroup)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    if (ip.value >= iObj.value && positive)
                        currentState = true;
                    else if (ip.value <= iObj.value && !positive)
                        currentState = true;
                }

            }

            foreach (inputObj ip in inputGroupOld)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    oldState = true;
                }

            }

            if (currentState && !oldState)
                return true;

            return false;
        }

        public double activeAtAll(inputObj iObj)
        {

            bool positive = false;

            if (iObj.value > 0)
                positive = true;

            foreach (inputObj ip in inputGroup)
            {
                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {

                    if (iObj.inputMarker.CompareTo("pov") == 0)
                    {

                        if (ip.value == iObj.value)
                        {
                            return 1;
                        }

                    }else if (ip.value >= iObj.value && positive)
                        return ip.value;
                    else if (ip.value <= iObj.value && !positive)
                        return ip.value;

                    return 0;
                }
            }

            return 0;
        }

        public bool fullRelease(inputObj iObj)
        {

            if (inputGroup.Count == 0 && inputGroupOld.Count == 0)
                return true;

            foreach (inputObj ip in inputGroup)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    return false;
                }

            }

            foreach (inputObj ip in inputGroupOld)
            {

                if (ip.joy == iObj.joy && ip.inputMarker.CompareTo(iObj.inputMarker) == 0)
                {
                    return false;
                }

            }

            return true;
        }

        public Point getMousePosition()
        {
            return Microsoft.Xna.Framework.Input.Mouse.GetState().Position;
        }
		
		//All Active Inputs... Also updates
        public String activates()
        {

            inputGroup.Clear();

            for (int i = 0; i < joysitcks.Count; ++i )
            {

                Joystick joy = joysitcks[i];

                if (joy == null)
                    break;

                try
                {
                    if (joy.Acquire().IsFailure)
                        return "";
                }
                catch (Exception e) { return ""; }

                if (joy.Poll().IsFailure)
                    return "";

                state = joy.GetCurrentState();

                checkDemSticks(i);
                checkDemPovs(i);

                bool[] buttons = state.GetButtons();

                for (int b = 0; b < buttons.Length; b++)
                {
                    if (buttons[b])
                        inputGroup.Add(new inputObj(i, "b" + b + "", 1));
                }
            }

            keyBoardControl();
            mouseControl();

            activeState = "";

            foreach (inputObj ip in inputGroup)
            {
                activeState += ip.joy + " " + ip.inputMarker + " " + ip.value + ",";
            }

            return activeState;
        }

        public void checkDemSticks(int joyNum)
        {

            int buffer = 250/2;

            if (state.X >= buffer || state.X <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "SSX", state.X));
            if (state.Y >= buffer || state.Y <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "SSY", state.Y));
            if (state.Z >= buffer || state.Z <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "SSZ", state.Z));


            if (state.VelocityX >= buffer || state.VelocityX <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "VSX", state.VelocityX));
            if (state.VelocityY >= buffer || state.VelocityY <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "VSY", state.VelocityY));
            if (state.VelocityZ >= buffer || state.VelocityZ <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "VSZ", state.VelocityY));

            if (state.RotationX >= buffer || state.RotationX <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "RSX", state.RotationX));
            if (state.RotationY >= buffer || state.RotationY <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "RSY", state.RotationY));
            if (state.RotationZ >= buffer || state.RotationZ <= -buffer)
                inputGroup.Add(new inputObj(joyNum, "RSZ", state.RotationZ));

        }

        public void keyBoardControl()
        {

            Microsoft.Xna.Framework.Input.Keys[] pressed = Microsoft.Xna.Framework.Input.Keyboard.GetState().GetPressedKeys();

            foreach (Microsoft.Xna.Framework.Input.Keys k in pressed)
            {

                String keyMarker = k.ToString();

                inputGroup.Add(new inputObj((int)nonJoyTypes.keyboard, keyMarker, 1));

            }
        }

        public void mouseControl()
        {

            Microsoft.Xna.Framework.Input.MouseState mouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if (Microsoft.Xna.Framework.Input.ButtonState.Pressed == mouseState.LeftButton)
                inputGroup.Add(new inputObj((int)nonJoyTypes.mouse, "Left", 1));

            if (Microsoft.Xna.Framework.Input.ButtonState.Pressed == mouseState.RightButton)
                inputGroup.Add(new inputObj((int)nonJoyTypes.mouse, "Right", 1));

            if (Microsoft.Xna.Framework.Input.ButtonState.Pressed == mouseState.MiddleButton)
                inputGroup.Add(new inputObj((int)nonJoyTypes.mouse, "Middle", 1));

            oldMouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }

        public void doStateUpdate()
        {

            inputGroupOld.Clear();
            foreach (inputObj ip in inputGroup)
            {
                inputGroupOld.Add(ip);
            }
        }

        public void checkDemPovs(int joyNum)
        {

            int[] pov = state.GetPointOfViewControllers();

            for (int i = 0; i < pov.Length; ++i)
            {
                if (pov[i] != -1)
                    inputGroup.Add(new inputObj(joyNum, "pov", pov[0]));
            }
        }

        public void releaseJoys()
        {
            for (int i = 0; i < joysitcks.Count; ++i)
            {
                if (!joysitcks[i].Disposed)
                {
                    joysitcks[i].Unacquire();
                    joysitcks[i].Dispose();
                    joysitcks[i] = null;
                }
            }
            joysitcks = null;
        }

        ~InputController()
        {

            releaseJoys();
            state = null;
            dinput.Dispose();
            
        }
    }

    /// <summary>
    /// To hold all chosen inputs. i.e. moveLeft held input is A(default keyboard)
    /// </summary>
    class inputHolder
    {

        public inputObj accept, cancel, toggleShowInfluence,
            moveLeft, moveRight, moveUp, moveDown,
            openBuildingMenu, openMarketMenu, pause,
            pageNext, pagePrevious, options;

    }

    class inputObj
    {

        public int joy;
        public String inputMarker;
        public double value;
        public bool isJoystick;

        public inputObj(int j, String im, double value, bool isJoystick = false)
        {
            joy = j;
            inputMarker = im;
            this.value = value;
            this.isJoystick = isJoystick;
        }

        override public String ToString()
        {
            if(joy >= 0)
                return joy + " " + inputMarker + " " + value;
            else
                return inputMarker;
        }
    }
}
