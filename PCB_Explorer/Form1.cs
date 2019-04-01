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
        int showOffsetX = 0;
        int showOffsetY = 0;
        int mouseDownX = 0;
        int mouseDownY = 0;
        bool moveImg = false;

        Image image1;
        Image image2;
        Pen redPen = new Pen(Color.Red, 2);
        Pen selPen = new Pen(Color.Yellow, 2);
        Pen selContactsPen = new Pen(Color.Cyan, 2);

        Config cfg;

        IList<Contact> selContacts = null;
        float selX = 0;
        float selY = 0;
        string selB = "";

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
            selB = "";
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


        private void drawImage(Image img, int x, int y, float scaleW, float scaleH, PaintEventArgs e)
        {
            RectangleF destinationRect = new RectangleF(
                    x,
                    y,
                    scaleW * image2.Width,
                    scaleH * image2.Height);
            e.Graphics.DrawImage(img, destinationRect);
        }

        private void drawPointer(Pen pen, float x, float y, PaintEventArgs e)
        {
            int w = 10;
            e.Graphics.DrawLine(pen, showOffsetX + x - w, showOffsetY + y, showOffsetX + x + w, showOffsetY + y);
            e.Graphics.DrawLine(pen, showOffsetX + x, showOffsetY + y - w, showOffsetX + x, showOffsetY + y + w);
        }

        private void drawPointerLeft(Pen pen, float x, float y, PaintEventArgs e)
        {
            drawPointer(pen, x + borderLeft, y + borderTop, e);
        }

        private void drawPointerRight(Pen pen, float x, float y, PaintEventArgs e)
        {
            int imgWidth = (int)(image1.Width * cfg.scale);
            drawPointer(pen, borderLeft + imgWidth + imageSpace + getMirroredX(x), y + borderTop, e);
        }

        private float getMirroredX(float x)
        {
            float imgWidth = (float)(image1.Width * cfg.scale);
            return (imgWidth - x);
        }

        private void drawImg(Image img, int x, int y, PaintEventArgs e)
        {
          //  showOffsetX;
          //  drawImage(img, borderLeft, borderTop, cfg.scale, cfg.scale, e);
        }

        

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawImage(image1, showOffsetX + borderLeft, showOffsetY + borderTop, cfg.scale, cfg.scale, e);
            //drawImg(image1, 0, 0, e);

            int imgWidth = (int)(image1.Width * cfg.scale);
            int imgBx = borderLeft + imageSpace + imgWidth;
            drawImage(image2, showOffsetX + imgBx + cfg.offsetX, showOffsetY + borderTop + cfg.offsetY, cfg.scale * cfg.scaleX, cfg.scale* cfg.scaleY, e);
            //drawImg(image2, 0, 0, e);

            drawPointerLeft(redPen, mouseX, mouseY, e);
            drawPointerRight(redPen, mouseX, mouseY, e);


            if (null != selContacts)
            {
                foreach (var c in selContacts)
                {
                    float x = c.x * cfg.scale;
                    float y = c.y * cfg.scale;
                    if (c.b == "l" || c.b == "b")
                        drawPointerLeft(selContactsPen, x, y, e);
                    if (c.b == "r" || c.b == "b")
                        drawPointerRight(selContactsPen, x, y, e);
                }
            }

            if (selB == "l" || selB == "b")
                drawPointerLeft(selPen, selX, selY, e);
            if (selB == "r" || selB == "b")
                drawPointerRight(selPen, selX, selY, e);
        }

        int mouseX = 0;
        int mouseY = 0;
        string mouseB = "";

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
            int x = (int)(((e.X - borderLeft) - showOffsetX) / cfg.scale);
            if (x > image1.Width)
            {
                x = (int)(((e.X - borderLeft - imageSpace) - showOffsetX) / cfg.scale);
                x = image1.Width - (x - image1.Width);
            }
            return x;
        }

        private int getUnscaledY(MouseEventArgs e)
        {
            int y = (int)(((e.Y - borderTop) - showOffsetY) / cfg.scale);
            return y;
        }

        private void updateMouseEvent(MouseEventArgs e)
        {
            int x = (int)(((e.X - borderLeft) - showOffsetX) / cfg.scale);
            int y = (int)(((e.Y - borderTop) - showOffsetY) / cfg.scale);

            string clickedLayer = (x < image1.Width) ? "l" : "r";

            if (e.Button == MouseButtons.Left)
            {
                Item i = findItemAtPos(getUnscaledX(e), getUnscaledY(e), clickedLayer);
                highlightItem(i);
            }
            else if (e.Button == MouseButtons.Right)
            {
                mouseX = (e.X - borderLeft) - showOffsetX;
                mouseY = (e.Y - borderTop) - showOffsetY;
                if(clickedLayer=="r")
                {
                    mouseX -= (imageSpace + (int)(image1.Width * cfg.scale));
                    float imgWidth = (float)(image1.Width * cfg.scale);
                    mouseX = (int)(imgWidth - mouseX);
                }
                int x2 = getUnscaledX(e);
                mouseB = clickedLayer;
                updt();
            }
            if ((Control.ModifierKeys & Keys.Shift) != 0)
            {
                showOffsetX -= (mouseDownX - e.X);
                showOffsetY -= (mouseDownY - e.Y);
                updt();
            }
            mouseDownX = e.X;
            mouseDownY = e.Y;
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
                i.contacts.Add(new Contact(mouseX / cfg.scale, mouseY / cfg.scale, b));
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
            selB = "";
            if (null != c)
            {
                selX = c.x * cfg.scale;
                selY = c.y * cfg.scale;
                selB = c.b;
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
