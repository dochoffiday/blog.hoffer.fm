using System;
using System.Text.RegularExpressions;

namespace AJ.UtiliTools
{
    public class UserPage : System.Web.UI.Page
    {
        protected bool IsInputValid = true;

        protected void Validate(String response, System.Web.UI.WebControls.WebControl controlToValidate, System.Web.UI.HtmlControls.HtmlGenericControl label)
        {
            String errorPattern = @"\b{0}\b".F("error");
            if (!response.IsNullOrEmpty())
            {
                IsInputValid = false;

                if(!Regex.IsMatch(controlToValidate.CssClass, errorPattern))
                {
                    controlToValidate.CssClass = controlToValidate.CssClass + "error";
                }

                label.InnerText = response;
                label.Visible = true;
            }
            else
            {
                controlToValidate.CssClass = Regex.Replace(controlToValidate.CssClass, errorPattern, "");
                label.InnerText = "";
                label.Visible = false;
            }
        }
    }
}