using System;
using System.Collections.Generic;
using System.Text;

using CocosSharp;
using TapFast2.Services;
using TapFast2.Enums;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using TapFast2.Helpers;
using Box2D.Dynamics;
using Box2D.Collision.Shapes;
using Box2D.Common;
using static TapFast2.Constants.Files.Images;

namespace TapFast2
{
    public abstract class GameLayer : CCLayerColor
    {
        Square _redSignal;
        Square _blueSignal;
        Square _greenSignal;
        Square _yellowSignal;

        Square _redPlay;
        Square _bluePlay;
        Square _greenPlay;
        Square _yellowPlay;


        public Action<int> OnGameIsOver;

        //ISettingsService _settingsService;
        // Define a label variable

        Random _random = new Random();

        List<Square> _squares;
        protected CCProgressTimer _progressTimer;

        List<Square> _signalSquares;        

        protected CordinatesGenerator _cordinatesGenerator;

        public SelectedColor _lastActiveColor;

        //Set from settings
        
        protected float _timeForChangingPositions = 0.2f;
        //Set from settings
        Sound _soundButton;
        protected Pause _pauseButton;
        protected Animator animator;

        public GameLayer(CCSizeI size, CCColor4B backcolor) : base(backcolor)
        {
           
           // _gameType = gameTipe;

            // create and initialize a Label
            //labelScore.VerticalAlignment = CCVerticalTextAlignment.Top;

            
            //CRect rect = new CCRect(50, 50, 200, 200);

            //CCDrawNode tile = new CCDrawNode();
            //var color = new CCColor4F(255, 216, 0, 0.5f);
            //tile.DrawRect(rect, new CCColor4F(color));

            //CCRect rect2 = new CCRect(50, 50 + 200, 200, 200);
            //CCDrawNode tile2 = new CCDrawNode();
            //tile.DrawRect(rect2, CCColor4B.Red);

            // add the label as a child to this Layer
            //int i = 0;
            
            

            

            InitProgressTimer(size);
            
            InitSquares();

            _soundButton = new Sound();
            AddChild(_soundButton);

            _pauseButton = new Pause();
            AddChild(_pauseButton);

            //InitNewGameComponents();

            _pauseButton.OnTapped = OnPausePressed;

        }

        protected abstract void OnPausePressed(bool play);
        

        public abstract void InitNewGameComponents();

        protected abstract void OnSquareTapped(Square obj);

        protected abstract void StartProgressLine();


        private void InitProgressTimer(CCSizeI size)
        {
            _progressTimer = new CCProgressTimer(new CCSprite(bluePx));

            _progressTimer.Type = CCProgressTimerType.Bar;
           // _progressTimer.SkewX
            //_progressTimer.ContentSize = new CCSize(size.Width, 8);
            _progressTimer.Midpoint = new CCPoint(0.5f, 0.5f);
            _progressTimer.ScaleY = 2;
            //Device.OnPlatform(null, Android: () => { _progressTimer.ScaleY = 4; _progressTimer.ScaleX = 2; });

            //_progressTimer.ScaleX = size.Width - 40;
            //_progressTimer.ScaleY = 4;
            //_progressTimer.AnchorPoint = new CCPoint(1.0f, 0.5f);
            //_progressTimer.Percentage = 100.0f;
            _progressTimer.BarChangeRate = new CCPoint(1, 0);
            AddChild(_progressTimer);
        }

