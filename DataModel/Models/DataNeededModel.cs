using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppApi.Models
{
    public enum DataNeededType
    {
        None,
        Alert,
        Error,
        //...
    }

    public enum DataNeededInputType
    {
        Block,   //  -- simple textblock
        Text,    //  -- text box input
        Password,//  -- password input
        Button   //  -- button action

        //checkbox
        //date
        //email
        //image
        //number
        //radio
        //tel
    }

    public class ActionCommandModel
    {

    }

    public class DataNeededModel
    {
        [Required]
        public string Type { get; set; }

        public string Title { get; set; }

        public List<DataNeededInputModel> Inputs { get; set; }
    }

    public class DataNeededReplyModel
    {
        [Required]
        public string Type { get; set; }

        public List<string> Data { get; set; }
    }

    public class DataNeededInputModel
    {
        public DataNeededInputType Type { get; set; }

        public int MaxLength { get; set; }
        public string Text { get; set; }
        public string Data { get; set; }
        public string Name { get; set; }
    }
}
