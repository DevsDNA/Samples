namespace USH.Models
{
    using Newtonsoft.Json;

    public class FaceModel
    {
        [JsonProperty("faceId")]
        public string FaceId { get; set; }

        [JsonProperty("faceRectangle")]
        public FaceRectangle BoundingBox { get; set; }

        [JsonProperty("faceAttributes")]
        public FaceAttributes Attributes { get; set; }
    }

    public class FaceRectangle
    {
        [JsonProperty("left")]
        public int X { get; set; }

        [JsonProperty("top")]
        public int Y { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }

    public class FaceAttributes
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("age")]
        public double Age { get; set; }
    }
}
