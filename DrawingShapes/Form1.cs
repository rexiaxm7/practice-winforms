using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DrawingShapes.ColorSelector;
using DrawingShapes.ShapeFactories;
using DrawingShapes.Shapes;

namespace DrawingShapes
{
    public partial class DrawingShapesForm : Form
    {
        private readonly List<IShape> _shapes = new List<IShape>();
        private readonly ISelectableShapeFactory[] _shapeFactories;

        public DrawingShapesForm()
        {
            var orderColorSelector = new OrderColorSelector();
            _shapeFactories = new ISelectableShapeFactory[]
            {
                new RoundFactory(new RandomColorSelector(), "丸"),
                new TriangleFactory(orderColorSelector, "三角形"),
                new SquareFactory(orderColorSelector, "四角形"),
                new OriginalShapeFactory("オリジナル"),
                new DraggableSquareFactory("枠付き四角形")
            };

            InitializeComponent();
            InitializeBackGround();
            RegisterShareRadioButton();

        }

        private void InitializeBackGround()
        {
            int tileSize = 40;
            int width = mainPictureBox.Size.Width;
            int height = mainPictureBox.Size.Height;

            CheckerboardCheckFactory checkerboardCheckFactory
                = new CheckerboardCheckFactory(widthCanvas: width,
                    heightCanvas: height, 
                    tileSize: tileSize
                );
            _shapes.Add(checkerboardCheckFactory.Create(0, 0));

        }

        private void RegisterShareRadioButton()
        {
            for (var index = 0; index < _shapeFactories.Length; index++)
            {
                var shapeFactory = _shapeFactories[index];
                
                var radioButton = new CustomRadioButton(shapeFactory)
                {
                    AutoSize = true,
                };

                if (index == 0)
                {
                    radioButton.Left = 0;
                    radioButton.Checked = true;
                }
                else
                {
                    var preControl = panel1.Controls[index - 1];
                    radioButton.Left = preControl.Right;
                }

                panel1.Controls.Add(radioButton);
            }

            int topPosition = (splitContainer1.Panel1.Height - panel1.Height) / 2;
            panel1.Top = topPosition;


        }

        private void mainPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
         
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            
            foreach (var shape in _shapes)
            {
                shape.Draw(e.Graphics);
            }

        }

        private void allDeleteButton_Click(object sender, EventArgs e)
        {
            var deletableShapes = _shapes
                .OfType<IDeletableShape>()
                .ToList();

            if (!deletableShapes.Any())
            {
                MessageBox.Show("削除できる要素がありません。");
                return;
            }

            _shapes.RemoveAll(shape => deletableShapes.Contains(shape));

            mainPictureBox.Refresh();
        }

        private void FileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JEPG形式|*.jpeg|PNG形式|*.png";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            try
            {
                using (var saveImage = new Bitmap(mainPictureBox.Width, mainPictureBox.Height))
                using (var g = Graphics.FromImage(saveImage))
                {
                    foreach (var shape in _shapes)
                    {
                        shape.Draw(g);
                    }

                    string extension = Path.GetExtension(saveFileDialog1.FileName);

                    switch (extension.ToUpper())
                    {
                        case ".JPEG":
                            saveImage.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                            break;
                        case ".PNG":
                            saveImage.Save(saveFileDialog1.FileName, ImageFormat.Png);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。再試行してください。" + ex.StackTrace);
            }
            
        }

        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {

            var draggableShapes = _shapes
                .OfType<IDraggableShape>()
                .LastOrDefault(item => item.Contains(e.X, e.Y));

            if ((e.Button & MouseButtons.Right) != 0 && draggableShapes != null)
            {
                draggableShapes.Selected = true;
            }
            else
            {
                // クリック箇所にドラッグ可能なShapeがなかった時
                var selectedShapeFactory = panel1.Controls
                    .OfType<CustomRadioButton>()
                    .Where(item => item.Checked)
                    .Select(item => item.ShapeFactory)
                    .First();

                var shape = selectedShapeFactory.Create(e.X, e.Y);
                _shapes.Add(shape);
            }

            mainPictureBox.Refresh();
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                var draggableShapes = _shapes
                    .OfType<IDraggableShape>();

                foreach (var draggableShape in draggableShapes)
                {
                    draggableShape.Selected = false;
                }
            }

            mainPictureBox.Refresh();
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var draggableShapes = _shapes
                .OfType<IDraggableShape>()
                .FirstOrDefault(item => item.Selected);

            if (draggableShapes == null)
            {
                return;
            }

            draggableShapes.ChangePosition(e.X,e.Y);
            mainPictureBox.Refresh();
        }
    }
}
