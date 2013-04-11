using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace WebServiceVersioning
{
    /// <summary>
    /// Summary description for Service1
    /// </summary>
    [WebService(Namespace = "http://www.canadahelps.org/v1")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class Service : System.Web.Services.WebService
    {

        #region Web Service Data Classes (External)

        [XmlRoot("WSCustomerInfoV1", Namespace = "http://www.canadahelps.org/v1")]
        public class WSCustomerInfo
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string CompanyName { get; set; }
            public string Street1 { get; set; }
            public string Street2 { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Country { get; set; }
            public string PostalCode { get; set; }

            

            public WSCustomerInfo()
            {
                
            }

            public virtual List<int> validate()
            {
                Validators.AbstractValidator validator = new Validators.WSCustomerInfoValidatorV1();
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
            response.ResponseMessage = "Response from Version 1 Method";
            response.ErrorCodes = request.CustomerInfo.validate().ToArray();
            return response;    
        }
    }
}