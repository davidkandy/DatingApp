using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class AccessTokenModel
    {
        /// <summary>
        /// JWT Access Token for user account.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
