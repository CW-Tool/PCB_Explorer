using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        int borderLeft = 150;
        int imageSpace = 10;
        int borderTop = 10;
        string dataPath = Path.Combine(Application.StartupPath,  @"..\..\..\data");
        string cfgFile = Path.Combine(Application.StartupPath, @"..\..\..\data\cfg.json");
        Point imgPosOffset;
        Point mouseDown;
        enum Side_t { eLeft, eRight }; 

        Image image1;
        Image image2;
        Pen redPen = new Pen(Color.Red, 2);
        Pen highlightedPen = new Pen(Color.Yellow, 2);
        Pen selContactsPen = new Pen(Color.Cyan, 2);

        Config cfg;

        IList<Contact> selContacts = null;
        PointF highlightedPos;
        string highlightedSide = "";

        Point mouse;
        string mouseB = "";


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
            this.MouseWheel += Form_MouseWheel;
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            float delta = 0.03f;
            if(e.Delta<0)
            {
                delta = -delta;
            }

            nuScale.Value += (decimal)delta;
        }

        private void populateItemList()
        {
            lbItems.Items.Clear();
            foreach (var i in cfg.items)
            {
                lbItems.Items.Add(i.name);
            }
        }

        private void populateContactList()
        {
            highlightedSide = "";
            lbContact.Items.Clear();
            selContacts = null;
            Item i = getSelectedItem();
            if (null != i)
            {
                selContacts = i.contacts;
                if (null != i.contacts)
                {
                    foreach (var c in i.contacts)
                    {
                        lbContact.Items.Add(c.b);
                    }
                }
            }
            updt();
        }

        private void updt()
        {
            Invalidate();
        }


        private void drawImage(Image img, float x, float y, float scaleW, float scaleH, PaintEventArgs e)
        {
            RectangleF destinationRect = new RectangleF(
                    x,
                    y,
                    scaleW * img.Width,
                    scaleH * img.Height);
            e.Graphics.DrawImage(img, destinationRect);
        }
        

        private void drawMaker(Side_t side, Pen pen, PointF point, PaintEventArgs e)
        {
            float y = point.Y + getImgY();
            float x;

            if (side == Side_t.eLeft)
            {
                x = getLeftImgX() + point.X;
            }
            else
            {
                x = getRightImgX() + getMirroredX(point.X);
            }

            int w = 10;
            e.Graphics.DrawLine(pen, x - w, y, x + w, y);
            e.Graphics.DrawLine(pen, x, y - w, x, y + w);
        }


        private void drawMaker(Side_t side, Pen pen, Point point, PaintEventArgs e)
        {
            PointF p = new PointF(point.X, point.Y);
            drawMaker(side, pen, p, e);
        }


        private float getScaledLeftImageWidth()
        {
            return image1.Width * cfg.scale;
        }


        private float getScaledRightImageWidth()
        {
            return image2.Width * cfg.scale * cfg.scaleX;
        }


        private float getScaling()
        {
            return cfg.scale;
        }


        private float getMirroredX(float x)
        {
            float imgWidth = (float)(image1.Width * getScaling());
            return (imgWidth - x);
        }
        

        private float getImgY()
        {
            return imgPosOffset.Y + borderTop;
        }


        private float getLeftImgX()
        {
            return imgPosOffset.X + borderLeft;
        }


        private float getRightImgX()
        {
            return getLeftImgX() + getScaledLeftImageWidth() + imageSpace;
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawImage(image1, getLeftImgX(), getImgY(), getScaling(), getScaling(), e);
            drawImage(image2, getRightImgX() + cfg.offsetX, getImgY() + cfg.offsetY, getScaling() * cfg.scaleX, getScaling() * cfg.scaleY, e);
            

            drawMaker(Side_t.eLeft, redPen, mouse, e);
            drawMaker(Side_t.eRight, redPen, mouse, e);


            if (null != selContacts)
            {
                PointF point = new Point();
                foreach (var c in selContacts)
                {
                    point.X = c.x * getScaling();
                    point.Y = c.y * getScaling();
                    if (c.b == "l" || c.b == "b")
                        drawMaker(Side_t.eLeft, selContactsPen, point, e);
                    if (c.b == "r" || c.b == "b")
                        drawMaker(Side_t.eRight, selContactsPen, point, e);
                }
            }

            if (highlightedSide == "l" || highlightedSide == "b")
                drawMaker(Side_t.eLeft, highlightedPen, highlightedPos, e);

            if (highlightedSide == "r" || highlightedSide == "b")
                drawMaker(Side_t.eRight, highlightedPen, highlightedPos, e);
        }
        
        
        private void highlightItem(Item item)
        {
            if (null != item)
            {
                int k = 0;
                foreach (var i in cfg.items)
                {
                    if (item.name == i.name)
                    {
                        lbItems.SelectedIndex = k;
                        break;
                    }
                    k++;
                }
            }
            else
            {
                lbItems.SelectedIndex = -1;
            }
        }

        private int getUnscaledX(MouseEventArgs e)
        {
            int x = (int)(((e.X - borderLeft) - imgPosOffset.X) / getScaling());
            if (x > image1.Width)
            {
                x = (int)(((e.X - borderLeft - imageSpace) - imgPosOffset.X) / getScaling());
                x = image1.Width - (x - image1.Width);
            }
            return x;
        }


        private int getUnscaledY(MouseEventArgs e)
        {
            int y = (int)(((e.Y - borderTop) - imgPosOffset.Y) / getScaling());
            return y;
        }


        private void updateMouseEvent(MouseEventArgs e)
        {
            int x = (int)(((e.X - borderLeft) - imgPosOffset.X) / getScaling());
            int y = (int)(((e.Y - borderTop) - imgPosOffset.Y) / getScaling());

            string clickedSide = (x < image1.Width) ? "l" : "r";

            if (e.Button == MouseButtons.Left)
            {
                Item i = findItemAtPos(getUnscaledX(e), getUnscaledY(e), clickedSide);
                highlightItem(i);
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouse.X = (e.X - borderLeft) - imgPosOffset.X;
                mouse.Y = (e.Y - borderTop) - imgPosOffset.Y;
                if(clickedSide=="r")
                {
                    mouse.X -= (imageSpace + (int)(image1.Width * getScaling()));
                    float imgWidth = (float)(image1.Width * getScaling());
                    mouse.X = (int)(imgWidth - mouse.X);
                }
                mouseB = clickedSide;
                updt();
            }
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                imgPosOffset.X -= (mouseDown.X - e.X);
                imgPosOffset.Y -= (mouseDown.Y - e.Y);
                updt();
            }
            mouseDown.X = e.X;
            mouseDown.Y = e.Y;
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            updateMouseEvent(e);
        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            updateMouseEvent(e);
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
            cfg.items.Add(i);
            populateItemList();
            tbItem.Text = "";
            if(lbItems.Items.Count>0)
                lbItems.SelectedIndex = lbItems.Items.Count - 1;
        }


        private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateContactList();
        }


        private Item getSelectedItem()
        {
            Item ret = null;
            int i = lbItems.SelectedIndex;
            if (i >= 0)
            {
                ret = cfg.items[i];
            }
            return ret;
        }


        private void addContact(string b)
        {
            Item i = getSelectedItem();
            if (null != i)
            {
                if(null == i.contacts)
                {
                    Contact c = new Contact(0,0,"");
                    Type t = typeof(List<>).MakeGenericType(c.GetType());
                    i.contacts = (IList<Contact>)Activator.CreateInstance(t);
                }
                i.contacts.Add(new Contact(mouse.X / getScaling(), mouse.Y / getScaling(), b));
            }
            populateContactList();
        }


        private void btnAddContactLeft_Click(object sender, EventArgs e)
        {
            addContact("l");
        }


        private void btnAddContactBoth_Click(object sender, EventArgs e)
        {
            addContact("b");
        }


        private void btnAddContactRight_Click(object sender, EventArgs e)
        {
            addContact("r");
        }


        private Contact getSelectedContact()
        {
            Item item = getSelectedItem();
            Contact ret = null;
            int i = lbContact.SelectedIndex;
            if (i >= 0 && null != item)
            {
                ret = item.contacts[i];
            }
            return ret;
        }


        private void showContact(Contact c)
        {
            highlightedSide = "";
            if (null != c)
            {
                highlightedPos.X = c.x * getScaling();
                highlightedPos.Y = c.y * getScaling();
                highlightedSide = c.b;
            }
            updt();
        }


        private void lbContact_SelectedIndexChanged(object sender, EventArgs e)
        {
            showContact(getSelectedContact());
        }


        private void btnItemDel_Click(object sender, EventArgs e)
        {
            Item i = getSelectedItem();
            if(null != i)
            {
                cfg.items.Remove(i);
            }
            populateItemList();
        }


        private void btnDelContact_Click(object sender, EventArgs e)
        {
            Contact c = getSelectedContact();
            Item i = getSelectedItem();
            if(null != c && null != i)
            {
                i.contacts.Remove(c);
            }
            populateContactList();
            if (lbContact.Items.Count > 0)
                lbContact.SelectedIndex = 0;
        }


        private bool doesContactMatch(Contact c, int x, int y, string layer)
        {
            int distance = 10;
            bool ret = false;
            if(null != c)
            {
                if(c.b == layer || c.b=="b")
                    ret = (Math.Abs(c.x - x) < distance) && (Math.Abs(c.y - y) < distance);
            }
            return ret;
        }


        private Item findItemAtPos(int x, int y, string layer)
        {
            Item item = null;
            foreach(Item i in cfg.items)
            {
                if (null != i.contacts)
                {
                    foreach (Contact c in i.contacts)
                    {
                        if (doesContactMatch(c, x, y, layer))
                        {
                            item = i;
                            break;
                        }
                    }
                }
            }
            return item;
        }
    }
}
