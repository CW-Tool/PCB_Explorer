using System;
using System.Collections;
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
        int borderLeft = 150;
        int imageSpace = 10;
        int borderTop = 10;
        string dataPath = Path.Combine(Application.StartupPath,  @"..\..\..\data");
        string cfgFile = Path.Combine(Application.StartupPath, @"..\..\..\data\cfg.json");

        Image image1;
        Image image2;
        Pen redPen = new Pen(Color.Red, 1);
        Pen selPen = new Pen(Color.Yellow, 1);
        Pen selContactsPen = new Pen(Color.Cyan, 1);

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
            lbContact.Items.Clear();
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
            e.Graphics.DrawLine(pen, x - w, y, x + w, y);
            e.Graphics.DrawLine(pen, x, y-w, x, y+w);
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

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawImage(image1, borderLeft, borderTop, cfg.scale, cfg.scale, e);

            int imgWidth = (int)(image1.Width * cfg.scale);
            int imgBx = borderLeft + imageSpace + imgWidth;
            drawImage(image2, imgBx + cfg.offsetX, borderTop + cfg.offsetY, cfg.scale * cfg.scaleX, cfg.scale* cfg.scaleY, e);

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

        private void updateMouseEvent(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                mouseX = e.X - borderLeft;
                mouseY = e.Y - borderTop;
                updt();
            }
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
            if(null != c)
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            updateMouseEvent(e);

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
    }
}
