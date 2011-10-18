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
    /// <summary>
    /// �������� ����� ���� � ��������� �����������
    /// </summary>
    public class BaseGame : Microsoft.Xna.Framework.Game
    {
        #region �������� ����������

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// ������ ��������� ����
        /// </summary>
        Rectangle ScreenRectangle;

        /// <summary>
        /// ������ �������� ����
        /// </summary>
        Rectangle GameScreenRectangle;

        /// <summary>
        /// ���������
        /// </summary>
        enum Selectors
        {
            Menu,
            Game,
        }

        Selectors currentSelector = 0;

        /// <summary>
        /// ������ ������ ����
        /// </summary>
        drvMenu drvMenu;

        /// <summary>
        /// ������ ������ ����
        /// </summary>

        
        #endregion

        #region �������� ������

        public BaseGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenRectangle = new Rectangle(0, 0, 1152, 720);
            GameScreenRectangle = new Rectangle(0, 0, 624, 624);

            graphics.PreferredBackBufferWidth = ScreenRectangle.Width;
            graphics.PreferredBackBufferHeight = ScreenRectangle.Height;
        }

        /// <summary>
        /// �������������
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //������ �������� ����
            LoadMenuGame();
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// �������� ������ ����
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            drvMenu.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// �������� ���������� ����
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //switch (currentSelector)
            //{
            //    case Selectors.Menu:
            //        GraphicsDevice.Clear(Color.CornflowerBlue);
            //        break;

            //    case Selectors.Game:
            //        GraphicsDevice.Clear(Color.Black);
            //        break;
            //}

            drvMenu.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }

        #endregion

        #region ��������������� ������

        void LoadMenuGame()
        {
            drvMenu = new drvMenu(this, GraphicsDevice, Services, ScreenRectangle, GameScreenRectangle);
        }

        #endregion
    }
}
