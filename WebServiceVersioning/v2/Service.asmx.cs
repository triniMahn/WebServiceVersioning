using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace WebServiceVersioning.v2
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://www.canadahelps.org/v2")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        #region *V2* Web Service Data Classes (External)
        [XmlRoot("WSCustomerInfoV2", Namespace = "http://www.canadahelps.org/v2")]
        public class WSCustomerInfo: WebServiceVersioning.Service.WSCustomerInfo
        {
            public string EmailAddress { get; set; }
            
            public WSCustomerInfo()
            {
                
            }

            public override List<int> validate()
            {
                Validators.AbstractValidator validator = new Validators.WSCustomerInfoValidatorV1();
                validator = new Validators.WSCusomterInfoValidatorV2(validator);

                return validator.isValid(this);
            }
        }

        public class WSRequest
        {
            public WSCustomerInfo CustomerInfo { get; set; }

            public WSRequest()
            {
            }

        }

        public class WSResponse
        {
            public string ResponseMessage { get; set; }
            public int[] ErrorCodes { get; set; }
            public WSResponse() { }

        }

        #endregion

        [WebMethod]
        public WSResponse ProcessRequest(WSRequest request)
        {
            WSResponse response = new WSResponse();
            response.ResponseMessage = "Response from Version *2* Method";
            response.ErrorCodes = request.CustomerInfo.validate().ToArray();
            return response;
        }
    }
}
