using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace cmspro.Models
{
    public class ViewModel
    {


        [Required(ErrorMessage = "ID not be empty")]
        public int user_id { get; set; }

        [Required(ErrorMessage = "Name not be empty")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Email not be empty")]
        public string user_email { get; set; }

        [Required(ErrorMessage = "Mobile not be empty")]
        public string user_mobile { get; set; }

        [Required(ErrorMessage = "Password not be empty")]
        public string user_pwd { get; set; }

        [Required(ErrorMessage = "Image not be empty")]
        public string user_image { get; set; }

        [Required(ErrorMessage = "Epassword not be empty")]
        public string user_epwd { get; set; }

        [Required(ErrorMessage = "Type not be empty")]
        public string user_type { get; set; }

        public Nullable<int> user_active { get; set; }

        [Required(ErrorMessage = "Site Url Must be in HTTP:// and not be empty")]
        public int site_id { get; set; }

        [Required(ErrorMessage = "Site Name not be empty")]
        public string site_name { get; set; }




        [Required(ErrorMessage = "ID not be empty")]
        public int e_id { get; set; }

        [Required(ErrorMessage = "Email not be empty")]
        public string email_invite { get; set; }

        [Required(ErrorMessage = "Message not be empty")]
        public string email_message { get; set; }

        [Required(ErrorMessage = "Subject not be empty")]
        public string email_subject { get; set; }





        [Required(ErrorMessage = "ID not be empty")]
        public int police_work_id { get; set; }
        [Required(ErrorMessage = "Name not be empty")]
        public string police_name { get; set; }
        [Required(ErrorMessage = "Mobile not be empty")]
        public string police_mobile { get; set; }
        [Required(ErrorMessage = "Email not be empty")]
        public string police_email { get; set; }
        [Required(ErrorMessage = "City not be empty")]
        public string police_city { get; set; }
        [Required(ErrorMessage = "State not be empty")]
        public string police_state { get; set; }
        [Required(ErrorMessage = "Status not be empty")]
        public string police_status { get; set; }
        [Required(ErrorMessage = "Work not be empty")]
        public string police_work { get; set; }
        [Required(ErrorMessage = "Time not be empty")]
        public string police_time { get; set; }






        [Required(ErrorMessage = "ID not be empty")]
        public int fir_id { get; set; }

        [Required(ErrorMessage = "Title not be empty")]
        public string fir_title { get; set; }

        [Required(ErrorMessage = "Name not be empty")]
        public string reporter_name { get; set; }

        [Required(ErrorMessage = "Mobile not be empty")]
        public string reporter_mobile { get; set; }

        [Required(ErrorMessage = "Email not be empty")]
        public string reporter_email { get; set; }

        [Required(ErrorMessage = "Report For not be empty")]
        public string report_for { get; set; }

        [Required(ErrorMessage = "Report Summary not be empty")]
        public string report_summary { get; set; }

        [Required(ErrorMessage = "Image not be empty")]
        public string report_image { get; set; }

        public string report_processing { get; set; }

        [Required(ErrorMessage = "City not be empty")]
        public string report_city { get; set; }

        [Required(ErrorMessage = "State not be empty")]
        public string report_state { get; set; }

        [Required(ErrorMessage = "Address not be empty")]
        public string report_address { get; set; }








        [Required(ErrorMessage = "Address not be empty")]
        public int fir_approved_id { get; set; }

        
        [Required(ErrorMessage = "Address not be empty")]
        public string police_id { get; set; }

        public string approve { get; set; }












        public int army_id { get; set; }
        public string army_name { get; set; }
        public string army_email { get; set; }
        public string army_mobile { get; set; }
        public string army_posting { get; set; }
        public string army_account_number { get; set; }
        public string army_ifsc_code { get; set; }
        public string army_image { get; set; }
        public string army_wife { get; set; }
        public string army_father { get; set; }
        public string army_address { get; set; }
        public string army_medal { get; set; }
        public string army_counter_strike { get; set; }
        public string army_hurt_count { get; set; }
        public string army_summary { get; set; }
    }
}