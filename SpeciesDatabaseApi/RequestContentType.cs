using System.ComponentModel;

namespace SpeciesDatabaseApi;

public enum RequestContentType
{
	Raw,

	[Description("application/json")]
	Json,

	[Description("application/xml")]
	Xml
}