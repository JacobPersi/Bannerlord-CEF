using TaleWorlds.TwoDimension;

namespace BannerlordCEF {
    public class SpriteTexture : Sprite {

        public override Texture Texture {
            get {
                return this._texture;
            }
        }

        public SpriteTexture(Texture texture, int width, int height) : base("Sprite", width, height) {
            this._texture = texture;
            this._vertices = new float[8];
            this._uvs = new float[8];
            this._indices = new uint[6];
            this._indices[0] = 0U;
            this._indices[1] = 1U;
            this._indices[2] = 2U;
            this._indices[3] = 0U;
            this._indices[4] = 2U;
            this._indices[5] = 3U;
        }

        private Texture _texture;
        private float[] _vertices;
        private float[] _uvs;
        private uint[] _indices;

        public override float GetScaleToUse(float width, float height, float scale) {
            return scale;
        }

        protected override DrawObject2D GetArrays(SpriteDrawData spriteDrawData) {
            return DrawObject2D.CreateQuad(new System.Numerics.Vector2(this._texture.Width, this._texture.Height));
        }
    }
}
