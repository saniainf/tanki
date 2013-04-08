using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TankGame
{
    class drvMenu
    {
        #region основные переменные

        BaseGame baseGame;
        Options options;

        ContentManager content;
        IServiceProvider serviceProvider;

        /// <summary>
        /// селекторы меню
        /// </summary>
        enum SelectorsMenu
        {
            NewGame,
            Options,
            Exit,
        }

        SelectorsMenu currentSelector = SelectorsMenu.NewGame;
        bool menuScreenMove = true;

        public bool PauseGame = false;
        public bool OptionsGame = false;

        RenderTarget2D MenuScreenRender;
        RenderTarget2D BackgroundRender;
        RenderTarget2D BaseRender;

        GraphicsDevice graphicsDevice;

        Rectangle ScreenRectangle;
        Rectangle GameScreenRectangle;

        Texture2D tstTexture;
        Texture2D BaseMenuBgr;
        Texture2D RedButton;
        Texture2D RedButtonCursor;
        Texture2D ButtonCursor;

        Vector2 MenuScreenCoord;

        SpriteFont FontMenu;

        KeyboardState keyboardState;

        float alfa = 0.0f;
        bool alfaswitch = true;

        bool keyDowntriger;
        bool keyUptriger;
      
        #endregion

        #region Конструктор

        public drvMenu(BaseGame baseGame ,GraphicsDevice graphicsDevice, IServiceProvider serviceProvider, Rectangle ScreenRectangle, Rectangle GameScreenRectangle)
        {
            this.baseGame = baseGame;
            this.graphicsDevice = graphicsDevice;
            this.ScreenRectangle = ScreenRectangle;
            this.GameScreenRectangle = GameScreenRectangle;
            this.serviceProvider = serviceProvider;

            content = new ContentManager(serviceProvider, "Content");

            MenuScreenRender = new RenderTarget2D(graphicsDevice, GameScreenRectangle.Width, GameScreenRectangle.Height);
            BackgroundRender = new RenderTarget2D(graphicsDevice, ScreenRectangle.Width, ScreenRectangle.Height);
            BaseRender = new RenderTarget2D(graphicsDevice, ScreenRectangle.Width, ScreenRectangle.Height);

            FontMenu = content.Load<SpriteFont>("Fonts/MenuFont");

            tstTexture = content.Load<Texture2D>("tstTexture");
            BaseMenuBgr = content.Load<Texture2D>("basemenu");
            RedButton = content.Load<Texture2D>("RedButton");
            RedButtonCursor = content.Load<Texture2D>("RedButtonCursor");
            ButtonCursor = content.Load<Texture2D>("ButtonCursor");

            MenuScreenCoord = new Vector2((ScreenRectangle.Width - GameScreenRectangle.Width) / 2, (ScreenRectangle.Height - GameScreenRectangle.Height) / 2);
        }

        #endregion

        #region Update

        public void Update()
        {
            if (!PauseGame && !OptionsGame)
            {
                MenuScreenMove();
                HandleInput();
            }

            else if (OptionsGame)
            {
                options.Update();
            }
        }

        #endregion

        #region Отрисовка

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //---------------------рендер таргет центрального окна
            graphicsDevice.SetRenderTarget(MenuScreenRender);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //фон
            spriteBatch.Draw(BaseMenuBgr, GameScreenRectangle, Color.White);
            //кнопки
            if (!menuScreenMove)
            {
                switch (currentSelector)
                {
                    case SelectorsMenu.NewGame:
                        DrawNewGame(spriteBatch);
                        break;

                    case SelectorsMenu.Options:
                        DrawCursorOptions(spriteBatch);
                        break;

                    case SelectorsMenu.Exit:
                        DrawCursorExit(spriteBatch);
                        break;
                }
            }
            spriteBatch.End();

            //-----------------------рендер таргет бэкграунда
            graphicsDevice.SetRenderTarget(BackgroundRender);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //spriteBatch.Draw(tstTexture, ScreenRectangle, Color.White);
            spriteBatch.End();

            //-----------------------рендер таргет "все в один"
            graphicsDevice.SetRenderTarget(BaseRender);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundRender, ScreenRectangle, Color.White);
            spriteBatch.Draw(MenuScreenRender, MenuScreenCoord, Color.White);
            spriteBatch.End();

            if (!PauseGame && !OptionsGame)
            {
                //-----------------------основной рендер
                graphicsDevice.SetRenderTarget(null);
                graphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();
                spriteBatch.Draw(BaseRender, ScreenRectangle, Color.White);
                spriteBatch.End();
            }

            else if (OptionsGame)
            {
                options.Draw(gameTime, spriteBatch, BaseRender);
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// метод движения окна меню
        /// </summary>
        private void MenuScreenMove()
        {
            if (menuScreenMove)
            {
                if (MenuScreenCoord.Y > ((ScreenRectangle.Height - GameScreenRectangle.Height) / 2))
                    MenuScreenCoord.Y -= 2;
                else
                    menuScreenMove = false;
            }

        }

        /// <summary>
        /// ввод 
        /// </summary>
        private void HandleInput()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space)) menuScreenMove = false;

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switch (currentSelector)
                {
                    case SelectorsMenu.NewGame:
                        
                        break;

                    case SelectorsMenu.Options:
                        OptionsMenu();
                        break;

                    case SelectorsMenu.Exit:
                        baseGame.Exit();
                        break;
                }
            }

            if (!menuScreenMove)
            {
                if (keyboardState.IsKeyUp(Keys.Down)) keyDowntriger = true;
                if (keyDowntriger & currentSelector < SelectorsMenu.Exit)
                {
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        currentSelector += 1;
                        keyDowntriger = false;
                    }
                }

                if (keyboardState.IsKeyUp(Keys.Up)) keyUptriger = true;
                if (keyUptriger & currentSelector > SelectorsMenu.NewGame)
                {
                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        currentSelector -= 1;
                        keyUptriger = false;
                    }
                }
            }
        }

        /// <summary>
        /// создание окна опций
        /// </summary>
        private void OptionsMenu()
        {
            if (!OptionsGame)
            {
                OptionsGame = true;
                options = new Options(baseGame, graphicsDevice, serviceProvider, ScreenRectangle, GameScreenRectangle);
            }
        }

        /// <summary>
        /// отрисовка кнопки NewGame
        /// </summary>
        private void DrawNewGame(SpriteBatch spriteBatch)
        {
            alfa = alfa + 0.05f;
            if (alfa > 180)
                alfa = 0;

            spriteBatch.Draw(RedButton, new Vector2(195, 306), Color.White * ((float)Math.Abs(Math.Sin(alfa))));
            spriteBatch.Draw(RedButtonCursor, new Vector2(236, 347), Color.White);
        }

        /// <summary>
        /// отрисовка курсора опций
        /// </summary>
        private void DrawCursorOptions(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ButtonCursor, new Vector2(237, 397), Color.White);
        }

        /// <summary>
        /// отисовка курсора Exit
        /// </summary>
        private void DrawCursorExit(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ButtonCursor, new Vector2(237, 423), Color.White);
        }

        #endregion
    }
}
