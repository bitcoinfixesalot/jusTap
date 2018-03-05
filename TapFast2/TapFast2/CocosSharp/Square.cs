using System;
using System.Collections.Generic;
using System.Text;
using CocosSharp;
using TapFast2.Enums;
using Box2D.Dynamics;
using Box2D.Common;

namespace TapFast2
{
    public class Square : CCNode
    {
        CCSprite _sprite;
        public CCSprite Current { get { return _sprite; } }

        //signal or play
        public bool IsTouchEnabled { get; private set; }

        public SelectedColor ColorType { get; private set; }

        public PositionInGame CurrentPosition { get; set; }

        public Action<Square> OnTapped;

        CCFadeIn _fadein = new CCFadeIn(0.5f);

        CCLabel _plusOneLabel;

        const byte VISIBLE = 255;
        const byte INACTIVE = 50;

        public bool IsActive
        {
            get { return _sprite.Opacity == VISIBLE; }
            set
            {
                if (!value)
                {
                    _sprite.Opacity = INACTIVE;
                    _plusOneLabel.Visible = false;
                }
                else
                    _sprite.Opacity = VISIBLE;
            }
        }

        public bool PlusOneEnabled
        {
            get
            {
                return _plusOneLabel.Visible;
            }
            set
            {
                _plusOneLabel.Visible = value;
            }
        }

        public Square(string fileName, bool isTouchEnabled, SelectedColor color, PositionInGame position) : base()
        {
            _sprite = new CCSprite(fileName);
            //_square.AnchorPoint = point;

            _sprite.ContentSize = new CCSize(200, 200);
            ColorType = color;
            CurrentPosition = position;

            AddChild(_sprite);


            if (isTouchEnabled)
            {
                IsTouchEnabled = isTouchEnabled;
                //var touchListener = new CCEventListenerTouchAllAtOnce();
                //touchListener.OnTouchesBegan = OnTouchesBegan;

                //AddEventListener(touchListener, this);
            }
            else //signal squares
            {
                //TODO: add +1 to square _sprite.AddChild
                _plusOneLabel = new CCLabel("+1", "Fonts/arial", 36, CCLabelFormat.SpriteFont);
                _plusOneLabel.Position = new CCPoint(100, 100);
                _plusOneLabel.Visible = false;
                _sprite.AddChild(_plusOneLabel, 1);

            }
        }

        //void OnTouchesBegan(List<CCTouch> touches, CCEvent touchEvent)
        //{
        //    if (touches.Count > 0)
        //    {
        //        var touch = touches[0];
        //        if (_sprite.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
        //        {
                    
        //            _sprite.AddAction(_fadein);
        //            if (OnTapped != null)
        //                OnTapped.Invoke(this);
        //        }
        //    }
        //}

        public b2Body PhysicsBody { get; set; }

        public void UpdateSprite()
        {
            if (PhysicsBody == null)
                return;

            b2Vec2 pos = PhysicsBody.Position;

            float x = pos.x * GameLayer.PTM_RATIO;
            float y = pos.y * GameLayer.PTM_RATIO;

            Position = new CCPoint(x, y);
            Rotation = -CCMacros.CCRadiansToDegrees(PhysicsBody.Angle);
        }

        internal void FadeIn()
        {
            _sprite.AddAction(_fadein);
        }
    }
}
