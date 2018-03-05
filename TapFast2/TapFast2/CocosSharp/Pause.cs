using CocosSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TapFast2.Constants.Files.Images;


namespace TapFast2
{
    public class Pause : CCNode
    {
        CCSprite _pause;
        CCSprite _play;
        CCFadeIn _fadein = new CCFadeIn(0.2f);

        public Pause()
        {
            _pause = new CCSprite(pause);
            _pause.Visible = false;
            _play = new CCSprite(play);
            _play.Visible = true;
            var size = new CCSize(86, 86);
            _pause.ContentSize = size;
            _play.ContentSize = size;
            AddChild(_pause);
            AddChild(_play);

            var touchListener = new CCEventListenerTouchAllAtOnce();
            touchListener.OnTouchesBegan = OnTouchesBegan;
            AddEventListener(touchListener, this);

        }

       

        private void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        {
            if (touches.Count > 0)
            {
                var touch = touches[0];
                if (_pause.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                {
                    PlayPauseTapped();
                }
            }
        }

        public Action<bool> OnTapped;

        private void PlayPauseTapped()
        {
            _play.Visible = !_play.Visible;
            _pause.Visible = !_pause.Visible;

            if (_play.Visible)
            {
                _play.AddAction(_fadein);
                OnTapped?.Invoke(false);
            }
            else
            {
                _pause.AddAction(_fadein);
                OnTapped?.Invoke(true);
            }
        }

        internal void SetPauseVisible(bool visible)
        {
            if (_pause.Visible)
                return;

            _pause.Visible = true;
            _play.Visible = false;

            _pause.AddAction(_fadein);
        }
    }
}
