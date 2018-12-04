namespace USH.Models
{
    using System;
    using System.Collections.Generic;

    public class CustomVisionModel
    {
        public string Id { get; set; }
        public string Project { get; set; }
        public string Iteration { get; set; }
        public DateTime Created { get; set; }
        public List<Tags> Predictions { get; set; }
    }

    public class Tags
    {
        public string TagId { get; set; }
        public string TagName { get; set; }
        public double Probability { get; set; }
    }
}
