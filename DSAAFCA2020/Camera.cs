using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAAFCA2020
{
    public class Camera
    {
        public Matrix Transform { get; set; }

        public void Follow(Vector2 target)
        {
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y,
                0);

            var offset = Matrix.CreateTranslation(210, 100, 0);

            Transform = position * offset;
        }

    }
}
