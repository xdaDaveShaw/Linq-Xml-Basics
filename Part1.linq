<Query Kind="Statements" />

var xml = 
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Files>
	<File id=""1234"" type=""document"">
		<Name>Document.txt</Name>
		<Size>44</Size>
	</File>
	<File id=""5678"" type=""image"">
		<Name>Picture.bmp</Name>
		<Size>100</Size>
	</File>
</Files>";

//Assuming, "xml" is a String containing our XML.
//It's just this simple.
var document = XDocument.Parse(xml);

//#1
document
.Root				//Get the root element - "Files" in our case.
.Elements("File")	//Get the elements called "File" in "Files".
.Dump();

//#2
document
.Root
.Elements("File")					//Get the elements called "File" in "Files".
.SelectMany(						//We have multiple File elements, with Elements, 
	element =>						//so we flatten the collection with SelectMany().
		element.Elements("Name"))	//Get all the elements in each File element, called "Name".
.Dump();

//#3
document
.Root
.Descendants("Name")	//Get the elements called "Name" in "Files" (the Root).
.Dump();

//#4
Int32 fileId = 1234;
document
.Root
.Elements("File")							 	//Get the elements called "File" in "Files". Where the "id"
.Where(e => Convert.ToInt32(e.Attribute("id").Value) == fileId)	 //attribute has a value that matches fileId.
.Select(
	e => 										//e is now any element called "File" with a matching ID.
		new
		{											
			Name = e.Element("Name").Value,		//Get the element called Name's value.
			Size = e.Element("Size").Value,		//Get the element called Sizes's value.
			Type = e.Attribute("type").Value,	//Get the value from the attribute from File called Type.
		})
.Single()										//Ensure there is only one.
.Dump();