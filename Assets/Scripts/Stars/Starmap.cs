using System.Linq;
using Flai;
using Flai.Diagnostics;
using UnityEngine;

namespace Assets.Scripts.Stars
{
    [ExecuteInEditMode]
    public class Starmap : FlaiScript
    {
        public int Width = 10;
        public int Height = 10;

        public float MinScale = 5;
        public float MaxScale = 25;
        public float Density = 0.5f;
        public Sprite Sprite;

        private RectangleF Bounds
        {
            get { return RectangleF.CreateCentered(this.Position2D, new SizeF(this.Width, this.Height)); }
        }

        protected override void LateUpdate()
        {
            FlaiDebug.DrawRectangleOutlines(this.Bounds, ColorF.White);
            this.LocalPosition2D = -this.Parent.GetPosition2D() / 40f;
            this.Transform.SetPositionZ(0);
        }

        public void Generate()
        {
            this.Reset();
            int count = (int)(this.Width * this.Height * this.Density);
            for (int i = 0; i < count; i++)
            {
                Vector2f position = new Vector2f(
                    Global.Random.NextFloat(this.Bounds.Left, this.Bounds.Right),
                    Global.Random.NextFloat(this.Bounds.Bottom, this.Bounds.Top));

                float scale = Global.Random.NextFloat(this.MinScale, this.MaxScale);
                this.CreateStar(position, scale, ColorF.White);
               // if (Global.Random.NextFromOdds(0.5f))
                {
               //     this.CreateStar(position - Vector2f.One * scale * 0.25f, scale, new ColorF(32, 32, 255)).Get<SpriteRenderer>().sortingOrder = -1000;
                }
            }
        }

        private GameObject CreateStar(Vector2f position, float scale, ColorF color)
        {
            var star = new GameObject("Star");
            star.SetParent(this.GameObject);
            star.SetPosition2D(position);
            star.SetScale2D(scale);

            SpriteRenderer spriteRenderer = star.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = this.Sprite;
            spriteRenderer.color = color;
            spriteRenderer.sortingOrder = -100;

            return star;
        }

        public void Reset()
        {
            while (this.Children.Any())
            {
                this.Children.First().DestroyImmediateIfNotNull();
            }
        }
    }
}