using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

class InputManager
/*
amele gibi her defasında update içinde if if if if if diye her boku çevirmek yerine
bi kere çevirip event olarak yayınlamaya yarıyor

eğer aynı tuş basılıysa da milyon kere aynı eventi firelamıyor

geliştirmeye açık
*/
{
    public event EventHandler OnDpadUp;
    public event EventHandler OnDpadDown;
    public event EventHandler OnDpadLeft;
    public event EventHandler OnDpadRight;
    public event EventHandler OnButtonA;
    public event EventHandler OnButtonB;
    public event EventHandler OnButtonX;
    public event EventHandler OnButtonY;
    GamePadState gamPadlast;
    public void InputCheck()
    {
        GamePadState gamePadcurrent = GamePad.GetState(PlayerIndex.One);
        if(gamePadcurrent!=gamPadlast)
        {
        if(gamePadcurrent.IsButtonDown(Buttons.DPadUp)) OnDpadUp?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.DPadDown)) OnDpadDown?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.DPadLeft)) OnDpadLeft?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.DPadRight)) OnDpadRight?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.Y)) OnButtonY?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.X)) OnButtonX?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.A)) OnButtonA?.Invoke(this,EventArgs.Empty);
        if(gamePadcurrent.IsButtonDown(Buttons.B)) OnButtonB?.Invoke(this,EventArgs.Empty);
        }
        gamPadlast = gamePadcurrent;
    }
}