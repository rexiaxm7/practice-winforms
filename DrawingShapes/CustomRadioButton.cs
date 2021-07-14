using System.Windows.Forms;
using DrawingShapes.ShapeFactories;

namespace DrawingShapes
{
    public class CustomRadioButton : RadioButton
    {
        public ISelectableShapeFactory ShapeFactory { get; }


        public CustomRadioButton(ISelectableShapeFactory shapeFactory) : base()
        {
            ShapeFactory = shapeFactory;
            Text = ShapeFactory.Name();
        }

    }
}