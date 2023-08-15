using System.Collections.Generic;
using System.Xml.Serialization;

namespace SpeciesDatabaseApi.BoldSystems;

[XmlRoot("matches")]
public class CoiMatches : List<CoiMatch>
{
    
}