        private void InitSquares()
        {
            _squares = GetSquares();
            _signalSquares = _squares.FindAll(a => !a.IsTouchEnabled);

            _playSquares = _squares.FindAll(a => a.IsTouchEnabled);

            foreach (var item in _squares)
            {
                AddChild(item);
            }

            _redSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Red);
            _blueSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Blue);
            _greenSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Green);
            _yellowSignal = _signalSquares.Find(a => a.ColorType == SelectedColor.Yellow);

            _redPlay = _playSquares.Find(a => a.ColorType == SelectedColor.Red);
            _bluePlay = _playSquares.Find(a => a.ColorType == SelectedColor.Blue);
            _greenPlay = _playSquares.Find(a => a.ColorType == SelectedColor.Green);
            _yellowPlay = _playSquares.Find(a => a.ColorType == SelectedColor.Yellow);

            //foreach (var playSquare in _squares.FindAll(a => a.IsTouchEnabled))
            //{
            //    playSquare.OnTapped = OnSquareTapped;
            //}

        }

        protected override void AddedToScene()
        {
            base.AddedToScene();

            initPhysics();

            // Use the bounds to layout the positioning of our drawable assets
            var bounds = VisibleBoundsWorldspace;

            // position the label on the center of the screen
            //labelScore.Position = new CCPoint(80, bounds.Size.Height - 16);

            //labelLives.Position = new CCPoint(bounds.Size.Width - 100, bounds.Size.Height - 16);

            _progressTimer.Position = new CCPoint(bounds.Center.X, bounds.Center.Y);
            //_progressTimer.Midpoint = new CCPoint(0.5f, 0.5f);
            //_progressTimer.ReverseDirection = true;

            _cordinatesGenerator = new CordinatesGenerator(bounds);
            animator = new Animator(_cordinatesGenerator);

            _cordinatesGenerator.SetSquaresCordinates(_squares);

            //_soundButton.Position = new CCPoint(bounds.Size.Width - 90, bounds.Size.Height - 90);
            //_pauseButton.Position = new CCPoint(90, bounds.Size.Height - 90);

            _cordinatesGenerator.SetSoundPosition(_soundButton);
            _cordinatesGenerator.SetPausePosition(_pauseButton);



            var touchListener = new CCEventListenerTouchOneByOne();
            touchListener.IsSwallowTouches = true;
            touchListener.OnTouchBegan = this.OnTouchesBegan;
            //AddEventListener(touchListener, this);
            AddEventListener(touchListener, _redPlay.Current);
            AddEventListener(touchListener.Copy(), _greenPlay.Current);
            AddEventListener(touchListener.Copy(), _yellowPlay.Current);
            AddEventListener(touchListener.Copy(), _bluePlay.Current);

            FirstRun();
        }


        private bool OnTouchesBegan(CCTouch touch, CCEvent touchEvent)
        {
            CCSprite caller = touchEvent.CurrentTarget as CCSprite;


            foreach (var square in _playSquares)
            {
                if (caller == square.Current)
                {
                    if (square.Current.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
                    {
                        System.Diagnostics.Debug.WriteLine(square.ColorType.ToString() + ": is touched ");
                        //currentSpriteTouched = Sprite1;
                        OnSquareTapped(square);
                        return true;  // swallow    
                    }
                    else
                    {
                        return false;  // do not swallow and try the next caller
                    }
                }
                System.Diagnostics.Debug.WriteLine(square.ColorType.ToString() + ": is NOT touched ");
            }
            System.Diagnostics.Debug.WriteLine("Something else was touched");
            return false;  // Do not swallow


            ////currentSpriteTouched = null;
            //if (caller == _redPlay.Current)
            //{
            //    if (_redPlay.Current.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            //    {
            //        System.Diagnostics.Debug.WriteLine("_redPlay 1 touched ");
            //        //currentSpriteTouched = Sprite1;
            //        OnSquareTapped(_redPlay);
            //        return true;  // swallow    
            //    }
            //    else
            //    {
            //        return false;  // do not swallow and try the next caller
            //    }

            //}
            //else if (caller == _greenPlay.Current)
            //{
            //    if (_greenPlay.Current.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            //    {
            //        //currentSpriteTouched = Sprite2;
            //        System.Diagnostics.Debug.WriteLine("_greenPlay 2 touched ");
            //        OnSquareTapped(_greenPlay);

            //        return true;  // swallow    
            //    }
            //    else
            //    {
            //        return false;  // do not swallow and try the next caller
            //    }
            //}
            //else if (caller == _yellowPlay.Current)
            //{
            //    if (_yellowPlay.Current.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            //    {
            //        //currentSpriteTouched = Sprite2;
            //        System.Diagnostics.Debug.WriteLine("_yellowPlay 2 touched ");
            //        OnSquareTapped(_yellowPlay);

            //        return true;  // swallow    
            //    }
            //    else
            //    {
            //        return false;  // do not swallow and try the next caller
            //    }
            //}
            //else if (caller == _bluePlay.Current)
            //{
            //    if (_bluePlay.Current.BoundingBoxTransformedToWorld.ContainsPoint(touch.Location))
            //    {
            //        //currentSpriteTouched = Sprite2;
            //        System.Diagnostics.Debug.WriteLine("_bluePlay 2 touched ");
            //        OnSquareTapped(_bluePlay);
            //        return true;  // swallow    
            //    }
            //    else
            //    {
            //        return false;  // do not swallow and try the next caller
            //    }
            //}
            //else
            //{
            //    // something else touched
            //    System.Diagnostics.Debug.WriteLine("Something else was touched");
            //    return false;  // Do not swallow
            //}
        }

        //private List<Square> GetPlayColorsOrderedByActive()
        //{
        //    //List<Square> result = new List<Square>();
            
        //    //foreach (SelectedColor color in Enum.GetValues(typeof(SelectedColor)))
        //    //{
        //    //    var signal = GetSignalSquare(color);
        //    //    if (signal.IsActive)
        //    //        result.Insert(0, GetPlaySquare(color));
        //    //    else
        //    //        result.Add(GetPlaySquare(color));
        //    //}

        //    //Debug.WriteLine("first square is: " +result[0].ColorType.ToString());
        //    //return result;
        //}

        protected bool HasActiveSquares()
        {
            return _signalSquares.Any(a => a.IsActive);
        }

        protected void StopProgressLine()
        {
            _progressTimer.StopAllActions();
        }

        protected void StartOnce()
        {
            SetAllSignalsInactive();

            //1% chance to get +1
            if (_random.Next(0, 1000) <= 20) // (_random.Next(1, 11) % 2 == 0)
            {
                SetColor(true);
            }

            SetColor();

            
            StartProgressLine();
        }

        public void FirstRun()
        {
            SetAllSignalsInactive();
            SetColor();
        }

        private void SetColor(bool isPlusEnabled = false)
        {
            SelectedColor randomColor = GetRandomColor();
            while (randomColor == _lastActiveColor || GetSignalSquare(randomColor).IsActive)
                randomColor = GetRandomColor();


            SetVisibleSignalColor(randomColor, isPlusEnabled);
        }

        private SelectedColor GetRandomColor()
        {
            int fromOneToFour = _random.Next(1, 5);
            return (SelectedColor)fromOneToFour;
        }

        void SetAllSignalsInactive()
        {
            _greenSignal.IsActive = false;
            _redSignal.IsActive = false;
            _blueSignal.IsActive = false;
            _yellowSignal.IsActive = false;
        }

        private void SetVisibleSignalColor(SelectedColor randomColor, bool plusOnLive = false)
        {
            _lastActiveColor = randomColor;

            var square = GetSignalSquare(randomColor);

            square.IsActive = true;
            if (plusOnLive)
                square.PlusOneEnabled = true;
        }

        protected Square GetSignalSquare(SelectedColor color)
        {
            
            switch (color)
            {
                case SelectedColor.Red:
                    return _redSignal;
                case SelectedColor.Green:
                    return _greenSignal;
                case SelectedColor.Yellow:
                    return _yellowSignal;
                case SelectedColor.Blue:
                    return _blueSignal;
                default:
                    throw new ArgumentException("there is no such color");
            }
            
        }

        protected Square GetPlaySquare(SelectedColor color)
        {

            switch (color)
            {
                case SelectedColor.Red:
                    return _redPlay;
                case SelectedColor.Green:
                    return _greenPlay;
                case SelectedColor.Yellow:
                    return _yellowPlay;
                case SelectedColor.Blue:
                    return _bluePlay;
                default:
                    throw new ArgumentException("there is no such color");
            }

        }

        private List<Square> GetSquares()
        {
            List<Square> squares = new List<Square>();

            squares.Add(CreateSquare(RedSquare, SelectedColor.Red, PositionInGame.SignalUpLeft, false));           
            squares.Add(CreateSquare(GreenSquare, SelectedColor.Green, PositionInGame.SignalUpRight, false));      
            squares.Add(CreateSquare(YellowSquare, SelectedColor.Yellow, PositionInGame.SignalDownLeft, false));   
            squares.Add(CreateSquare(BlueSquare, SelectedColor.Blue, PositionInGame.SignalDownRight, false));                                                                                                                                                                                                                                                                                                                                                                                                                                                                       

            squares.Add(CreateSquare(RedSquare, SelectedColor.Red, PositionInGame.PlayUpLeft, true));
            squares.Add(CreateSquare(GreenSquare, SelectedColor.Green, PositionInGame.PlayUpRight, true));
            squares.Add(CreateSquare(YellowSquare, SelectedColor.Yellow, PositionInGame.PlayDownLeft, true));
            squares.Add(CreateSquare(BlueSquare, SelectedColor.Blue, PositionInGame.PlayDownRight, true));

            return squares;
        }

        private Square CreateSquare(string fileName, SelectedColor color, PositionInGame position, bool isTouchEnabled)
        {
            return new Square(fileName, isTouchEnabled, color, position);
        }

        public void PlayEffect(string sound)
        {
            if (Settings.SoundEnabled)
            {
                CCAudioEngine.SharedEngine.PlayEffect(sound);
                
            }
        }


        //FIX THIS move to another class
        public void WriteGameOver()
        {
            string gameOver = "GAMEOVER";

            int i = 0;
            var gameOverStack = new Queue<KeyValuePair<Square, CCLabel>>();
            foreach (var letter in gameOver)
            {
                var positionInGame = (PositionInGame)i + 1;
                var label = GetLetterLabel(letter);
                var square = _cordinatesGenerator.PositionAndSquare[positionInGame];

                gameOverStack.Enqueue(new KeyValuePair<Square, CCLabel>(square, label));
                //square.AddChild(label);
                //label.Position = new CCPoint(100, 100)

                i++;
            }

            Schedule(a =>
            {
                if (gameOverStack.Count == 0)
                {
                    foreach (var sprite in _cordinatesGenerator.PositionAndSquare.Values)
                        SetPhysicsToSquare(sprite);

                    Schedule(UpdatePhysics);
                    return;
                }
                var item = gameOverStack.Dequeue();
                item.Key.AddChild(item.Value);
            }, 0.10f, 8, 0);
        }

        public CCLabel GetLetterLabel(char letter)
        {
            var label = new CCLabel(letter.ToString(), "Fonts/arial", 36, CCLabelFormat.SpriteFont);
            label.Color = new CCColor3B(27, 161, 226); ;
            return label;
        }
        //FIX THIS move to another class

        //PHYSICS refactor this

        void UpdatePhysics(float dt)
        {
            _world.Step(dt, 8, 1);

            foreach (var sprite in _cordinatesGenerator.PositionAndSquare.Values)
            {
                //var sprite = node as CCPhysicsSprite;

                if (sprite == null)
                    continue;

                if (sprite.Visible && sprite.PhysicsBody.Position.y < 0f)
                {
                    _world.DestroyBody(sprite.PhysicsBody);

                    sprite.Visible = false;
                }
                else
                    sprite.UpdateSprite();
            }
        }

        void SetPhysicsToSquare(Square square)
        {
            // Define the dynamic body.
            //Set up a 1m squared box in the physics world
            b2BodyDef def = new b2BodyDef();
            def.position = new b2Vec2(square.Position.X / PTM_RATIO, square.Position.Y / PTM_RATIO);
            def.type = b2BodyType.b2_dynamicBody;
            b2Body body = _world.CreateBody(def);
            // Define another box shape for our dynamic body.
            var dynamicBox = new b2PolygonShape();
            dynamicBox.SetAsBox(0.5f, 0.5f); //These are mid points for our 1m box

            // Define the dynamic body fixture.
            b2FixtureDef fd = new b2FixtureDef();
            fd.shape = dynamicBox;
            fd.density = 1f;
            fd.friction = 0.3f;
            b2Fixture fixture = body.CreateFixture(fd);

            square.PhysicsBody = body;
            //_world.SetContactListener(new Myb2Listener());

            // _world.Dump();
        }

        public const int PTM_RATIO = 200;
        protected b2World _world;
        private List<Square> _playSquares;
        private CCEventListenerTouchOneByOne touchListenerRed;
        private CCEventListener touchListenerGreen;
        private CCEventListener touchListenerYellow;
        private CCEventListener touchListenerBlue;

        private void initPhysics()
        {
            CCSize s = Layer.VisibleBoundsWorldspace.Size;

            var gravity = new b2Vec2(0.0f, -10.0f);
            _world = new b2World(gravity);
            float debugWidth = s.Width / PTM_RATIO * 2f;
            float debugHeight = s.Height / PTM_RATIO * 2f;

            //CCBox2dDraw debugDraw = new CCBox2dDraw(new b2Vec2(debugWidth / 2f + 10, s.Height - debugHeight - 10), 2);
            //debugDraw.AppendFlags(b2DrawFlags.e_shapeBit);

            //_world.SetDebugDraw(debugDraw);
            _world.SetAllowSleeping(true);
            _world.SetContinuousPhysics(true);

            //m_debugDraw = new GLESDebugDraw( PTM_RATIO );
            //world->SetDebugDraw(m_debugDraw);

            //uint32 flags = 0;
            //flags += b2Draw::e_shapeBit;
            //        flags += b2Draw::e_jointBit;
            //        flags += b2Draw::e_aabbBit;
            //        flags += b2Draw::e_pairBit;
            //        flags += b2Draw::e_centerOfMassBit;
            //m_debugDraw->SetFlags(flags);


            // Call the body factory which allocates memory for the ground body
            // from a pool and creates the ground box shape (also from a pool).
            // The body is also added to the world.
            b2BodyDef def = new b2BodyDef();
            def.allowSleep = true;
            def.position = b2Vec2.Zero;
            def.type = b2BodyType.b2_staticBody;
            b2Body groundBody = _world.CreateBody(def);
            groundBody.SetActive(true);

            // Define the ground box shape.

            // bottom
            b2EdgeShape groundBox = new b2EdgeShape();
            groundBox.Set(b2Vec2.Zero, new b2Vec2(s.Width / PTM_RATIO, 0));
            b2FixtureDef fd = new b2FixtureDef();
            fd.shape = groundBox;
            groundBody.CreateFixture(fd);

            // top
            groundBox = new b2EdgeShape();
            groundBox.Set(new b2Vec2(0, s.Height / PTM_RATIO), new b2Vec2(s.Width / PTM_RATIO, s.Height / PTM_RATIO));
            fd.shape = groundBox;
            groundBody.CreateFixture(fd);

            // left
            groundBox = new b2EdgeShape();
            groundBox.Set(new b2Vec2(0, s.Height / PTM_RATIO), b2Vec2.Zero);
            fd.shape = groundBox;
            groundBody.CreateFixture(fd);

            // right
            groundBox = new b2EdgeShape();
            groundBox.Set(new b2Vec2(s.Width / PTM_RATIO, s.Height / PTM_RATIO), new b2Vec2(s.Width / PTM_RATIO, 0));
            fd.shape = groundBox;
            groundBody.CreateFixture(fd);

            // _world.Dump();
        }
    }
}
