'Program Name:  Intuition Approved Travel Windows application
'Developer:     Jason A. Frye
'Date:          February 22, 2013
'Purpose        The approved travel requests window application opens an
'               access database with the approved company travel requests in
'               a windows form. The database can be viewed, updated, and 
'               deleted. The application also computes the total of the
'               travel costs that have been entered into the database

Imports System.Data

Public Class frmApprovedTravel

    Private Sub ApprovedTravelRequestsBindingNavigatorSaveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ApprovedTravelRequestsBindingNavigatorSaveItem.Click
        'This click even is created by the database wizard

        Me.Validate()
        Me.ApprovedTravelRequestsBindingSource.EndEdit()
        Me.TableAdapterManager.UpdateAll(Me.TravelDataSet)

    End Sub

    Private Sub frmApprovedTravel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'the database wizard creates this method
        'the Try catch block catched an exception caused by a missing database file. 

        Try
            'TODO: This line of code loads data into the 'TravelDataSet.ApprovedTravelRequests' table. You can move, or remove it, as needed.
            Me.ApprovedTravelRequestsTableAdapter.Fill(Me.TravelDataSet.ApprovedTravelRequests)
        Catch ex As Exception
            MsgBox("The database file is unavailable", , "Error")
            Close()
        End Try

    End Sub

    Private Sub btnTotalTravelCost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTotalTravelCost.Click

        'strSql is a SQL statement that selects all the fields from the approvedtravelrequests table
        Dim strSql As String = "Select * FROM ApprovedTravelRequests"

        'strPath provides the database type and path of the Travel database. 
        Dim strPath As String = "Provider=Microsoft.ACE.OLEDB.12.0 ;" & "Data Source=Travel.accdb"
        Dim odaTravel As New OleDb.OleDbDataAdapter(strSql, strPath)
        Dim datCost As New DataTable
        Dim intCount As Integer = 0
        Dim decTotalCost As Decimal = 0D

        'The datatable name datCost is filled with the table data
        odaTravel.Fill(datCost)

        'the conneciton to the database is disconnection
        odaTravel.Dispose()

        For intCount = 0 To datCost.Rows.Count - 1
            decTotalCost += Convert.ToDecimal(datCost.Rows(intCount)("Travel Cost"))

        Next
        lblTotalTravelCost.Visible = True
        lblTotalTravelCost.Text = "The total approved travel cost is " & decTotalCost.ToString("C")

    End Sub
End Class
