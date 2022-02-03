namespace SampleJson.Models
{
	using Newtonsoft.Json;
	using System.Collections.Generic;
	using System.Text.Json.Serialization;

	public class Enterprise
	{
		[Newtonsoft.Json.JsonIgnore]
		[System.Text.Json.Serialization.JsonIgnore]
		public long Id { get; set; }


		[JsonProperty("id")]
		[JsonPropertyName("id")]
		public int JsonId { get; set; }


		[JsonProperty("Name")]
		[JsonPropertyName("Name")]
		public string FullCompanyName { get; set; }
		
		public string Description { get; set; }
		
		public IEnumerable<string> Persons { get; set; }
	}
}