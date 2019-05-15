Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace MyXtraGrid
	Partial Public Class Form1
		Inherits Form

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
			Dim someDT As New DataTable()
			someDT.Columns.Add("GroupInformation1", GetType(String))
			someDT.Columns.Add("GroupInformation2", GetType(String))
			For i As Integer = 0 To 4
				someDT.Columns.Add("Value" & i.ToString(), GetType(Integer))
			Next i
			Dim myRand As New Random()
			For i As Integer = 0 To 19
				If i > 10 Then
					someDT.Rows.Add("Long description for the Group " & (i Mod 2).ToString(), "Second Group " & (i Mod 3).ToString(), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100))
				Else
					someDT.Rows.Add("Group " & (i Mod 2).ToString(), "Long description for the Second Group " & (i Mod 3).ToString(), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100), myRand.Next(1, 100))
				End If

			Next i

			gridControl1.DataSource = someDT
			gridControl1.ForceInitialize()

			gridView1.Columns("GroupInformation1").GroupIndex = 0
			gridView1.Columns("GroupInformation2").GroupIndex = 1

			gridView1.OptionsMenu.ShowGroupSummaryEditorItem = True
			gridView1.Appearance.GroupRow.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap

		End Sub

		Private Sub gridView1_CalcRowHeight(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs) Handles gridView1.CalcRowHeight
			If ((TryCast(sender, GridView))).IsGroupRow(e.RowHandle) Then
				Dim viewInfo As GridViewInfo = TryCast((TryCast(sender, GridView)).GetViewInfo(), GridViewInfo)

				' height indents calculating
				Dim singleLineTextHeight As Integer = viewInfo.PaintAppearance.GroupRow.CalcTextSizeInt(viewInfo.GInfo.Graphics, "Wg", 100).Height
				Dim groupRowTextOffset As Integer = e.RowHeight - singleLineTextHeight

				Dim groupRowDisplayText As String = viewInfo.View.GetGroupRowDisplayText(e.RowHandle)

				' current group row width obtaining
				Dim requredWidth As Integer = viewInfo.ViewRects.Rows.Width - viewInfo.Painter.ElementsPainter.RowPreview.GetPreviewIndent(viewInfo) * (viewInfo.View.GetRowLevel(e.RowHandle) + 2)

				' required group row height calculating
				Dim requiredGroupRowTextHeight As Integer = viewInfo.PaintAppearance.GroupRow.CalcTextSizeInt(viewInfo.GInfo.Graphics, groupRowDisplayText, requredWidth).Height
				e.RowHeight = requiredGroupRowTextHeight + groupRowTextOffset
			End If
		End Sub
	End Class
End Namespace