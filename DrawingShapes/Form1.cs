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
        private Point _drawingStartPoint; //図形の描画開始点
        private Point _draggingPoint; //図形の移動点

        public DrawingShapesForm()
        {
            _shapeFactories = new ISelectableShapeFactory[]
            {
                new DraggableRoundFactory("楕円形"),
                new DraggableSquareFactory("四角形")
            };

            InitializeComponent();
            RegisterShapeRadioButton();
            RegisterColorsPanel();
        }

        private void RegisterColorsPanel()
        {

        }

        private void InitializeBackGround()
        {
            const int tileSize = 40;
            var width = mainPictureBox.Size.Width;
            var height = mainPictureBox.Size.Height;

            var checkerboardCheckFactory
                = new CheckerboardCheckFactory(widthCanvas: width,
                    heightCanvas: height, 
                    tileSize: tileSize
                );
            _shapes.Add(checkerboardCheckFactory.Create(0, 0));

        }

        private void RegisterShapeRadioButton()
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

            var topPosition = (splitContainer1.Panel1.Height - panel1.Height) / 2;
            panel1.Top = topPosition;


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
            //マウスの左が押されたときは描画モード
            if ((e.Button & MouseButtons.Left) != 0)
            {
                // 描画開始点の保存
                _drawingStartPoint.X = e.X;
                _drawingStartPoint.Y = e.Y;

                // 描画する図形を作成する
                var selectedShapeFactory = panel1.Controls
                    .OfType<CustomRadioButton>()
                    .Where(item => item.Checked)
                    .Select(item => item.ShapeFactory)
                    .First();

                if (selectedShapeFactory.Create(e.X, e.Y, 0, 0) is IDraggableShape shape)
                {
                    shape.Preview = true;
                    _shapes.Add(shape);
                }
            }

            //マウスの右が押されたときは移動モード
            if ((e.Button & MouseButtons.Right) != 0)
            {
                var draggableShapes = _shapes
                    .OfType<IDraggableShape>()
                    .LastOrDefault(item => item.Contains(e.X, e.Y));

                if (draggableShapes != null)
                {
                    draggableShapes.Selected = true;
                    _draggingPoint = new Point(e.X, e.Y);
                }
            }

            mainPictureBox.Refresh();
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            // マウスの左が押されているとき
            if ((e.Button & MouseButtons.Left) != 0)
            {
                var drawingShape = _shapes.OfType<IDraggableShape>().FirstOrDefault(item => item.Preview);

                if (drawingShape != null)
                {
                    drawingShape.Resize(_drawingStartPoint, new Point(e.X, e.Y));
                }
            }

            //マウスの右が押されているときは移動モード
            if ((e.Button & MouseButtons.Right) != 0)
            {
                var draggableShapes = _shapes
                    .OfType<IDraggableShape>()
                    .FirstOrDefault(item => item.Selected);

                if (draggableShapes == null)
                {
                    return;
                }

                var subtract = Point.Subtract(e.Location, new Size(_draggingPoint));
                draggableShapes.Point = Point.Add(draggableShapes.Point, new Size(subtract));
                //var newPoint = new Point(draggableShapes.Point.X + (e.X - _draggingPoint.X), draggableShapes.Point.Y + (e.Y - _draggingPoint.Y));

                _draggingPoint = new Point(e.X, e.Y);
            }

            mainPictureBox.Refresh();
        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {

            if ((e.Button & MouseButtons.Left) != 0)
            {
                var drawingShape = _shapes.OfType<IDraggableShape>().FirstOrDefault(item => item.Preview);

                var noSize = new Size(0, 0);
                var deleteShape = _shapes.OfType<IDraggableShape>().ToList()
                    .RemoveAll(shape => shape.Size.Equals(noSize) && shape.Preview);

                if (drawingShape != null)
                {
                    drawingShape.Resize(_drawingStartPoint, new Point(e.X, e.Y));
                    drawingShape.Preview = false;
                }

            }

            if ((e.Button & MouseButtons.Right) != 0)
            {
                var draggableShapes = _shapes
                    .OfType<IDraggableShape>();

                foreach (var draggableShape in draggableShapes)
                {
                    draggableShape.Selected = false;
                }
            }

            _draggingPoint = new Point();
            _drawingStartPoint = new Point();

            mainPictureBox.Refresh();
        }

        private void DrawingShapesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
