using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCB_Explorer
{
    public partial class Form1 : Form
    {
        int borderLeft = 100;
        int imageSpace = 10;
        int borderTop = 50;
        string dataPath = Path.Combine(Application.StartupPath,  @"..\..\..\data");
        string cfgFile = Path.Combine(Application.StartupPath, @"..\..\..\data\cfg.json");

        Image image1;
        Image image2;
        Pen redPen = new Pen(Color.Red, 1);
        Pen selPen = new Pen(Color.Green, 1);

        Config cfg;

        int selX = 0;
        int selY = 0;

        public Form1()
        {
            InitializeComponent();
            cfg = Config.Load(cfgFile);
            nuScale.Value = (decimal)cfg.scale;
            nuScaleX.Value = (decimal)cfg.scaleX;
            nuScaleY.Value = (decimal)cfg.scaleY;
            nuOffsetX.Value = (decimal)cfg.offsetX;
            nuOffsetY.Value = (decimal)cfg.offsetY;
            populateItemList();
            image1 = Image.FromFile(Path.Combine(dataPath, "A.png"));
            image2 = Image.FromFile(Path.Combine(dataPath, "B.png"));
        }

        private void populateItemList()
        {
            lbItems.Items.Clear();
            foreach (var i in cfg.items)
            {
                lbItems.Items.Add(i.name);
            }
        }

        private void updt()
        {
            Invalidate();
        }


        private void drawImage(Image img, int x, int y, float scaleW, float scaleH, PaintEventArgs e)
        {
            RectangleF destinationRect = new RectangleF(
                    x,
                    y,
                    scaleW * image2.Width,
                    scaleH * image2.Height);
            e.Graphics.DrawImage(img, destinationRect);
        }

        private void drawPointer(Pen pen, int x, int y, PaintEventArgs e)
        {
            int w = 10;
            e.Graphics.DrawLine(pen, x - w, y, x + w, y);
            e.Graphics.DrawLine(pen, x, y-w, x, y+w);
        }

        private int getMirroredX(int x)
        {
            int imgWidth = (int)(image1.Width * cfg.scale);
            return (imgWidth - x);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawImage(image1, borderLeft, borderTop, cfg.scale, cfg.scale, e);

            int imgWidth = (int)(image1.Width * cfg.scale);
            int imgBx = borderLeft + imageSpace + imgWidth;
            drawImage(image2, imgBx + cfg.offsetX, borderTop + cfg.offsetY, cfg.scale * cfg.scaleX, cfg.scale* cfg.scaleY, e);

            drawPointer(redPen, mouseX, mouseY, e);
            int mouseXoffset = mouseX - borderLeft;

            int x2 = imgBx + (imgWidth - mouseXoffset);

            drawPointer(redPen, borderLeft + imgWidth + imageSpace + getMirroredX(mouseXoffset), mouseY, e);

            drawPointer(selPen, borderLeft + selX, borderTop + selY, e);
            drawPointer(selPen, borderLeft + imgWidth + imageSpace + getMirroredX(selX), borderTop + selY, e);
        }

        int mouseX = 0;
        int mouseY = 0;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseX = e.X;
                mouseY = e.Y;
                updt();
            }
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            cfg.Save(cfgFile);
        }

        private void nuScale_ValueChanged(object sender, EventArgs e)
        {
            cfg.scale = (float)nuScale.Value;
            updt();
        }

        private void nuOffsetX_ValueChanged(object sender, EventArgs e)
        {
            cfg.offsetX = (int)nuOffsetX.Value;
            updt();
        }

        private void nuOffsetY_ValueChanged(object sender, EventArgs e)
        {
            cfg.offsetY = (int)nuOffsetY.Value;
            updt();
        }

        private void nuScaleX_ValueChanged(object sender, EventArgs e)
        {
            cfg.scaleX = (float)nuScaleX.Value;
            updt();
        }

        private void nuScaleY_ValueChanged(object sender, EventArgs e)
        {
            cfg.scaleY = (float)nuScaleY.Value;
            updt();
        }

        private void btnItemAdd_Click(object sender, EventArgs e)
        {
            Item i = new Item();
            i.name = tbItem.Text;
            i.x = mouseX - borderLeft;
            i.y = mouseY - borderTop;
            cfg.items.Add(i);
            populateItemList();
        }

        private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = lbItems.SelectedIndex;
            if (i >= 0)
            {
                selX = cfg.items[i].x;
                selY = cfg.items[i].y;
            }
            updt();
        }
    }
}
