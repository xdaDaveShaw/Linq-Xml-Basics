<Query Kind="Statements" />

var xml = 
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Library xmlns=""http://taeguk.co.uk/People/"" xmlns:b=""http://taeguk.co.uk/Books/"">
  <Person id=""1"">
    <Name>Alice</Name>
    <b:Books>
      <b:Name id=""1"" rating=""5"">Philosopher's Stone</b:Name>
      <b:Name id=""2"" rating=""5"">Chamber of Secrets</b:Name>
    </b:Books>
  </Person>
  <Person id=""2"">
    <Name>Bob</Name>
    <b:Books>
      <b:Name id=""3"" rating=""5"">Prisoner of Azkaban</b:Name>
      <b:Name id=""4"">Goblet of Fire</b:Name>
    </b:Books>
  </Person>
</Library>";

//Assuming, "xml" is a String containing our XML.
//It's just this simple.
var document = XDocument.Parse(xml);

//#1
document
.Descendants("Name")
.Select(name => name.Value)
.Dump();

//#1.1
//Cannot use 'var' because it would be a string.
//You need to use XNamespace, then string gets implicitly 
//converted to the correct type
XNamespace ns = "http://taeguk.co.uk/People/";	
document
.Descendants(ns + "Name")		//Addition of XNamespace and string produces an XName.
.Select(name => name.Value)
.Dump();

//#1.2
document
.Root
.Elements(ns + "Person")		
.Where(xe => xe.Element(ns + "Name").Value == "Bob")
.Dump();

//#2
XNamespace bookNs = "http://taeguk.co.uk/Books/";
document
.Descendants(bookNs + "Name")
.Select(name => name.Value)
.Dump();


//#3
var ids = 
	document
	.Root
	.Descendants(bookNs + "Name")
	.Select(book => book.Attribute("id").Value);
ids.Dump();

//#3.1
var avgRating= 
	document
	.Root
	.Descendants(bookNs + "Name")
	.Select(book => book.Attribute("rating") != null ? (Int32?)Convert.ToInt32(book.Attribute("rating").Value) : null)
	.Average();
avgRating.Dump();

//#3.2
avgRating= 
	document
	.Root
	.Descendants(bookNs + "Name")
	.Select(
		book => 
			{
				var att = book.Attribute("rating");
				return
					att != null ? (Int32?)Convert.ToInt32(att.Value) : null;
			})
	.Average();
avgRating.Dump();

//#3.3
avgRating = 
	document
	.Root
	.Descendants(bookNs + "Name")
	.Select(book => (Int32?)book.Attribute("rating"))
	.Average();
avgRating.Dump();
