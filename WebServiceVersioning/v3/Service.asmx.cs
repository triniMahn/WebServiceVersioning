using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace WebServiceVersioning.v3
{
    /// <summary>
    /// Summary description for Service
    /// </summary>
    [WebService(Namespace = "http://www.canadahelps.org/v3")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Service : System.Web.Services.WebService
    {

        #region *V3* Web Service Data Classes (External)

        [XmlRoot("WSCustomerInfoV3", Namespace = "http://www.canadahelps.org/v3")]
        public class WSCustomerInfo: WebServiceVersioning.v2.Service.WSCustomerInfo
        {
            public string SIN { get; set; }
            
            public WSCustomerInfo()
            {
            }

            public override List<int> validate()
            {
                Validators.AbstractValidator validator = new Validators.WSCustomerInfoValidatorV1();
                validator = new Validators.WSCusomterInfoValidatorV2(validator);
                validator = new Validators.WSCusomterInfoValidatorV3(validator);

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
            response.ResponseMessage = "Response from Version *3* Method";
            response.ErrorCodes = request.CustomerInfo.validate().ToArray();
            return response;
        }
    }
}
