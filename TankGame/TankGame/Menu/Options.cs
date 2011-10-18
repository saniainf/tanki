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
    class Options
    {
        #region основные переменные

        BaseGame baseGame;

        ContentManager content;

        /// <summary>
        /// селекторы выбора опций
        /// </summary>
        enum SelectorsOptions
        {
            VolumeMusic,
            VolumeSound,
        }

        SelectorsOptions currentSelector = SelectorsOptions.VolumeMusic;
        bool ScreenMovetriger = true;

        RenderTarget2D OptionsRender;
        RenderTarget2D BackgroundRender;

        Effect blurEffect;

        GraphicsDevice graphicsDevice;

        Rectangle ScreenRectangle;
        Rectangle GameScreenRectangle;

        //Texture2D tstTexture;
        Texture2D OptionsMenuBgr;
        //Texture2D RedButton;
        //Texture2D RedButtonCursor;
        //Texture2D ButtonCursor;

        Vector2 ScreenCoord;

        //SpriteFont FontMenu;

        KeyboardState keyboardState;

        bool keyDowntriger;
        bool keyUptriger;
        bool keyLefttriger;
        bool keyRighttriger;

        #endregion

        #region Конструктор

        public Options(BaseGame baseGame, GraphicsDevice graphicsDevice, IServiceProvider serviceProvider, Rectangle ScreenRectangle, Rectangle GameScreenRectangle)
        {
            this.baseGame = baseGame;
            this.graphicsDevice = graphicsDevice;
            this.ScreenRectangle = ScreenRectangle;
            this.GameScreenRectangle = GameScreenRectangle;

            content = new ContentManager(serviceProvider, "Content");

            OptionsRender = new RenderTarget2D(graphicsDevice, GameScreenRectangle.Width, GameScreenRectangle.Height);
            BackgroundRender = new RenderTarget2D(graphicsDevice, ScreenRectangle.Width, ScreenRectangle.Height);

            //FontMenu = content.Load<SpriteFont>("Fonts/MenuFont");

            //tstTexture = content.Load<Texture2D>("tstTexture");
            OptionsMenuBgr = content.Load<Texture2D>("Options");
            //RedButton = content.Load<Texture2D>("RedButton");
            //RedButtonCursor = content.Load<Texture2D>("RedButtonCursor");
            //ButtonCursor = content.Load<Texture2D>("ButtonCursor");

            blurEffect = content.Load<Effect>("Effect");

            ScreenCoord = new Vector2((ScreenRectangle.Width - GameScreenRectangle.Width) / 2, ScreenRectangle.Bottom);
        }

        #endregion

        #region Update

        public void Update()
        {
            ScreenMove();
            HandleInput();
        }

        #endregion

        #region Отрисовка

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, RenderTarget2D InputRender)
        {
            //---------------------рендер таргет центрального окна
            graphicsDevice.SetRenderTarget(OptionsRender);
            graphicsDevice.Clear(Color.Transparent);

            spriteBatch.Begin();
            //фон
            //spriteBatch.Draw(BaseMenuBgr, GameScreenRectangle, Color.White);
            ////кнопки
            //if (!menuScreenMove)
            //{
            //    switch (currentSelector)
            //    {
            //        case SelectorsMenu.NewGame:
            //            DrawNewGame(spriteBatch);
            //            break;

            //        case SelectorsMenu.Options:
            //            DrawCursorOptions(spriteBatch);
            //            break;

            //        case SelectorsMenu.Exit:
            //            DrawCursorExit(spriteBatch);
            //            break;
            //    }
            //}
            spriteBatch.Draw(OptionsMenuBgr, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            //-----------------------рендер таргет бэкграунда
            graphicsDevice.SetRenderTarget(BackgroundRender);
            graphicsDevice.Clear(Color.Black);
            blurEffect.CurrentTechnique = blurEffect.Techniques["Blur"];

            spriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, blurEffect);
            spriteBatch.Draw(InputRender, ScreenRectangle, Color.White);
            spriteBatch.End();

            //-----------------------основной рендер
            graphicsDevice.SetRenderTarget(null);
            graphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundRender, ScreenRectangle, Color.White);
            spriteBatch.Draw(OptionsRender, ScreenCoord, Color.White);
            spriteBatch.End();
        }

        #endregion

        #region Вспомогательные методы

        private void ScreenMove()
        {
            if (ScreenMovetriger)
            {
                if (ScreenCoord.Y > ((ScreenRectangle.Height - GameScreenRectangle.Height) / 2))
                    ScreenCoord.Y -= 56;
                else
                    ScreenMovetriger = false;
            }
        }

        private void HandleInput()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space))
            {

            }

            //if (keyboardState.IsKeyDown(Keys.Enter))
            //{
            //    switch (currentSelector)
            //    {
            //        case SelectorsMenu.NewGame:

            //            break;

            //        case SelectorsMenu.Options:

            //            break;

            //        case SelectorsMenu.Exit:
            //            baseGame.Exit();
            //            break;
            //    }
            //}

            //if (!menuScreenMove)
            //{
            //    if (keyboardState.IsKeyUp(Keys.Down)) keydowntriger = true;
            //    if (keydowntriger & currentSelector < SelectorsMenu.Exit)
            //    {
            //        if (keyboardState.IsKeyDown(Keys.Down))
            //        {
            //            currentSelector += 1;
            //            keydowntriger = false;
            //        }
            //    }

            //    if (keyboardState.IsKeyUp(Keys.Up)) keyuptriger = true;
            //    if (keyuptriger & currentSelector > SelectorsMenu.NewGame)
            //    {
            //        if (keyboardState.IsKeyDown(Keys.Up))
            //        {
            //            currentSelector -= 1;
            //            keyuptriger = false;
            //        }
            //    }
            //}
        }

        //private void DrawNewGame(SpriteBatch spriteBatch)
        //{
        //    if (alfaswitch)
        //    {
        //        alfa = alfa + 0.03f;
        //        if (alfa >= 0.9f)
        //            alfaswitch = false;
        //    }

        //    if (!alfaswitch)
        //    {
        //        alfa = alfa - 0.03f;
        //        if (alfa <= 0.01f)
        //            alfaswitch = true;
        //    }

        //    spriteBatch.Draw(RedButton, new Vector2(195, 306), Color.White * alfa);
        //    spriteBatch.Draw(RedButtonCursor, new Vector2(236, 347), Color.White);
        //}

        //private void DrawCursorOptions(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(ButtonCursor, new Vector2(237, 397), Color.White);
        //}

        //private void DrawCursorExit(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(ButtonCursor, new Vector2(237, 423), Color.White);
        //}

        #endregion
    }
}
