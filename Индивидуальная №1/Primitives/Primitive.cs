using System.Drawing;

namespace affine_transformations.Primitives
{
    interface Primitive
    {
        void Draw(Graphics g, bool selected);
        void Apply(Transformation t);
    }
}
