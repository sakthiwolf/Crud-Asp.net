using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace crud
{
    public partial class index : System.Web.UI.Page
    {      
        string CS = ConfigurationManager.ConnectionStrings["DBCON"].ConnectionString;

      
           

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }       
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
         
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                string selectedDate = datepicker.Text;

                if (string.IsNullOrEmpty(Convert.ToString(hdfid.Value)))
                {
                    if (fileUpload.HasFile)
                    {
                        fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                        fileUpload.SaveAs(Server.MapPath("~/img/" + fileName));
                    }


                    SqlConnection con = new SqlConnection(CS);
                    SqlCommand cmd = new SqlCommand("INSERT INTO tbl_std (Name, Gender, Img, Dropdown, Content, Checkbox,CalendarDate) VALUES (@Name, @Gender, @Img, @Dropdown, @Content, @Checkbox,@CalendarDate)", con);
                    cmd.Parameters.AddWithValue("@Name", TextBox.Text);
                    cmd.Parameters.AddWithValue("@Gender", radiobtn.SelectedValue);
                    cmd.Parameters.AddWithValue("@Img", fileName);
                    cmd.Parameters.AddWithValue("@Dropdown", DropDownaList.SelectedValue);
                    cmd.Parameters.AddWithValue("@Content", Message_Box.Text);
                    cmd.Parameters.AddWithValue("@Checkbox", CheckBox.Text);
                    cmd.Parameters.AddWithValue("@CalendarDate", selectedDate); 
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Response.Write("<script>alert('Add successful');</script>");
                    BindData();
                    ClearFields();
                }
                else
                {
                    if (fileUpload.HasFile)
                    {
                        fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                        fileUpload.SaveAs(Server.MapPath("~/img/" + fileName));
                    }
                    else
                    {
                        fileName = hidimg.Value;
                    }
                    SqlConnection con = new SqlConnection(CS);
                    SqlCommand cmd = new SqlCommand("UPDATE tbl_std SET Name=@Name, Gender=@Gender,Img=@Img, Dropdown=@Dropdown,Content=@Content,Checkbox=@Checkbox,CalendarDate=@CalendarDate where ID=@ID", con);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(hdfid.Value));
                    cmd.Parameters.AddWithValue("@Name", TextBox.Text);
                    cmd.Parameters.AddWithValue("@Gender",radiobtn.SelectedValue);
                    cmd.Parameters.AddWithValue("@Img", fileName);
                    cmd.Parameters.AddWithValue("@Dropdown",DropDownaList.SelectedValue);
                    cmd.Parameters.AddWithValue("@Content",Message_Box.Text);
                    cmd.Parameters.AddWithValue("@checkbox", CheckBox.Text);
                    cmd.Parameters.AddWithValue("@CalendarDate", selectedDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    Response.Write("<script>alert('Updated Successfully!');</script>");
                    BindData();
                    ClearFields();

                }
            }
            catch (Exception ex)
            {
              
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }
        protected void btnSelectDate_Click(object sender, EventArgs e)
        {
            //txtDate.Text = Calendar1.SelectedDate.ToShortDateString();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        protected void lnkbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                btnsubmit.Text = "Update";
                LinkButton lnk = (LinkButton)sender;
                string id = lnk.CommandArgument;
                hdfid.Value = id;
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_std where ID=@ID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataAdapter da =new SqlDataAdapter(cmd);
                DataTable dt =new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    TextBox.Text = dt.Rows[0]["Name"].ToString();
                    radiobtn.SelectedValue = dt.Rows[0] ["Gender"].ToString();
                    hidimg.Value = dt.Rows[0]["img"].ToString();
                
                    DropDownaList.SelectedValue = dt.Rows[0]["Dropdown"].ToString();
                    Message_Box.Text = dt.Rows[0]["Content"].ToString();
                    string checkboxValue = dt.Rows[0]["Checkbox"].ToString().ToLower();
                    CheckBox.Text= dt.Rows[0]["checkbox"].ToString();
                    datepicker.Text = dt.Rows[0]["CalendarDate"].ToString();
                    string imgFileName = dt.Rows[0]["img"].ToString();
                    if (!string.IsNullOrEmpty(imgFileName))
                    {
                        hidla.Text= imgFileName;
                        hidla.Visible = true;

                    }

                    if (!string.IsNullOrEmpty(hidimg.Value))
                    {
                        image.Visible = true;
                        image.ImageUrl = "~/img/" + hidimg.Value;
                    
                    }


                }
            }
            catch (Exception ex)

            {
                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk= (LinkButton)sender;
                string id = lnk.CommandArgument;
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("DELETE FROM tbl_std WHERE ID = @ID", con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                BindData();

            }
            
            catch(Exception ex) 
            { 
            
            }
          
        }

        protected void BindData()
        {
            try
            {
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_std", con);

                con.Open();
                reptproductlist.DataSource = cmd.ExecuteReader();
                reptproductlist.DataBind();

                con.Close();
            }
            catch (Exception ex)
            {

                Response.Write("<script>alert('An error occurred: " + ex.Message + "');</script>");
            }
        }

        protected void ClearFields()
        {
            TextBox.Text = "";
            radiobtn.ClearSelection();
            btnsubmit.Text = "Submit";
            image.Visible = false;
            datepicker.Text= string.Empty;
            DropDownaList.SelectedIndex = 0;
            CheckBox.ClearSelection();
            Message_Box.Text = "";
            hidla.Text
                = string.Empty;

        }
    }
}

