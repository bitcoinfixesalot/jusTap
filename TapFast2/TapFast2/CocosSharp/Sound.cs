using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapFast2.Helpers;
using static TapFast2.Constants.Files.Images;

namespace TapFast2
{
    public class Sound : CCNode
    {
        CCSprite _soundOn;
        CCSprite _soundOff;
        CCFadeIn _fadein = new CCFadeIn(0.2f);

        //CCSequence changeActive;


        public Sound()
        {
            _soundOn = new CCSprite(sound);
            _soundOff = new CCSprite(mute);
            var size = new CCSize(86, 86);
            _soundOn.ContentSize = size;
            _soundOff.ContentSize = size;
            AddChild(_soundOn);
            AddChild(_soundOff);

            var soundEnabled = Settings.SoundEnabled;
            SetActiveSprite(soundEnabled);

            //var moveCompletedAction = new CCCallFunc(SoundPressed);


            //changeActive = new CCSequence(_fadein, moveCompletedAction);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouchesBegan;
            AddEventListener(touchListener, this);

        }

        private void SoundPressed()
        {
            if (Settings.SoundEnabled)
            {
                Settings.SoundEnabled = false;
                SetActiveSprite(false);
            }
            else
            {
                Settings.SoundEnabled = true;
                SetActiveSprite(true);
            }
        }

        private void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                var touch = touches[0];
                if (_soundOn.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    SoundPressed();
                }
            }
        }

        private void SetActiveSprite(bool soundEnabled)
        {
            _soundOn.Visible = soundEnabled;
            _soundOff.Visible = !soundEnabled;
            if (soundEnabled)
                _soundOn.AddAction(_fadein);
            else
                _soundOff.AddAction(_fadein);
        }

        CCSprite GetActiveSprite()
        {
            if (_soundOff.Visible)
                return _soundOff;
            else
                return _soundOn;
        }

        //private bool soundOn;

        //public bool SoundOn
        //{
        //    get { return soundOn; }
        //    set { soundOn = value; }
        //}

    }
}
