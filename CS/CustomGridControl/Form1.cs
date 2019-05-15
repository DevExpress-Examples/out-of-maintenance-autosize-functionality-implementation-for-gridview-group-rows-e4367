using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace MyXtraGrid {
    public partial class Form1 : Form {
        public Form1() 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            DataTable someDT = new DataTable();
            someDT.Columns.Add("GroupInformation1", typeof(string));
            someDT.Columns.Add("GroupInformation2", typeof(string));
            for (int i = 0; i < 5; i++)
            {
                someDT.Columns.Add("Value" + i.ToString(), typeof(int));
            }
            Random myRand = new Random();
            for (int i = 0; i < 20; i++)
            {                
                if (i > 10)
                    someDT.Rows.Add("Long description for the Group " + (i % 2).ToString(), "Second Group " + (i % 3).ToString(), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100));
                else
                    someDT.Rows.Add("Group " + (i % 2).ToString(), "Long description for the Second Group " + (i % 3).ToString(), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100));

            }

            gridControl1.DataSource = someDT;
            gridControl1.ForceInitialize();

            gridView1.Columns["GroupInformation1"].GroupIndex = 0;
            gridView1.Columns["GroupInformation2"].GroupIndex = 1;

            gridView1.OptionsMenu.ShowGroupSummaryEditorItem = true;
            gridView1.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

        }

        private void gridView1_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            if (((sender as GridView)).IsGroupRow(e.RowHandle))
	        {
                GridViewInfo viewInfo = (sender as GridView).GetViewInfo() as GridViewInfo;

                // height indents calculating
                int singleLineTextHeight = viewInfo.PaintAppearance.GroupRow.CalcTextSizeInt(viewInfo.GInfo.Graphics, "Wg", 100).Height;                
                int groupRowTextOffset = e.RowHeight - singleLineTextHeight;
                
                string groupRowDisplayText = viewInfo.View.GetGroupRowDisplayText(e.RowHandle);

                // current group row width obtaining
                int requredWidth = viewInfo.ViewRects.Rows.Width - viewInfo.Painter.ElementsPainter.RowPreview.GetPreviewIndent(viewInfo) * (viewInfo.View.GetRowLevel(e.RowHandle) + 2);

                // required group row height calculating
                int requiredGroupRowTextHeight = viewInfo.PaintAppearance.GroupRow.CalcTextSizeInt(viewInfo.GInfo.Graphics, groupRowDisplayText, requredWidth).Height;
                e.RowHeight = requiredGroupRowTextHeight + groupRowTextOffset;
	        }
        }
    }
}